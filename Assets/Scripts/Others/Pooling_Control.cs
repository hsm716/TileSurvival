using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling_Control : MonoBehaviour
{
    public static Pooling_Control instance;
    public int type;
    public Queue<GameObject> Normal_queue = new Queue<GameObject>();
    public Queue<GameObject> Bomb_queue = new Queue<GameObject>();
    public Queue<GameObject> Armor_queue = new Queue<GameObject>();
    public Queue<GameObject> Zombie_queue = new Queue<GameObject>();
    public Queue<GameObject> Fly_queue = new Queue<GameObject>();
    public Queue<GameObject> bullet_queue = new Queue<GameObject>();
    public Queue<GameObject> aggroClone_queue = new Queue<GameObject>();
    public Queue<GameObject> Item_bomb_queue = new Queue<GameObject>();
    public Queue<GameObject> Item_hp_queue = new Queue<GameObject>();
    public Queue<GameObject> T_Ice_queue = new Queue<GameObject>();
    public Queue<GameObject> effect_StepOnTile_queue = new Queue<GameObject>();

    public List<Transform> targeting_queue = new List<Transform>();

    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject BombEnemyPrefab;
    [SerializeField]
    private GameObject ArmorEnemyPrefab;
    [SerializeField]
    private GameObject ZombieEnemyPrefab;
    [SerializeField]
    private GameObject FlyEnemyPrefab;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject aggroClonePrefab;

    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private GameObject hpPrefab;


    [SerializeField]
    private GameObject t_ice_Prefab;

    [SerializeField]
    private GameObject Effect_StepOnTile_Prefab;

    void Awake()
    {
        instance = this;
        if (type == 0)
        {
            for (int i = 0; i < 400; i++)
            {
                GameObject Enemy_Nor = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
                InsertQueue(Enemy_Nor, 1);
            }
            for (int i = 0; i < 200; i++)
            {
                GameObject Enemy_Bomb = Instantiate(BombEnemyPrefab, transform.position, Quaternion.identity);
                InsertQueue(Enemy_Bomb, 2);

            }
            for (int i = 0; i < 80; i++)
            {
                GameObject Enemy_Armor = Instantiate(ArmorEnemyPrefab, transform.position, Quaternion.identity);
                InsertQueue(Enemy_Armor, 3);
            }
            for (int i = 0; i < 100; i++)
            {
                GameObject Enemy_Fly = Instantiate(FlyEnemyPrefab, transform.position, Quaternion.identity);
                InsertQueue(Enemy_Fly, 4);
            }
            for (int i = 0; i < 50; i++)
            {
                GameObject go_hp = Instantiate(hpPrefab, transform.position, Quaternion.identity);
                InsertQueue(go_hp, 100);
            }


            

        }
        else if (type == 1)
        {
            for (int i = 0; i < 1500; i++)
            {
                GameObject Enemy_zombie = Instantiate(ZombieEnemyPrefab, transform.position, Quaternion.identity);
                InsertQueue(Enemy_zombie, 5);
            }
        }
        for (int i = 0; i < 500; i++)
        {
            GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            InsertQueue(go, 200);
        }
        for (int i = 0; i < 6; i++)
        {
            GameObject go = Instantiate(aggroClonePrefab, transform.position, Quaternion.identity);
            InsertQueue(go, 201);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject go_ice = Instantiate(t_ice_Prefab, transform.position, Quaternion.identity);
            InsertQueue(go_ice, 202);
        }

        for (int i = 0; i < 50; i++)
        {
            GameObject go_bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            InsertQueue(go_bomb, 101);
        }
        for (int i = 0; i < 20; i++)
        {
            GameObject effect_steponTile = Instantiate(Effect_StepOnTile_Prefab, transform.position, Quaternion.identity);
            effect_steponTile.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            InsertQueue(effect_steponTile, 300);

        }

    }
    private void Start()
    {
        targeting_queue.Add(Player.instance.tr);
    }
    public void InsertQueue(GameObject p_object, int mode)
    {

        //mode 1~99 까지는 Tile
        //mode 100~199까지는 아이템
        //mode 200~299까지는 공격 오브젝트
        //mode 300~399까지는 이펙트

        if (mode == 1)
        {
            Normal_queue.Enqueue(p_object);
        }
        else if (mode == 2)
        {
            Bomb_queue.Enqueue(p_object);
        }
        else if (mode == 3)
        {
            Armor_queue.Enqueue(p_object);
        }
        else if (mode == 4)
        {
            Fly_queue.Enqueue(p_object);
        }
        else if (mode == 5)
        {
            Zombie_queue.Enqueue(p_object);
        }
        
        else if (mode == 100)
        {
            Item_bomb_queue.Enqueue(p_object);
        }
        else if (mode == 101)
        {
            Item_hp_queue.Enqueue(p_object);
        }
        else if (mode == 200)
        {
            bullet_queue.Enqueue(p_object);
        }
        else if (mode == 201)
        {
            aggroClone_queue.Enqueue(p_object);
        }
        else if (mode == 202)
        {
            T_Ice_queue.Enqueue(p_object);
        }
        else if (mode == 300)
        {
            effect_StepOnTile_queue.Enqueue(p_object);
        }


        p_object.SetActive(false);
    }

    public GameObject GetQueue(int mode)
    {
        GameObject t_object = null;
        if (mode == 1)
        {
            t_object = Normal_queue.Dequeue();
           // t_object.SetActive(true);

        }
        else if (mode == 2)
        {
            t_object = Bomb_queue.Dequeue();
          //  t_object.SetActive(true);

        }
        else if (mode == 3)
        {
            t_object = Armor_queue.Dequeue();
           // t_object.SetActive(true);

        }
        else if (mode == 4)
        {
            t_object = Fly_queue.Dequeue();
            //  t_object.SetActive(true);
        }
        else if (mode == 5)
        {
            t_object = Zombie_queue.Dequeue();
            //  t_object.SetActive(true);
        }
        
        else if (mode == 100)
        {
            t_object = Item_bomb_queue.Dequeue();
           // t_object.SetActive(true);
        }
        else if (mode == 101)
        {
            t_object = Item_hp_queue.Dequeue();
           // t_object.SetActive(true);
        }

        else if (mode == 200)
        {
            t_object = bullet_queue.Dequeue();
        }
        else if (mode == 201)
        {
            t_object = aggroClone_queue.Dequeue();
            // t_object.SetActive(true);
        }
        else if (mode == 202)
        {
            t_object = T_Ice_queue.Dequeue();
            // t_object.SetActive(true);
        }
        else if (mode == 300)
        {
            t_object = effect_StepOnTile_queue.Dequeue();
        }





        return t_object;
    }
}
