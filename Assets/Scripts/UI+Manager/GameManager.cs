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

    /*[SerializeField]
    private Text PP;*/

    public bool paused;
    public int point_;
    public float time;
    public int s;
    public int m;
    void Awake()
    {
        instance = this;
        playerLogic = player.GetComponent<Player>();
        time = 0f;
        s = 0;
        m = 0;
        point_ = 0;
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


  
}
