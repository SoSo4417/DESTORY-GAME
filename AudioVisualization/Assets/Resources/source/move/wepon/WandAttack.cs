using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAttack : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

    private float rag = 0.0f;
    private bool Set;
    private int jc_ind = 0;
    private Vector3 CamP;
    private Quaternion CamR;
    private GameObject ExpInst;
    public float lifetime;
    public GameObject exprosion;
    public AudioSource SetSoundEffect;
    public AudioSource ExpSoundEffect;
    private ParticleSystem flare1;
    private ParticleSystem flare2;
    //private bool Charge;
    //public float Charge_Distance = 1.5f;

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
        flare1 = transform.Find("Wand01_el1/Flare1").gameObject.GetComponent<ParticleSystem>();
        flare2 = transform.Find("Wand01_el1/Flare1_sparks(world)").gameObject.GetComponent<ParticleSystem>();
        flare1.Stop();
        flare2.Stop();
        //Charge = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 秒数をカウント
        rag += Time.deltaTime;

        //// 距離チャージ
        //if (Charge == true)
        //{
        //    Charge_Distance += rag / 30;
        //}

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

        // 突き出し(JoyCon上ではx)
        float Z = accel.x;
        if (Z < -1.0 && Set == false && rag > 0.25f)
        {
            Setting();
            Set = true;
            rag = 0.0f;
        }
        if (Z > 1.0 && Set == true && rag > 0.25f)
        {
            Shooting();
            Set = false;
            rag = 0.0f;
            GetComponent<weapon_count>().Zweapon_count();
            if (Z > dataManager.Zaccel_max)
            {
                dataManager.Zaccel_max = Z;
            }
        }
    }

    void Setting()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("y", -0.2f, "z", 0.5f,
            "time", 0.1f, "easeType", "easeInOutSine", "isLocal", true));
        flare1.Play();
        flare2.Play();
        //Charge = true;

        SetSoundEffect.Play();
    }

    void Shooting()
    {
        iTween.MoveAdd(this.gameObject, iTween.Hash("z", -0.2f, "time", 0.1f,
            "easeType", "easeInOutBounce", "isLocal", true));
        flare1.Stop();
        flare2.Stop();

        Vector3 EPos = CamR * new Vector3(0f, 0f, 0.5f);
        var parent = this.transform;
        ExpInst = Instantiate(exprosion, this.transform.position + EPos, CamR) as GameObject;
        ExpInst.GetComponent<Flying>().Go();

        Destroy(ExpInst, lifetime);
        ExpInst.transform.parent = null;
        //Charge = false;
        //Charge_Distance = 1.5f;

        ExpSoundEffect.Play();
    }
}
