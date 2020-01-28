using UnityEngine;

public enum WeaponShootType
{
    Manual,
    Automatic,
    Charge,
}

[System.Serializable]
public struct CrosshairData
{
    [Tooltip("The image that will be used for this weapon's crosshair")]
    public Sprite crosshairSprite;
    [Tooltip("The size of the crosshair image")]
    public int crosshairSize;
    [Tooltip("The color of the crosshair image")]
    public Color crosshairColor;
}

[RequireComponent(typeof(AudioSource))]
public class WeaponController : MonoBehaviour
{
    [Header("Information")]
    [Tooltip("The name that will be displayed in the UI for this weapon")]
    public string weaponName;
    [Tooltip("The image that will be displayed in the UI for this weapon")]
    public Sprite weaponIcon;

    [Tooltip("Default data for the crosshair")]
    public CrosshairData crosshairDataDefault;
    [Tooltip("Data for the crosshair when targeting an enemy")]
    public CrosshairData crosshairDataTargetInSight;

    [Header("Internal References")]
    [Tooltip("The root object for the weapon, this is what will be deactivated when the weapon isn't active")]
    public GameObject weaponRoot;
    [Tooltip("Tip of the weapon, where the projectiles are shot")]
    public Transform weaponMuzzle;

    [Header("Shoot Parameters")]
    [Tooltip("The type of weapon wil affect how it shoots")]
    public WeaponShootType shootType;
    [Tooltip("The projectile prefab")]
    public ProjectileBase projectilePrefab;
    [Tooltip("Minimum duration between two shots")]
    public float delayBetweenShots = 0.5f;
    [Tooltip("Angle for the cone in which the bullets will be shot randomly (0 means no spread at all)")]
    public float bulletSpreadAngle = 0f;
    [Tooltip("Amount of bullets per shot")]
    public int bulletsPerShot = 1;
    [Tooltip("Force that will push back the weapon after each shot")]
    [Range(0f, 2f)]
    public float recoilForce = 1;
    [Tooltip("Ratio of the default FOV that this weapon applies while aiming")]
    [Range(0f, 1f)]
    public float aimZoomRatio = 1f;
    [Tooltip("Translation to apply to weapon arm when aiming with this weapon")]
    public Vector3 aimOffset;

    [Header("Ammo Parameters")]
    [Tooltip("Amount of ammo reloaded per second")]
    public float ammoReloadRate = 1f;
    [Tooltip("Delay after the last shot before starting to reload")]
    public float ammoReloadDelay = 2f;
    [Tooltip("Maximum amount of ammo in the gun")]
    public float maxAmmo = 8;

    [Header("Charging parameters (charging weapons only)")]
    [Tooltip("Duration to reach maximum charge")]
    public float maxChargeDuration = 2f;
    [Tooltip("Initial ammo used when starting to charge")]
    public float ammoUsedOnStartCharge = 1f;
    [Tooltip("Additional ammo used when charge reaches its maximum")]
    public float ammoUsageRateWhileCharging = 1f;

    [Header("Audio & Visual")]
    [Tooltip("Prefab of the muzzle flash")]
    public GameObject muzzleFlashPrefab;
    [Tooltip("sound played when shooting")]
    public AudioClip shootSFX;
    [Tooltip("Sound played when changing to this weapon")]
    public AudioClip changeWeaponSFX;

    float m_CurrentAmmo;
    float m_LastTimeShot = Mathf.NegativeInfinity;
    float m_TimeBeginCharge;
    Vector3 m_LastMuzzlePosition;

    public GameObject owner { get; set; }
    public GameObject sourcePrefab { get; set; }
    public bool charging { get; private set; }
    public float currentAmmoRatio { get; private set; }
    public bool weaponActive { get; private set; }
    public bool cooling { get; private set; }
    public float currentCharge { get; private set; }
    public Vector3 muzzleWorldVelocity { get; private set; }
    public float GetAmmoNeededToShoot() => (shootType != WeaponShootType.Charge ? 1 : ammoUsedOnStartCharge) / maxAmmo;

