using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Arrow : MonoBehaviour
{
   
    [SerializeField]
    private float ShootSpeed = 5f;
    private Vector3 ShootDir;
    Rigidbody rgbd;
    void Awake()
    {
       
        rgbd = GetComponent<Rigidbody>();
        rgbd.AddForce(transform.forward * ShootSpeed, ForceMode.Impulse);

        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().EnemyHp -= 100;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }


}
