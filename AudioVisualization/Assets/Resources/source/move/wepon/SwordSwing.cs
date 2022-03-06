using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

    private float rag = 0.0f;
    private bool Set;
    private int jc_ind = 0;
    private ParticleSystem flare1;
    private Vector3 CamP;
    private Quaternion CamR;
    public float lifetime;
    public GameObject darkeffect;
    public AudioSource SoundEffect;
    public AudioSource SetSE;

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
        flare1 = transform.Find("Sword10_el1/Flare1").gameObject.GetComponent<ParticleSystem>();
        flare1.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // 秒数をカウント
        rag += Time.deltaTime;

        // ジョイコンは一個に限定
        Joycon j = joycons[0];

        // カメラ位置を更新
        if (this.transform.parent != null)
        {
            CamP = this.transform.parent.position;
            CamR = this.transform.parent.rotation;
        }

        // 加速度を得る
        accel = j.GetAccel();

        // 縦振り(JoyCon上ではz)
        float Y = accel.z;
        if (Y < -1.0 && Set == false && rag > 0.55f)
        {
            Setting();
            Set = true;
            rag = 0.0f;
            GetComponent<weapon_count>().Yweapon_count();
        }
        if (Y > 1.0 && Set == true && rag > 0.55f)
        {
            Swing();
            Set = false;
            rag = 0.0f;
            if (Y > dataManager.Yaccel_max)
            {
                dataManager.Yaccel_max = Y;
            }
        }
    }

    void Setting()
    {
        flare1.Play();

        iTween.MoveTo(this.gameObject, iTween.Hash("x", 0.15f, "y", 0f, "z", 0.8f, "time", 0.35f,
    "easeType", "easeOutExpo", "isLocal", true));
        iTween.RotateTo(this.gameObject, iTween.Hash("x", 60f, "y", 0f, "z", 100f, "time", 0.35f,
            "easeType", "easeOutExpo", "isLocal", true));

        SetSE.Play();
    }

    void Swing()
    {
        flare1.Stop();

        Collider cl = null;
        if (this.GetComponent<Collider>())
            cl = this.GetComponent<Collider>();
        else if (this.transform.GetChild(0).GetComponent<Collider>())
            cl = this.transform.GetChild(0).GetComponent<Collider>();

        cl.isTrigger = false;
        iTween.MoveAdd(this.gameObject, iTween.Hash("x", 0.5f, "z", 0.5f, "time", 0.45f,
            "easeType", "easeInOutBack", "isLocal", true));
        iTween.RotateAdd(this.gameObject, iTween.Hash("y", -150.0f, "time", 0.5f,
            "easeType", "easeInOutBack", "isLocal", true));
        StartCoroutine(OnKinematic(cl, 0.5f));
        // SE再生
        SoundEffect.Play();

        Vector3 EPos = CamR * new Vector3(0f, 0f, 1f);
        GameObject Dark = Instantiate(darkeffect, this.transform.position + EPos, CamR) as GameObject;
        Dark.GetComponent<Flying>().Go();
        Destroy(Dark, lifetime);
    }

    IEnumerator OnKinematic(Collider cl, float delay)
    {
        yield return new WaitForSeconds(delay);
        cl.isTrigger = true;
    }

}
