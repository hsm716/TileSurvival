using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro_act : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem AggroEffect;
    public void StartAct(float sec)
    {
        StartCoroutine(aggro_effect(sec));
    }

    IEnumerator aggro_effect(float sec)
    {
        yield return new WaitForSeconds(sec);
        AggroEffect.Play();
        transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.tag = "Untagged";
        Pooling_Control.instance.targeting_queue.RemoveAt(0);
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(0).gameObject.SetActive(true);
        Pooling_Control.instance.InsertQueue(this.gameObject, 5);
    }

}
