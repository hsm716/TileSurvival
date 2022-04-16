using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int type;
    
    private float nor_xPos;
    private float nor_zPos;

    private float bomb_xPos;
    private float bomb_zPos;

    private float armor_xPos;
    private float armor_zPos;

    private Vector3 nor_RandomVector;
    private Vector3 bomb_RandomVector;
    private Vector3 armor_RandomVector;

    //public int stage = 2;

    float time = 0;
    
    [SerializeField]
    private float Normal_SpawnTime=2f;
    [SerializeField]
    private float Bomb_SpawnTime = 10f;
    [SerializeField]
    private float Armor_SpawnTime = 60f;
    [SerializeField]
    private float Zombie_SpawnTime = 1f;

    void Start()
    {
        if (type == 0)
        {

            StartCoroutine(normal_spawn());
            StartCoroutine(Bomb_spawn());
            StartCoroutine(Armor_spawn());
        }
        else if (type == 1)
        {
            StartCoroutine(zombie_spawn());
        }
    }
   


    private void Update()
    {
        time = GameManager.instance.time;
        if (type == 0)
        {
            if (time >= 240)
            {
                Normal_SpawnTime = 1f;
            }
            if (time >= 360)
            {
                Normal_SpawnTime = 0.7f;
                Bomb_SpawnTime = 7f;
            }
            if (time >= 480)
            {
                Normal_SpawnTime = 0.5f;
                Bomb_SpawnTime = 5f;
            }
            if (time >= 600)
            {
                Armor_SpawnTime = 30f;

            }
            if (time >= 720)
            {
                Normal_SpawnTime = 0.3f;
                Bomb_SpawnTime = 3f;
            }
            if (time >= 840)
            {
                Normal_SpawnTime = 0.2f;
                Bomb_SpawnTime = 2f;
            }
            if (time >= 1440)
            {
                Normal_SpawnTime = 0.1f;
                Bomb_SpawnTime = 1f;
                Armor_SpawnTime = 10f;
            }
            if (time >= 2000)
            {
                Normal_SpawnTime = 0.02f;
                Bomb_SpawnTime = 0.5f;
                Armor_SpawnTime = 1f;
            }
        }

    }
    IEnumerator zombie_spawn()
    {
        while (true)
        {
            if (Pooling_Control.instance.Zombie_queue.Count != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Pooling_Control.instance.Zombie_queue.Count == 0)
                    {
                        break;
                    }
                    nor_xPos = Random.Range(-2, 2);
                    nor_zPos = Random.Range(-0.5f, 0.5f);
                    nor_RandomVector = new Vector3(nor_xPos, 0.0f, nor_zPos);
                    GameObject t_object = Pooling_Control.instance.GetQueue(100);
                    t_object.transform.position = this.gameObject.transform.position + nor_RandomVector;
                    //t_object.GetComponent<Enemy>().Spawn_effect.Play();
                }
            }

            yield return new WaitForSeconds(Zombie_SpawnTime);
        }

    }

    IEnumerator normal_spawn() ////일반 적을 스폰하는 코루틴 함수
    {
        while (true)
        {
            if (Pooling_Control.instance.Normal_queue.Count != 0)// Que안이 비어 있지 않을 때에 작업을 진행
            {
                
                for (int i = 0; i < 5; i++)// 한번에 5 개체 씩 꺼냄
                {
                    if (Pooling_Control.instance.Normal_queue.Count == 0)// Que안이 비어 있지 않을 때에 작업을 진행
                    {
                        break;
                    }
                    //스포너 위치에서 어느 정도 랜덤하게 스폰하게 해줌(겹침 현상 방지)
                    nor_xPos = Random.Range(-2, 2);
                    nor_zPos = Random.Range(-0.5f, 0.5f);
                    nor_RandomVector = new Vector3(nor_xPos, 0.0f, nor_zPos);
                    GameObject t_object = Pooling_Control.instance.GetQueue(1);
                    t_object.transform.position = this.gameObject.transform.position + nor_RandomVector;
                    t_object.GetComponent<Enemy>().Spawn_effect.Play();
                }
            }
            //각 적 종류마다의 스폰 시간간격에 맞게 스폰함
            yield return new WaitForSeconds(Normal_SpawnTime);
        }
        
     }
    IEnumerator Bomb_spawn()
    {
        yield return new WaitForSeconds(60f);
        while (true)
        {
            if (Pooling_Control.instance.Bomb_queue.Count != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Pooling_Control.instance.Bomb_queue.Count == 0)
                    {
                        break;
                    }
                    bomb_xPos = Random.Range(-2, 2);
                    bomb_zPos = Random.Range(-2, 2);
                    bomb_RandomVector = new Vector3(bomb_xPos, 0.0f, bomb_zPos);
                    GameObject te_object = Pooling_Control.instance.GetQueue(2);
                    te_object.transform.position = gameObject.transform.position + bomb_RandomVector;
                    te_object.GetComponent<Enemy>().Spawn_effect.Play();
                }
            }
            yield return new WaitForSeconds(Bomb_SpawnTime);
        }
    }
    IEnumerator Armor_spawn()
    {
        yield return new WaitForSeconds(180f);
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Pooling_Control.instance.Armor_queue.Count == 0)
                {
                    break;
                }
                if (Pooling_Control.instance.Armor_queue.Count != 0)
                {
                    armor_xPos = Random.Range(-2, 2);
                    armor_zPos = Random.Range(-2, 2);
                    armor_RandomVector = new Vector3(armor_xPos, 0.0f, armor_zPos);

                    GameObject tem_object = Pooling_Control.instance.GetQueue(3);
                    tem_object.transform.position = gameObject.transform.position + armor_RandomVector;
                    tem_object.GetComponent<Enemy>().Spawn_effect.Play();
                }
            }
            yield return new WaitForSeconds(Armor_SpawnTime);
            
        }
    }



}
