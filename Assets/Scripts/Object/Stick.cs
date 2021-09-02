﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    GameObject player;

    Player playerLogic;

    [SerializeField]
    private GameObject playerEquipPoint;

    private bool isPlayerEnter;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<Player>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            isPlayerEnter = true;
        
    }
 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerEnter)
        {
            transform.SetParent(playerEquipPoint.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = new Vector3(0.05f,0.2f, 0.05f);
            

//            playerLogic.Pickup(transform.gameObject);
            isPlayerEnter = false;
        }
    }
}
