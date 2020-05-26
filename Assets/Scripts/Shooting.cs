using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    // para el cooldown
    private float timeLastShot = 0;
    public float cooldownTime = 0.3f;
    //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetButtonDown("Fire1") && Time.time - timeLastShot > cooldownTime)
        {
            Shoot();
            timeLastShot = Time.time;
        }
    }

    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);

        // destruir la bala para que no se acumulen en la escena por si las dudas
        Destroy(bullet, 15f);
    }
}
