using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

    private float rag = 0.0f;
    private bool Set;
    public int jc_ind = 0;
    private Vector3 CamP;
    private Quaternion CamR;
    public AudioSource SoundEffect;
    public AudioSource SetSE;
    //public Vector3[] vecs;

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
        {
            CamP = this.transform.parent.position;
            CamR = this.transform.parent.rotation;
        }

        // ジョイコンは一個に限定
        Joycon j = joycons[0];

        // 加速度を得る
        accel = j.GetAccel();

        // 横振り(JoyCon上ではy)
        float X = accel.y;
        if (X > 1.0 && Set == false && rag > 1.50f)
        {
            // ->こっち向きでセット(ぐるぐる回す)
            Setting();
            Set = true;
            rag = 0.0f;
        }
        if (X < -1.0 && Set == true && rag > 0.55f)
        {
            // <-こっち向きに振る
            Swing();
            SoundEffect.Play();
            Set = false;
            rag = 0.0f;
            GetComponent<weapon_count>().Xweapon_count();
            if (-X > dataManager.Xaccel_max)
            { // 負なので*-1
                dataManager.Xaccel_max = -X;
            }
        }
    }

    void Setting()
    {
        iTween.Stop(this.gameObject, "rotate");
        iTween.MoveTo(this.gameObject, iTween.Hash("x", -0.25f, "y", -0.2f, "z", 0.4f,
            "time", 0.5f, "isLocal", true));
        iTween.RotateTo(this.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f,
            "easeType", "easeInOutBack", "time", 0.5f, "isLocal", true));

        SetSE.Play();
    }

    void Swing()
    {
        Collider cl = null;
        if (this.GetComponent<Collider>())
            cl = this.GetComponent<Collider>();
        else if (this.transform.GetChild(0).GetComponent<Collider>())
            cl = this.transform.GetChild(0).GetComponent<Collider>();

        cl.isTrigger = false;

        iTween.RotateAdd(this.gameObject, iTween.Hash("y", 180f,
            "time", 0.1f, "loopType", "loop", "isLocal", true));

        Vector3 tgt = new Vector3(0.0f, 0.2f, 8.0f);
        iTween.MoveTo(this.gameObject, iTween.Hash("position", tgt,
            "time", 2.0f, "easyType", "easeOutQuad", "isLocal", true));

        iTween.MoveTo(this.gameObject, iTween.Hash("position", this.transform.position,
            "delay", 0.5f));

        iTween.MoveTo(this.gameObject, iTween.Hash("position", CamP + new Vector3(0f, -0.2f, 0.4f),
            "time", 2.0f, "delay", 1.5f, "easyType", "easeOutExpo", "isLocal", false));

        //Vector3[] path = new Vector3[5];
        //path[0] = CamR * vecs[0];
        //path[1] = CamR * vecs[1];
        //path[2] = CamR * vecs[2];
        //path[3] = CamR * vecs[3];
        //path[4] = CamR * vecs[4];
        //iTween.MoveTo(this.gameObject, iTween.Hash("path", path,
        //"time", 1.5f, "easyType", "easeOutExpo", "isLocal", true));
        //iTween.MoveTo(this.gameObject, iTween.Hash("x", 0.0f, "y", -0.2f, "z", 0.4f,
        //    "time", 1.0f, "delay", 1.5f, "easyType", "easeInOutExpo", "isLocal", true));

        //Vector3 Pos1 = CamR * new Vector3(-0.2f, -0.1f, 0.5f);
        //Vector3 Pos2 = CamR * new Vector3(0.2f, -0.15f, 1.0f);
        //Vector3 Pos3 = CamR * new Vector3(0.0f, 0.2f, 0.4f);

        //iTween.MoveTo(this.gameObject, iTween.Hash("position", Pos1,
        //    "time", 1.0f));
        //iTween.MoveTo(this.gameObject, iTween.Hash("position", Pos2,
        //    "time", 1.0f));
        //iTween.MoveTo(this.gameObject, iTween.Hash("position", Pos3,
        //    "time", 1.0f));

        StartCoroutine(OnKinematic(cl, 1.0f));
    }

    IEnumerator OnKinematic(Collider cl, float delay)
    {
        yield return new WaitForSeconds(delay);
        cl.isTrigger = true;
    }
}
