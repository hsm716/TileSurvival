using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;
    public Queue<GameObject> Normal_queue = new Queue<GameObject>();
    public Queue<GameObject> Bomb_queue = new Queue<GameObject>();
    public Queue<GameObject> Armor_queue = new Queue<GameObject>();
    
    private float nor_xPos;
    private float nor_zPos;

    private float bomb_xPos;
    private float bomb_zPos;

    private float armor_xPos;
    private float armor_zPos;

    private Vector3 nor_RandomVector;
    private Vector3 bomb_RandomVector;
    private Vector3 armor_RandomVector;

    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject BombEnemyPrefab;
    [SerializeField]
    private GameObject ArmorEnemyPrefab;

    //public int stage = 2;
    float time = 0;
    [SerializeField]
    private float Normal_SpawnTime=2f;
    [SerializeField]
    private float Bomb_SpawnTime = 10f;
    [SerializeField]
    private float Armor_SpawnTime = 60f;
    
    void Awake()
    {
        instance = this;
        for(int i = 0; i < 400; i++)
        {
            GameObject Enenmy_Nor = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            Normal_queue.Enqueue(Enenmy_Nor);
            Enenmy_Nor.SetActive(false);
        }
        for(int i = 0; i < 200; i++)
        {
            GameObject Enemy_Bomb = Instantiate(BombEnemyPrefab, transform.position, Quaternion.identity);
            Bomb_queue.Enqueue(Enemy_Bomb);
            Enemy_Bomb.SetActive(false);
        }
        for (int i = 0; i < 80; i++)
        {
            GameObject Enemy_Armor = Instantiate(ArmorEnemyPrefab, transform.position, Quaternion.identity);
            Armor_queue.Enqueue(Enemy_Armor);
            Enemy_Armor.SetActive(false);
        }
        StartCoroutine(normal_spawn());
        StartCoroutine(Bomb_spawn());
        StartCoroutine(Armor_spawn());
    }
    public void InsertQueue(GameObject p_object,int mode)
    {
        if (mode == 1)
        {
            Normal_queue.Enqueue(p_object);
        }
        else if(mode == 2)
        {
            Bomb_queue.Enqueue(p_object);
        }
        else if(mode == 3)
        {
            Armor_queue.Enqueue(p_object);
        }
        
        p_object.SetActive(false);
    }
    public GameObject GetQueue(int mode)
    {
        GameObject t_object=null;
        if (mode == 1)
        {
            t_object = Normal_queue.Dequeue();
    
        }
        else if(mode==2)
        {
            t_object = Bomb_queue.Dequeue();
           
        }
        else if (mode == 3)
        {
            t_object = Armor_queue.Dequeue();
            
        }

        t_object.SetActive(true);

        return t_object;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 240)
        {
            Normal_SpawnTime = 1f;
        }
        if(time >= 360)
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
        if(time >= 720)
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

    IEnumerator normal_spawn()
    {
        while (true)
        {
            if (Normal_queue.Count != 0)
            {
                
                for (int i = 0; i < 5; i++)
                {
                    if (Normal_queue.Count == 0)
                    {
                        break;
                    }
                    nor_xPos = Random.Range(-2, 2);
                    nor_zPos = Random.Range(-0.5f, 0.5f);
                    nor_RandomVector = new Vector3(nor_xPos, 0.0f, nor_zPos);
                    GameObject t_object = GetQueue(1);
                    t_object.transform.position = this.gameObject.transform.position + nor_RandomVector;
                }
            }

            yield return new WaitForSeconds(Normal_SpawnTime);
        }
        
     }
    IEnumerator Bomb_spawn()
    {
        yield return new WaitForSeconds(60f);
        while (true)
        {
            if (Bomb_queue.Count != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Bomb_queue.Count == 0)
                    {
                        break;
                    }
                    bomb_xPos = Random.Range(-2, 2);
                    bomb_zPos = Random.Range(-2, 2);
                    bomb_RandomVector = new Vector3(bomb_xPos, 0.0f, bomb_zPos);
                    GameObject te_object = GetQueue(2);
                    te_object.transform.position = gameObject.transform.position + bomb_RandomVector;
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
                if (Armor_queue.Count == 0)
                {
                    break;
                }
                if (Armor_queue.Count != 0)
                {
                    armor_xPos = Random.Range(-2, 2);
                    armor_zPos = Random.Range(-2, 2);
                    armor_RandomVector = new Vector3(armor_xPos, 0.0f, armor_zPos);

                    GameObject tem_object = GetQueue(3);
                    tem_object.transform.position = gameObject.transform.position + armor_RandomVector;
                }
            }
            yield return new WaitForSeconds(Armor_SpawnTime);
            
        }
    }



}
