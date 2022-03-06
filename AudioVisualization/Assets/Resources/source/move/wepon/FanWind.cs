using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanWind : MonoBehaviour
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
    private GameObject WindInst;
    public float lifetime;
    public GameObject windeffect;
    public AudioSource SetSoundEffect;
    public AudioSource ShootSoundEffect;

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
        iTween.RotateTo(this.gameObject, iTween.Hash("x", -60f, "y", 0f, "z", 0f, "time", 0.35f,
            "easeType", "easeOutExpo", "isLocal", true));
        iTween.MoveTo(this.gameObject, iTween.Hash("x", 0.1f, "y", 0.1f, "z", 0.6f, "time", 0.35f,
            "easeType", "easeOutExpo", "isLocal", true));
        SetSoundEffect.Play();
    }

    void Swing()
    {
        iTween.RotateAdd(this.gameObject, iTween.Hash("x", 120f, "z", -50f, "time", 0.5f,
            "easeType", "easeInOutBack", "isLocal", true));
        iTween.MoveAdd(this.gameObject, iTween.Hash("y", -0.7f, "time", 0.5f,
            "easeType", "easeInOutBack", "isLocal", true));

        var parent = this.transform;
        WindInst = Instantiate(windeffect, this.transform.position, CamR, parent) as GameObject;
        WindInst.GetComponent<Flying>().Go();
        Destroy(WindInst, lifetime);

        // SE再生
        ShootSoundEffect.Play();
    }
}
