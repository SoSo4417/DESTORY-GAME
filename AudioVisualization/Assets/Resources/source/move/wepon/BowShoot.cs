using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShoot : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

    private float rag = 0.0f;
    private bool Set;
    private int jc_ind = 0;
    private Vector3 CamP;
    private GameObject ArrowInst;
    public float lifetime;
    public GameObject Arrow;
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
            CamP = this.transform.parent.position;

        // 加速度を得る
        accel = j.GetAccel();

        // 突き出し(JoyCon上ではx)
        float Z = accel.x;
        if (Z < -1.0 && Set == false && rag > 0.55f)
        {
            Setting();
            Set = true;
            rag = 0.0f;
        }
        if (Z > 1.0 && Set == true && rag > 0.95f)
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
        // 弦
        GameObject chord = transform.Find("Bow04_joint_base/chord").gameObject;
        iTween.Init(chord);
        iTween.MoveAdd(chord, iTween.Hash("x", -0.5f, "time", 0.20f, "easeType", "easeInOutCubic", "isLocal", true));
        iTween.MoveAdd(this.gameObject, iTween.Hash("z", 0.05f, "time", 1.0f, "easeType", "easeInOutBounce"));

        // 矢
        Vector3 ArrowPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.9f);
        var parent = this.transform;
        Vector3 ang = parent.parent.eulerAngles;
        ArrowInst = Instantiate(Arrow, this.transform.position, Quaternion.Euler(ang.x + 180f, ang.y + 0f, ang.z + 90f), parent) as GameObject;
        iTween.MoveTo(ArrowInst, iTween.Hash("x", -1f, "isLocal", true));
        iTween.MoveAdd(ArrowInst, iTween.Hash("z", 0.05f, "time", 1.0f, "easeType", "easeInOutBounce",
            "isLocal", true));
        SetSoundEffect.Play();
    }

    void Shooting()
    {
        // 弦
        GameObject chord = transform.Find("Bow04_joint_base/chord").gameObject;
        iTween.Init(chord);
        iTween.MoveTo(chord, iTween.Hash("x", -0.226329, "y", 0f, "z", 0f, "time", 0.01f, "easeType", "easeInOutBounce", "isLocal", true));

        //　矢
        Rigidbody arwrb = ArrowInst.GetComponent<Rigidbody>();
        arwrb.isKinematic = false;
        arwrb.AddForce(30 * -ArrowInst.transform.forward, ForceMode.Impulse);
        arwrb.useGravity = true;
        ArrowInst.transform.parent = null;
        Destroy(ArrowInst, lifetime);
        iTween.MoveAdd(this.gameObject, iTween.Hash("z", -0.05f, "time", 0.1f, "easeType", "easeInOutBounce"));
        ShootSoundEffect.Play();
    }
}
