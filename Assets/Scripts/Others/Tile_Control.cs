using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Control : MonoBehaviour
{
    public enum Type { normal,flame,dash,gun,aggro,ice};
    public Type myType;
    public MeshRenderer mr;
    Color[] colors = { Color.white, Color.red, new Color(0.127759f, 0.8428385f, 0.9339623f),Color.black, new Color(0.4433962f, 0.2677765f,0f), new Color(0.1f, 0.8f, 0.1f) };
    public Transform[] bulletShotPos;
    

    public int select_dir;

    bool isAct = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& !isAct)
        {
            isAct = true;
            switch (myType)
            {
                case Type.normal:
                    for(int i = 0; i < 8; i++)
                    {
                        GameObject bullet = Pooling_Control.instance.GetQueue(200);
                        bullet.transform.position = bulletShotPos[i].position;
                        bullet.transform.rotation = bulletShotPos[i].rotation;
                        bullet.SetActive(true);
                        bullet.GetComponent<Bullet>().LifeTime();
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
                    GameObject ac = Pooling_Control.instance.GetQueue(201);
                    ac.tag = "Aggro";
                    ac.transform.position = transform.position+new Vector3(0f,1.3f,0f);
                    ac.SetActive(true);
                    ac.GetComponent<Aggro_act>().StartAct(5f);
                    Pooling_Control.instance.targeting_queue.Add(ac.transform);
                    Transform tmp_tr = Pooling_Control.instance.targeting_queue[0];
                    Pooling_Control.instance.targeting_queue.Add(tmp_tr);
                    Pooling_Control.instance.targeting_queue.RemoveAt(0);
                    break;
                case Type.ice:
                    
                    GameObject io = Pooling_Control.instance.GetQueue(202);
                    io.transform.position = bulletShotPos[select_dir].position+new Vector3(0f,1f,0f);
                    io.transform.rotation = bulletShotPos[select_dir].rotation;
                    io.SetActive(true);
                    io.GetComponent<Ice_act>().Act();
                    
                    break;
            }
            GameObject effect_StepOnTile = Pooling_Control.instance.GetQueue(300);
            
            effect_StepOnTile.transform.position = transform.position + new Vector3(0f, 1f, 0f);
            effect_StepOnTile.SetActive(true);
            effect_StepOnTile.GetComponent<ParticleSystem>().Play();
            effect_StepOnTile.GetComponent<LifeCycle>().LifeTime();
            Invoke("FormChange", 0.1f);
        }
    }
    private void Update()
    {
        ShowDir();
    }
    void ShowDir()
    {

        switch (myType)
        {
            case Type.ice:
                for(int i = 0; i < 8; i++)
                {
                    if(i==select_dir)
                        bulletShotPos[i].transform.GetChild(0).gameObject.SetActive(true);
                    else
                    {
                        bulletShotPos[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                break;
            default:
                for (int i = 0; i < 8; i++)
                {
                    bulletShotPos[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                break;
        }
    }
   IEnumerator ChangeDir()
    {
        while (true)
        {
            select_dir += 1;
            if (select_dir >= 8)
            {
                select_dir = 0;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    void FormChange()
    {
        int r_idx = Random.Range(0, 9);
        float r_r = Random.Range(-5f, 5f);
        float r_c = Random.Range(-5f, 5f);
        float r_y = Random.Range(-0.01f, 0.01f);
        transform.position = GameManager.instance.Tile_SpawnSpots[r_idx].position + new Vector3(r_r, r_y, r_c);

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
        else if(rand_value >=11&& rand_value <= 15)
        {
            type_idx = 5;
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
            case 5:
                myType = Type.ice;
                StartCoroutine(ChangeDir());
                break;
        }
        mr.material.color = colors[type_idx];
        isAct = false;
    }
    
    
}
