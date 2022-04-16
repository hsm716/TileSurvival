using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    [SerializeField]
    private float ShootSpeed = 5f;
    private Vector3 ShootDir;

    public void LifeTime()
    {
        Invoke("ResetBullet",3f);
    }
    void ResetBullet()
    {
        Pooling_Control.instance.InsertQueue(this.gameObject, 4);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().EnemyHp -= 100;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Collider col = GetComponent<SphereCollider>();
            col.enabled = false;
            StartCoroutine(StopAct());
            //Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            //Pooling_Control.instance.InsertQueue(this.gameObject, 4);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Collider col = GetComponent<SphereCollider>();
            col.enabled = false;
            StartCoroutine(StopAct());
            //Destroy(this.gameObject);
        }
    }
    IEnumerator StopAct()
    {
        yield return new WaitForSeconds(0.5f);
        Collider col = GetComponent<SphereCollider>();
        col.enabled = true;
        Pooling_Control.instance.InsertQueue(this.gameObject, 4);
    }


}
