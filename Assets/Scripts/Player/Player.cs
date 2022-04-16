using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Transform tr;
    private Rigidbody rgbd;
    private float h;
    private float v;

    private bool isGun;
    private bool isDashing;

    public bool ShotOnoff = false;
    public float gunBulletGage;
    public float Player_HP;
    public float DashGage;
    public float FlameGage;


    //피격시 혈흔UI
    [SerializeField]
    private Image bloodScreen;
    [SerializeField]
    private Image bloodScreen_back;

    //피격시 사운드
    [SerializeField]
    private AudioSource uck;

    //플레이어 조이스틱
    [SerializeField]
    private bl_Joystick joystick;

    //죽을 시 UI
    [SerializeField]
    private GameObject Finish_UI;

    //죽을 시 토탈 스코어 및 버틴 시간
    [SerializeField]
    private Text Total_socre;
    [SerializeField]
    private Text Time_;

    //플레이어 화염 방사기
    [SerializeField]
    private GameObject FlameObj;

    //플레이어 주변 공격링
    [SerializeField]
    private GameObject DashObj;

    //플레이어 총
    [SerializeField]
    private GameObject GunObj;


    //플레이어 이동속도 or 대쉬속도 or 회전 속도
    [SerializeField]
    private float moveSpeed = 10f;
    private float runSpeed;
    [SerializeField]
    private float rotateSpeed;
    private Animator animator;
    private Vector3 movement;

    Vector2 pos;
    public Vector3 ShotPoint;
    private Vector3 mpTemp;
    bool TurnShotOn = false;

    public LayerMask whatIsEnemy;
    Collider[] enemies;

    void Awake()
    {
        instance = this;
        tr = this.transform;
        animator = GetComponent<Animator>();
        bloodScreen.gameObject.SetActive(false);
        movement = Vector3.zero;

        rgbd = GetComponent<Rigidbody>();
        runSpeed = moveSpeed;
        Player_HP = 100f;
        DashGage = 0f;
        FlameGage = 0f;
        gunBulletGage = 0;
        Finish_UI.SetActive(false);


    }
    IEnumerator ShowBloodScreen()
    {
        bloodScreen.gameObject.SetActive(true);
        bloodScreen_back.gameObject.SetActive(true);

        bloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.2f, 0.3f));
        bloodScreen_back.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.2f);
        bloodScreen.color = Color.clear;
        bloodScreen_back.color = Color.clear;

    }


    private void FixedUpdate()
    {


        if (TurnShotOn)
        {
            TurnShot();
        }
        else
        {
            Turn();
        }
        Run();
        Dash();
    }

    void SetStat()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate { StatisticName = "Score", Value =GameManager.instance.point_}, //스코어 필드에 현재 점수 정보를 업데이트함 (서버 내에서 본인의 기록된 점수보다 높아야 갱신되는 것으로 설정 되있음)
                    new StatisticUpdate { StatisticName = "Time_m",Value=GameManager.instance.m},
                    new StatisticUpdate { StatisticName = "Time_s",Value=GameManager.instance.s}
                }
        },
           (result) => {; },
           (error) => {; }

           );
    }
    void pauseNow()
    {
        Time.timeScale = 0;
    }

    void GunMode()
    {
        enemies = Physics.OverlapSphere(transform.position, 50f, whatIsEnemy);
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float dist = Mathf.Infinity;
        Vector3 nearObj = Vector3.zero;
        foreach (var enemy in enemies)
        {
            float dist_cost = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist > dist_cost)
            {
                dist = dist_cost;
                nearObj = enemy.transform.position;
            }
        }
        animator.transform.forward = nearObj - transform.position;
    }
    void Update()
    {
        rgbd.velocity = new Vector3(0, 0, 0);
        h = joystick.Horizontal;
        v = joystick.Vertical;

        //HP
        if (Player_HP >= 100)
        {
            Player_HP = 100;
        }

        //Gun
        if (gunBulletGage > 0)
        {
            GunMode();
            TurnShotOn = true;
            GunObj.SetActive(true);
            animator.SetBool("isGun", true);
        }
        else
        {
            TurnShotOn = false;
            GunObj.SetActive(false);
            animator.SetBool("isGun", false);
        }

        //Dash
        if (DashGage > 0)
        {
            DashObj.SetActive(true);
            isDashing = true;
        }
        else if (DashGage <= 0)
        {
            DashObj.SetActive(false);
        }

        //Flame
        if (FlameGage > 0)
        {
            FlameObj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            //FlameObj.SetActive(true);

        }
        else if (FlameGage <= 0)
        {

            FlameObj.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            //   FlameObj.SetActive(false);
        }

    }
    public void TakeDamage(float dmg)
    {
        Player_HP -= dmg;
        if (Player_HP <= 0)
        {
            pauseNow();
            
            SetStat();
            Total_socre.text = "Total_socre : " + GameManager.instance.point_;
            Time_.text = "Time  \t\t\t : " + GameManager.instance.m + "m " + GameManager.instance.s + "s";
            Finish_UI.SetActive(true);
        }
    }

    void Run()
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rgbd.MovePosition(transform.position + movement);
    }
    void Dash()
    {
        if (!isDashing)
        {
            moveSpeed = runSpeed;
            return;
        }
        
        moveSpeed = 40f;
        isDashing = false;
    }
    void Turn()
    {
        if (h == 0 && v == 0) //이동 방향 정보가 없으면 아무 반응 없이 끝 마침
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement); //이동 방향 정보에 맞게 새로운 Rotation정보를 갱신한다.
        rgbd.rotation = Quaternion.Slerp(rgbd.rotation, newRotation, rotateSpeed * Time.deltaTime); 
        //플레이어의 현재 회전 방향을 새로운 회전 방향으로 갱신 시키는데, Slerp(구면선형보간)함수를 사용하여 부드럽게 회전하는 느낌을 준다.
    }
    void TurnShot()
    {
        
        
        Quaternion newRotation = Quaternion.LookRotation(ShotPoint);
        rgbd.rotation = Quaternion.Slerp(rgbd.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            uck.Play();
            StartCoroutine(ShowBloodScreen());
        }
    }
}
