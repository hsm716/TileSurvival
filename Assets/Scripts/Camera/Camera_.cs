using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ : MonoBehaviour
{
    public static Camera_ instance;
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
        instance = this;
        
        cameraPosition = transform.position;

        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.z = player.transform.position.z + offsetZ;
  

/*        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -54.80301f, 54.40301f);

        cameraPosition.z = Mathf.Clamp(cameraPosition.z, -38.74083f, 38.74083f);
*/


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
