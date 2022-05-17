using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    public void LifeTime()
    {
        Invoke("ResetQueue", 1f);
    }
    void ResetQueue()
    {
        Pooling_Control.instance.InsertQueue(this.gameObject,300);
    }
}
