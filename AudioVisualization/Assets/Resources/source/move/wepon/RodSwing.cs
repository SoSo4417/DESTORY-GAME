using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodSwing : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public GameObject fireball;

    private bool R = false;
    private float rag = 0.0f;
    private int jc_ind = 0;

    // Start is called before the first frame update
    void Start()
    {
        gyro = new Vector3(0.0f, 0.0f, 0.0f);
        accel = new Vector3(0.0f, 0.0f, 0.0f);
        iTween.Init(this.gameObject);

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
        float rot_g = gyro.y * -0.5f;

        this.transform.Rotate(rot_g, 0.0f, 0.0f);

        // 加速度を得る
        accel = j.GetAccel();

        // 横振り(Joycon上ではy)
        float X = accel.y;
        if (X > 0.25 && R == false && rag > 0.55f)
        {
            SwingR();
            R = true;
            rag = 0.0f;
            GetComponent<weapon_count>().Xweapon_count();
        }
        if (X < -0.85 && R == true && rag > 0.55f)
        {
            SwingL();
            R = false;
            rag = 0.0f;
        }

    }

    void SwingR()
    {
        iTween.RotateTo(this.gameObject, iTween.Hash("x", 0.0f, "y", 60.0f, "time", 0.5f, "easeType", "easeInOutBack",
            "isLocal", true));
    }
    void SwingL()
    {
        iTween.RotateTo(this.gameObject, iTween.Hash("y", -60.0f, "time", 0.5f, "easeType", "easeInOutBack",
            "isLocal", true));

        // 火球を飛ばす
        fire();
    }

    void fire()
    {
        Instantiate(fireball, this.transform.position, 
            Quaternion.Euler(this.transform.eulerAngles.x, this.transform.parent.eulerAngles.y, this.transform.parent.eulerAngles.z));
        //ThroughEffect.transform.Rotate(rot_x, 0.0f, 0.0f);
    }
}
