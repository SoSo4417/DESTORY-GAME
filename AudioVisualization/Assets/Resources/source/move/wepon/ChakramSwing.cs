using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakramSwing : MonoBehaviour
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
    public float lifetime;
    public GameObject zangeki;

    public AudioSource SetSE;
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
        if (X > 1.0 && Set == false && rag > 0.55f)
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
        iTween.MoveTo(this.gameObject, iTween.Hash("x", 0.3f, "y", -0.18f, "z", 0.8f,
            "easeType", "easeInOutBack", "time", 0.5f, "isLocal", true));

        iTween.RotateTo(this.gameObject, iTween.Hash("x", 90f, "y", 30f, "z", 0f,
            "time", 0.5f, "isLocal", true));

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

        for (int i = 0; i < 5; i++)
        {
            float delay = i * 0.005f;
            iTween.MoveAdd(this.gameObject, iTween.Hash("x", -0.25f,
                "time", 0.005f, "delay", delay, "isLocal", true));
            iTween.RotateAdd(this.gameObject, iTween.Hash("z", 20f,
                "time", 0.005f, "delay", delay, "isLocal", true));
        }

        // 飛ぶ斬撃
        GameObject zngk = Instantiate(zangeki, this.transform.position, this.transform.parent.rotation) as GameObject;
        Destroy(zngk, lifetime);// 飛ばしたのは寿命がある
        zngk.GetComponent<Flying>().Go();

        StartCoroutine(OnKinematic(cl, 0.5f));
    }

    IEnumerator OnKinematic(Collider cl, float delay)
    {
        yield return new WaitForSeconds(delay);
        cl.isTrigger = true;
    }
}
