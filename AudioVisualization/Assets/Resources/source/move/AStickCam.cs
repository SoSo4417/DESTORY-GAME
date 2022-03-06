using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStickCam : MonoBehaviour
{
    private Transform transform;
    public float moveSpeed = 5f;
    public float rotateSpeed = 50f;

    // 
    private List<Joycon> joycons;
    public float[] stick;
    public int jc_ind = 0;

    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;


    void Start()
    {
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
        iTween.Init(this.gameObject);
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Joycon j = joycons[0];
        stick = j.GetStick();

        // カメラリセット
        if (j.GetButtonDown(Joycon.Button.STICK))
        {
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }
        // 後ろを向く
        if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            iTween.RotateAdd(this.gameObject, iTween.Hash("y", 180.0f, "time", 0.5f,
            "easeType", "easeOutExpo", "isLocal", true));
        }

        rotateCmaeraAngle();

        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        float angle_y = transform.eulerAngles.y;
        angle_y += stick[0];

        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
            angle_y, //transform.eulerAngles.y,
            transform.eulerAngles.z
        );
    }

    private void rotateCmaeraAngle()
    {
        Vector3 angle = new Vector3(
            stick[0] * rotateSpeed * Time.deltaTime ,    // 左右
            -stick[1] * rotateSpeed * Time.deltaTime,    // 上下
            0
        );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }


    /*
    void Update()
    {
        Joycon j = joycons[0];
        stick = j.GetStick();
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * stick[0]);
        transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime * stick[1]);

        // カメラリセット
        if (j.GetButtonDown(Joycon.Button.STICK))
        {
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }

        // 後ろを向く
        if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            iTween.RotateAdd(this.gameObject, iTween.Hash("y", 180.0f, "time", 0.5f,
            "easeType", "easeOutExpo", "isLocal", true));
        }

        // //old..Wu
        //float adValue = Input.GetAxis("Horizontal");
        //float wsValue = Input.GetAxis("Vertical");
        //float mValue = Input.GetAxis("Mouse X");

        //var moveDirection = (Vector3.forward * wsValue) + (Vector3.right * adValue);
        //transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.Self);
        //transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * mValue);
    }
    */

}