using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_act : MonoBehaviour
{
    public bool main_body;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(100f,0, Quaternion.identity);
           
        }
    }
    public void Act()
    {
        Invoke("LifeCycle", 4f);
    }
    void LifeCycle()
    {
        if(main_body)
            Pooling_Control.instance.InsertQueue(this.gameObject, 8);
    }
}
