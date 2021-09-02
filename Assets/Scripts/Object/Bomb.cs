using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject Effect;
    [SerializeField]
    private Renderer bomb;
    private Color OriginColor;

    Camera_ camera;

    AudioSource BoomSound;

    bool Onoff = false;
    float timer;
    int waitingTime;
    int touchNumber;

    void Awake()
    {
        touchNumber = 0;
        timer = 0.0f;
        waitingTime = 3;
        OriginColor = new Color(0.5f,0.5f,0.5f);

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera_>();
        BoomSound = GetComponent<AudioSource>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchNumber += 1;
            Onoff = true;
            if (touchNumber == 1)
            {
                BoomSound.Play();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Onoff)
        {
            timer += Time.deltaTime;
            float flicker = Mathf.Abs(Mathf.Sin(Time.time * 10));
            bomb.material.color = OriginColor * flicker;
            
            if (timer > waitingTime)
            {
                
                timer = 0;
                Instantiate(Effect, transform.position, transform.rotation);
                camera.VibrateForTime(0.5f);
                Collider[] colls = Physics.OverlapSphere(transform.position, 15.0f);

                //추출한 Collider 객체에 폭발력 전달
                foreach (Collider coll in colls)
                {
                    Rigidbody rbody = coll.GetComponent<Rigidbody>();
                    if (rbody != null)
                    {
                        rbody.mass = 1.0f;
                        rbody.AddExplosionForce(500f, transform.position, 10.0f, 300.0f);
                    }
                    if (coll.gameObject.CompareTag("Enemy"))
                    {
                        coll.gameObject.GetComponent<Enemy>().EnemyHp -= 200f;
                    }
                }
                
                Destroy(gameObject);
            }
           

        }
    }
}
