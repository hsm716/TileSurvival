using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMove : MonoBehaviour
{
    float x, z;
    Vector3 pos;
    void Start()
    {
        StartCoroutine(move(1f));
    }


    IEnumerator move(float sec)
    {
        while (true)
        {
            x = Random.Range(-60, 50);
            z = Random.Range(-300, -170);
            pos = new Vector3(x, 0f, z);
            transform.position = pos;

            yield return new WaitForSeconds(sec);
        }
 
    }
}
