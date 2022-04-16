using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Control : MonoBehaviour
{
    public enum Type { normal,flame,dash,gun,aggro};
    public Type myType;
    public MeshRenderer mr;
    Color[] colors = { Color.white, Color.red, new Color(0.127759f, 0.8428385f, 0.9339623f),Color.black, new Color(0.4433962f, 0.2677765f,0f) };
    public Transform[] bulletShotPos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (myType)
            {
                case Type.normal:
                    for(int i = 0; i < 8; i++)
                    {
                        GameObject bullet = Pooling_Control.instance.GetQueue(4);
                        bullet.transform.position = bulletShotPos[i].position;
                        bullet.transform.rotation = bulletShotPos[i].rotation;
                        bullet.SetActive(true);
                        Rigidbody b_rgbd = bullet.GetComponent<Rigidbody>();
                        b_rgbd.velocity = Vector3.zero;
                        b_rgbd.AddForce(bullet.transform.forward * 65f, ForceMode.Impulse);
                    }
                    break;
                case Type.dash:
                    Player.instance.DashGage = 100;
                    break;
                case Type.flame:
                    Player.instance.FlameGage=100;
                    break;
                case Type.gun:
                    Player.instance.gunBulletGage = 100;
                    break;
                case Type.aggro:
                    GameObject ac = Pooling_Control.instance.GetQueue(5);
                    ac.tag = "Aggro";
                    ac.transform.position = transform.position+new Vector3(0f,1.3f,0f);
                    ac.GetComponent<Aggro_act>().StartAct(3f);
                    Pooling_Control.instance.targeting_queue.Add(ac.transform);
                    Transform tmp_tr = Pooling_Control.instance.targeting_queue[0];
                    Pooling_Control.instance.targeting_queue.Add(tmp_tr);
                    Pooling_Control.instance.targeting_queue.RemoveAt(0);
                    break;
            }
            Invoke("FormChange", 0.1f);
        }
    }
    void FormChange()
    {
        int r_idx = Random.Range(0, 9);
        int r_r = Random.Range(-5, 6);
        int r_c = Random.Range(-5, 6);
        transform.position = GameManager.instance.Tile_SpawnSpots[r_idx].position + new Vector3(r_r, 0f, r_c);

        int rand_value = Random.Range(1, 100);
        int type_idx = 0;
        if (rand_value >= 85 && rand_value <= 89)
        {
            type_idx = 1;
            //Instantiate(DashTile, transform.position, transform.rotation);
        }
        else if (rand_value >= 90 && rand_value <= 95)
        {
            type_idx = 2;
            //Instantiate(FlameTile, transform.position, transform.rotation);

        }
        else if (rand_value >= 4 && rand_value <= 10)
        {
            type_idx = 3;
            //Instantiate(GunTile, transform.position, Quaternion.LookRotation(new Vector3(0f, 0f, -1f)));

        }
        else if (rand_value >= 1 && rand_value <= 3)
        {
            type_idx = 4;
            //Instantiate(AggroTile, transform.position, transform.rotation);
           
        }
        else
        {
            type_idx = 0;
            
        }
        switch (type_idx)
        {
            case 0:
                myType = Type.normal;
                break;
            case 1:
                myType = Type.flame;
                break;
            case 2:
                myType = Type.dash;
                break;
            case 3:
                myType = Type.gun;
                break;
            case 4:
                myType = Type.aggro;
                break;
        }
        mr.material.color = colors[type_idx];
    }
}
