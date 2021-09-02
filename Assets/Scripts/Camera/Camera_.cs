using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float offsetZ;

    [SerializeField]
    private float followSpeed;


    //카메라 shake about
    #region
    [SerializeField]
    private float ShakeAmount;
    private float ShakeTime;
   // Vector3 initialPosition;

    Vector3 cameraPosition;
    #endregion

    public void VibrateForTime(float time)
    {
        ShakeTime = time;
    }
   


    void Awake()
    {
     //   initialPosition = new Vector3(5f, 0f, 0f);
        cameraPosition = transform.position;

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.z = player.transform.position.z + offsetZ;
  

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -67.4f, 57.6f);

        cameraPosition.z = Mathf.Clamp(cameraPosition.z,-306.2f, -177.3f);



        transform.position = cameraPosition;

        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + cameraPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            //transform.position = initialPosition;
        }
        //transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.fixedDeltaTime);
    }
}