    AudioSource m_ShootAudioSource;
    // Start is called before the first frame update
    void Awake()
    {
        m_CurrentAmmo = maxAmmo;
        m_LastMuzzlePosition = weaponMuzzle.position;

        m_ShootAudioSource = GetComponent<AudioSource>();
        DebugUtility.HandleErrorIfNullGetComponent<AudioSource, WeaponController>(m_ShootAudioSource, this, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmo();
        UpdateCharge();

        if (Time.deltaTime > 0)
        {
            muzzleWorldVelocity = (weaponMuzzle.position - m_LastMuzzlePosition) / Time.deltaTime;
            m_LastMuzzlePosition = weaponMuzzle.position;
        }
    }

    void UpdateAmmo()
    {
        if (m_LastTimeShot + ammoReloadDelay < Time.time && m_CurrentAmmo < maxAmmo && !charging)
        {
            //reloads the ammo over time
            m_CurrentAmmo += ammoReloadRate * Time.deltaTime;
            m_CurrentAmmo = Mathf.Clamp(m_CurrentAmmo, 0, maxAmmo);
            cooling = true;
        }
        else cooling = false;

        if (maxAmmo == Mathf.Infinity) currentAmmoRatio = 1f;
        else currentAmmoRatio = m_CurrentAmmo / maxAmmo;
    }

    void UpdateCharge()
    {
        if (charging && currentCharge < 1f)
        {
            float chargesLeft = 1f - currentCharge;
            //checks the charge ratio to add this frame
            float chargeAdded = 0f;
            if (maxChargeDuration <= 0f) chargeAdded = chargesLeft;
            chargeAdded = (1f / maxChargeDuration) * Time.deltaTime;
            chargeAdded = Mathf.Clamp(chargeAdded, 0f, chargesLeft);

            //checks if charge can actually fit
            float ammoThisChargeWouldRequire = chargeAdded * ammoUsageRateWhileCharging;

            UseAmmo(ammoThisChargeWouldRequire);

            currentCharge = Mathf.Clamp01(currentCharge + chargeAdded);


        }
    }

    public void UseAmmo(float amount)
    {
        m_CurrentAmmo = Mathf.Clamp(m_CurrentAmmo - amount, 0f, maxAmmo);
        m_LastTimeShot = Time.time;
    }

    public void ShowWeapon(bool show)
    {
        weaponRoot.SetActive(show);
        if (show && changeWeaponSFX) m_ShootAudioSource.PlayOneShot(changeWeaponSFX);
        weaponActive = show;
    }

    public bool HandleShootType(bool inputDown, bool inputHeld, bool inputUp)
    {
        switch (shootType)
        {
            case WeaponShootType.Manual:
                if (inputDown) return TryShoot();
                return false;

            case WeaponShootType.Automatic:
                if (inputHeld) return TryShoot();
                return false;

            case WeaponShootType.Charge:
                if (inputHeld) TryBeginCharge();
                if (inputUp) return TryReleaseCharge();
                return false;

            default:
                return false;
        }
    }

    bool TryShoot()
    {
        if (m_CurrentAmmo > 0 && m_LastTimeShot + delayBetweenShots <= Time.time)
        {
            HandleShoot();
            m_CurrentAmmo--;
            return true;
        }
        return false;
    }

    bool TryBeginCharge()
    {
        if (!charging && m_CurrentAmmo >= ammoUsedOnStartCharge && m_LastTimeShot + delayBetweenShots <= Time.time)
        {
            UseAmmo(ammoUsedOnStartCharge);
            charging = true;
            return true;
        }
        return false;
    }

    bool TryReleaseCharge()
    {
        if (charging)
        {
            HandleShoot();
            currentCharge = 0;
            return true;
        }
        return false;
    }

    void HandleShoot()
    {
        //this creates the bullets per shot and shoots them
        for(int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
            newProjectile.Shoot(this);
        }

        //makes the muzzle flash if there is any
        if(muzzleFlashPrefab != null)
        {
            GameObject muzzleFlashInstance = Instantiate(muzzleFlashPrefab, weaponMuzzle.position, weaponMuzzle.rotation, weaponMuzzle.transform);
            Destroy(muzzleFlashInstance, 2f);
        }

        m_LastTimeShot = Time.time;

        //shoots SFX sound
        if (shootSFX) m_ShootAudioSource.PlayOneShot(shootSFX);
        
    }

    public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
    {
        float spreadAngleRatio = bulletSpreadAngle / 180f;
        Vector3 spreadWorldDirection = Vector3.Slerp(shootTransform.forward, UnityEngine.Random.insideUnitSphere, spreadAngleRatio);

        return spreadWorldDirection;
    }

}   


