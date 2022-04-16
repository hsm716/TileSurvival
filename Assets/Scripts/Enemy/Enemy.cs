using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb_obj;
    [SerializeField]
    private GameObject Hp_obj;
    [SerializeField]
    private Collider Enemy_col;


    Camera_ camera;



    public GameObject bloodPrefab;
    public GameObject BeShotPrefab;
    public GameObject bombPrefab;

    //GameObject player;
    bool isAggro;

    Player player;

    Animator animator;

    Rigidbody rgbd;

    NavMeshAgent nav;

    public int EnemyMode;
    public float EnemyHp;

    float time = 0;


    public ParticleSystem Spawn_effect;
    public ParticleSystem hit_blood_effect;
    public ParticleSystem hit_iron_effect;
    public ParticleSystem bomb_effect;


    void Awake()
    {

        //player = GameObject.FindGameObjectWithTag("Player");
        //player = Player.instance;
        rgbd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        //playerLogic = player.GetComponent<Player>();
        if (EnemyMode == 1 || EnemyMode == 2|| EnemyMode==100)
        {
            EnemyHp = 10f;
        }
        if (EnemyMode == 3)
        {
            EnemyHp = 1000f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")||other.gameObject.CompareTag("spike")|| 
            other.gameObject.CompareTag("bomb")
            )// 적이 플레이어가 발사하는 총알이나, Spike_Tile의 Spike나, 폭탄을 맞게 되면..
        {
            hit_blood_effect.Play();// 피 이펙트를 재생한다.
            //Instantiate(bloodPrefab, transform.position,transform.rotation); 
        }
        if (other.gameObject.CompareTag("Bullet") && EnemyMode == 3)
        {
            hit_iron_effect.Play(); // 적 종류가 거대화 적이라면, 추가 피격 이펙트도 재생한다.
        }
        if (other.gameObject.CompareTag("Player")) //플레이어와 부딫히는 방식으로 공격하게 되는데,
        {
            if (EnemyMode == 1|| EnemyMode == 100)//일반 적
            {
                Player.instance.TakeDamage(5); // 일반적은 5정도의 데미지를 입히고
            }
            else if (EnemyMode == 2)//폭탄 적
            {
                bomb_effect.Play();
                EnemyHp = -10f;
                Camera_.instance.VibrateForTime(0.1f);
                Collider[] colls = Physics.OverlapSphere(transform.position, 10.0f); //실수값 10정도의 반경의 충돌체들을 파악하여

                //추출한 Collider 객체에 폭발력 전달
                foreach (Collider coll in colls) // 반경안에 든 충돌체(오브젝트)들에게
                {
                    Rigidbody rbody = coll.GetComponent<Rigidbody>();
                    if (rbody != null)
                    {
                        rbody.mass = 1.0f;
                        rbody.AddExplosionForce(100.0f, transform.position, 10.0f, 300.0f); // AddForce로 힘을 가해 반동을 주고
                    }
                    if (coll.gameObject.CompareTag("Player")) //충돌체가 플레이어라면 피해를 입힌다.
                    {
                        Player.instance.TakeDamage(10);
                    }
                }
               // Die(); 
                //Destroy(this.gameObject);
            }
            else if ( EnemyMode == 3)//큰 적
            {
                Player.instance.TakeDamage(15); //플레이어에게 15정도의 데미지를 입힌다.
            }       
           
        }   
    }


    void Die()
    {
  
        if (EnemyMode == 1)
        {
            GameManager.instance.GetPoint(10); //플레이어의 점수를 증가시킨다.
            EnemyHp = 10;  // 다시 Que에 들어가기 전 최대 체력으로 초기화 시켜준다.
            
            Pooling_Control.instance.InsertQueue(gameObject,1); // 자신의 종류에 맞게 que에 들어감
        }
        else if (EnemyMode == 2)
        {
            GameManager.instance.GetPoint(50);
            EnemyHp = 10;
            Pooling_Control.instance.InsertQueue(gameObject,2);
        }
        else if (EnemyMode == 3)
        {
            GameManager.instance.GetPoint(100);
            EnemyHp = 1000;
            Pooling_Control.instance.InsertQueue(gameObject, 3);
        }
        else if (EnemyMode == 100)
        {
            GameManager.instance.GetPoint(5);
            EnemyHp = 10;
            Pooling_Control.instance.InsertQueue(gameObject, 100);
        }
        Enemy_col.enabled = true; // 다시 Collider를 원상복구시킨다.
        nav.enabled = true;
        int rand_value = Random.Range(1, 100); //랜덤 값으로 초기화 시킨다.

        if (rand_value >= 1 && rand_value <= 50&&EnemyMode==2) // 적 종류가 폭탄 적이라면 50% 확률로 폭탄 오브젝트를 Drop한다 
        {
            GameObject go = Pooling_Control.instance.GetQueue(6);
            go.transform.position = transform.position+new Vector3(0f,0.8f,0f);
            go.transform.rotation = transform.rotation;
            //Instantiate(bomb_obj,transform.position,transform.rotation);
        }
        if (rand_value >= 51 && rand_value <= 55) // 적 종류에 상관 없이 5% 확률로 회복 오브젝트를 Drop한다
        {
            GameObject go = Pooling_Control.instance.GetQueue(7);
            go.transform.position = transform.position+new Vector3(0f,0.8f,0f);
            go.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            //Instantiate(Hp_obj, transform.position, Quaternion.Euler(new Vector3(90,0,0)));
        }
        gameObject.SetActive(false);
        
    }

    void Update()
    {
        rgbd.velocity = new Vector3(0, 0, 0); //충돌로 인한 가속력을 억제해준다.

       
        // 아니라면 플레이어를 쫓게 한다.
         nav.SetDestination(Pooling_Control.instance.targeting_queue[0].position);


        if (EnemyHp <= 0) // 적이 죽게되면,
        {

            Enemy_col.enabled=false; // 적의 Collider를 없애주어 다른 적들의 경로를 방해하지 못하게 하고,
            nav.enabled = false;// 움직이지 못하게 처리하고
            animator.SetBool("isDie", true); // 죽는 모션의 애니매이션을 실행한다
            StartCoroutine(waitSec(1f)); // 애니매이션 모션이 1초정도 걸리므로 1초를 기다렸다 죽게 함.
        }
    }
    IEnumerator waitSec(float sec)
    {
        yield return new WaitForSeconds(sec);
        Die();
    }
}
