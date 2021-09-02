using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Tile : MonoBehaviour
{
    bool Attack_controll=false;
    AudioSource spikeSound;
    public Animator spikeTrapAnim;
    void Awake()
    {
        //get the Animator component from the trap;
        spikeTrapAnim = GetComponent<Animator>();
        //start opening and closing the trap for demo purposes;
        spikeSound = GetComponent<AudioSource>();
    }

    IEnumerator SpikeUpDown(float sec)
    {
        spikeTrapAnim.SetTrigger("open");
        yield return new WaitForSeconds(sec);
        spikeTrapAnim.SetTrigger("close");
        yield return new WaitForSeconds(1.5f);
        Attack_controll = false;

    }
    IEnumerator waitSecond(float sec)
    {
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")&&Attack_controll==false)
        {
            Attack_controll = true;
            spikeSound.Play();
            StartCoroutine(SpikeUpDown(0.2f));
        }
    }
}
