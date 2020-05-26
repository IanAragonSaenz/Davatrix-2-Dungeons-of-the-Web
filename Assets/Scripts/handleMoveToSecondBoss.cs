using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class handleMoveToSecondBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToPyramidHead());
    }

    IEnumerator MoveToPyramidHead(){
        yield return new WaitForSeconds(18.6f);
        SceneManager.LoadScene(9);
    }
}
