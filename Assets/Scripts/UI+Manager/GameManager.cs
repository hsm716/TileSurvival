using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject player;
    Player playerLogic;

    public Transform[] Tile_SpawnSpots;
    public Transform Tile_SpawnList;
    /*[SerializeField]
    private Text PP;*/

    public bool paused;
    public int point_;
    public float time;
    public int s;
    public int m;

    public GameObject Tile_prefab;
    public int[,] TileGraph;
    public int Tile_Count;
    public Transform Tile_parent;
    void Awake()
    {
       
        instance = this;
        TileGraph = new int[30, 30];
        playerLogic = player.GetComponent<Player>();
        Tile_Count = 20;
        time = 0f;
        s = 0;
        m = 0;
        point_ = 0;
    }
    private void Start()
    {
        for(int i = 0; i < 30; i++)
        {
            for(int j = 0; j < 30; j++)
            {
                TileGraph[i, j] = 0;
            }
        }
        int tc = Tile_Count;
        for(int i = 0; i < tc; i++)
        {
            int rand_index = Random.Range(0, 900);
            int r_ = rand_index / 30;
            int c_ = rand_index % 30;
            if (TileGraph[r_, c_] >= 1)
            {
                tc++;
                continue;
            }
            TileGraph[r_, c_]=1;
            GameObject go = Instantiate(Tile_prefab, transform.position, transform.rotation);
            //go.transform.parent = Tile_parent;
            go.GetComponent<Tile_Control>().r_index = r_;
            go.GetComponent<Tile_Control>().c_index = c_;
            go.transform.position = Tile_SpawnList.GetChild(rand_index).position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        m = ((int)time % 3600) / 60;
        s = ((int)time % 3600) % 60;


    }
    public void GetPoint(int amount)
    {
        point_ += amount;
    }
    void AddTile(int cnt)
    {
        int count = cnt;
        for (int i = 0; i < count; i++)
        {
            int rand_index = Random.Range(0, 900);
            int r_ = rand_index / 30;
            int c_ = rand_index % 30;
            if (TileGraph[r_, c_] >= 1)
            {
                count++;
                continue;
            }
            TileGraph[r_, c_]++;
            GameObject go = Instantiate(Tile_prefab, transform.position, transform.rotation);
            go.transform.parent = Tile_parent;
            go.GetComponent<Tile_Control>().r_index = r_;
            go.GetComponent<Tile_Control>().c_index = c_;
            go.transform.position = Tile_SpawnList.GetChild(rand_index).position;
        }
    }


  
}
