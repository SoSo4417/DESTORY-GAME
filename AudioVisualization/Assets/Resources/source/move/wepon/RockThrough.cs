using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrough : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

    private bool P = false;
    private float rag = 0.0f;
    private float rot_g;
    public int jc_ind = 0;
    private Vector3 firstP;

    // Start is called before the first frame update
    void Start()
    {
        gyro = new Vector3(0.0f, 0.0f, 0.0f);
        accel = new Vector3(0.0f, 0.0f, 0.0f);
        iTween.Init(this.gameObject);
        firstP = this.transform.position;
        this.transform.Rotate(-170f, 0f, 0f);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 秒数をカウント
        rag += Time.deltaTime;

        // ジョイコンは一個に限定
        Joycon j = joycons[0];

        // 現在のジャイロを取得
        gyro = j.GetGyro();
        rot_g = gyro.y * -0.5f;

        this.transform.Rotate(rot_g, 0.0f, 0.0f);
        if (P == false)
            this.transform.position = firstP;

        // 加速度を得る
        accel = j.GetAccel();

        // 縦振り(Joycon上ではz)
        float Y = accel.z;
        if (Y > 1.50 && P == false && rag > 0.55f)
        {
            ThroughOut();
            P = true;
            rag = 0.0f;
            GetComponent<weapon_count>().Yweapon_count();
        }
        if (Y < -1.00 && P == true && rag > 2.0f)
        {
            RockSet();
            P = false;
            rag = 0.0f;
        }
    }

    void ThroughOut()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        float forceMagnitude = 5f;
        Vector3 force = forceMagnitude * new Vector3(0.0f, -rot_g, 1.0f);
        rb.AddForce(force, ForceMode.Impulse);
        print("through");
    }
    void RockSet()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("x", firstP.x, "y", firstP.y, "z", firstP.z,
            "time", 0.001f));
        print("set");
    }
}
