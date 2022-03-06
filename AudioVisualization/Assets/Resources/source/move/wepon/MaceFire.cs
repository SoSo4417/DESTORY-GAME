using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceFire : MonoBehaviour
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
    private GameObject FireInst;
    public float lifetime;
    public GameObject fireball;
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

        // 突き出し(JoyCon上ではx)
        float Z = accel.y;
        if (Z < -1.00 && Set == false && rag > 0.25f)
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
        iTween.MoveTo(this.gameObject, iTween.Hash("y", -0.2f, "z", 0.5f, "time", 0.1f, "easeType", "easeInOutSine", "isLocal", true));
        //Vector3 FIrePos = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z + 0.3f);
        Vector3 FirePos = CamR * new Vector3(0f, 0.3f, 0.3f);

        var parent = this.transform;
        FireInst = Instantiate(fireball, this.transform.position + FirePos, Quaternion.identity, parent) as GameObject;
        FireInst.GetComponent<Flying>().Set();

        SetSoundEffect.Play();
    }

    void Shooting()
    {
        FireInst.transform.rotation = CamR;
        FireInst.GetComponent<Flying>().Go();
        Destroy(FireInst, lifetime);
        iTween.MoveAdd(this.gameObject, iTween.Hash("z", -0.2f, "time", 0.1f, "easeType", "easeInOutBounce", "isLocal", true));
        ShootSoundEffect.Play();
    }
}
