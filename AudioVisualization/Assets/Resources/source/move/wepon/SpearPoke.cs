using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPoke : MonoBehaviour
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
    public AudioSource SetSoundEffect;
    public AudioSource PokeSoundEffect;
    public GameObject fire;
    public float lifetime;

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
        float Z = accel.x;
        if (Z < -1.00 && Set == false && rag > 0.25f)
        {
            Setting();
            Set = true;
            rag = 0.0f;
        }
        if (Z > 1.0 && Set == true && rag > 0.25f)
        {
            Poking();
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
        //iTween.RotateTo(this.gameObject, iTween.Hash("x", 180f, "time", 0.1f, "easeType", "easeInOutSine", "isLocal", true));
        iTween.MoveTo(this.gameObject, iTween.Hash("x", 0.2f, "y", -0.2f, "z", -0.5f,
            "time", 0.1f, "easeType", "easeInOutSine", "isLocal", true));
        iTween.RotateTo(this.gameObject, iTween.Hash("x", 180f, "y", 0f, "z", 0f,
    "time", 0.1f, "easeType", "easeInOutSine", "isLocal", true));
        SetSoundEffect.Play();
    }

    void Poking()
    {
        Collider cl = null;
        if (this.GetComponent<Collider>())
            cl = this.GetComponent<Collider>();
        else if (this.transform.GetChild(0).GetComponent<Collider>())
            cl = this.transform.GetChild(0).GetComponent<Collider>();

        cl.isTrigger = false;
        iTween.MoveAdd(this.gameObject, iTween.Hash("z", -2.5f, "time", 0.1f, "easeType", "easeInOutBounce", "isLocal", true));
        PokeSoundEffect.Play();
        StartCoroutine(OnKinematic(cl, 0.1f));

        // 射出用の生成
        Vector3 fpos = this.transform.GetChild(0).position;
        GameObject Fire = Instantiate(fire, fpos, this.transform.parent.rotation) as GameObject;

        Fire.GetComponent<Flying>().Go();
        Destroy(Fire, lifetime);
    }

    IEnumerator OnKinematic(Collider cl, float delay)
    {
        yield return new WaitForSeconds(delay);
        cl.isTrigger = true;
    }
}
