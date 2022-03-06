using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syuriken : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

    public GameObject throwing;
    private float rag = 0.0f;
    private bool Set;
    public int jc_ind = 0;
    private Vector3 CamP;
    public float lifetime;
    public AudioSource SoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        gyro = new Vector3(0.0f, 0.0f, 0.0f);
        accel = new Vector3(0.0f, 0.0f, 0.0f);
        Set = false;
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

        // カメラ位置を更新
        if (this.transform.parent != null)
            CamP = this.transform.parent.position;

        // ジョイコンは一個に限定
        Joycon j = joycons[0];

        // 加速度を得る
        accel = j.GetAccel();

        // 横振り：joycon上でｙ
        float X = accel.y;
        if (X < -1.0 && Set == false && rag > 0.55f)
        {
            // ←こっち向きで着火・セット
            SetL();
            Set = true;
            rag = 0.0f;
        }
        if (X > 1.0 && Set == true && rag > 0.55f)
        {
            // →こっち向きに投げる
            throwR();
            Set = false;
            rag = 0.0f;
            GetComponent<weapon_count>().Xweapon_count();
            if (X > dataManager.Xaccel_max)
            {
                dataManager.Xaccel_max = X;
            }
        }
    }
    void throwR()
    {
        // 射出用の生成
        //GameObject thr = Instantiate(throwing, this.transform.position, Quaternion.identity) as GameObject;
        GameObject thr = Instantiate(throwing, this.transform.position, this.transform.parent.rotation) as GameObject;

        // 射出
        Rigidbody rb = thr.GetComponent<Rigidbody>();
        float forceMagnitude = 15f;
        Vector3 force = forceMagnitude * this.transform.forward;
        rb.velocity = force;
        rb.useGravity = true;
        Destroy(thr, lifetime);// 飛ばしたのは寿命がある

        // SE再生
        SoundEffect.Play();

        // 戻す
        Vector3 sp = this.GetComponent<Setting>().GetPos();
        iTween.MoveTo(this.gameObject, iTween.Hash("x", sp.x, "y", sp.y, "z", sp.z, "time", 0.5f, "isLocal", true));
    }

    void SetL()
    {
        iTween.MoveAdd(this.gameObject, iTween.Hash("x", -0.25f, "z", -0.01f, "easeType", "easeInOutBack", "time", 0.5f,
            "isLocal", true));
    }
}
