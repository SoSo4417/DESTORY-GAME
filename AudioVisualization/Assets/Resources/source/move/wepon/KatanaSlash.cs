using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSlash : MonoBehaviour
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
    public float lifetime;
    public GameObject zangeki;

    public AudioSource SetSoundEffect;
    public AudioSource SlashSoundEffect;

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

        // 横振り(Joycon上ではy)
        float X = accel.y;
        if (X < -1.00 && Set == false && rag > 0.55f)
        {
            Setting();
            Set = true;
            rag = 0.0f;
        }
        if (X > 1.0 && Set == true && rag > 0.85f)
        {
            Slashing();
            Set = false;
            rag = 0.0f;
            GetComponent<weapon_count>().Xweapon_count();
            if (X > dataManager.Xaccel_max)
            {
                dataManager.Xaccel_max = X;
            }
        }

        // A button
        if (j.GetButtonDown(Joycon.Button.PLUS))
        {
            print(j.GetAccel());
        }
    }

    void Setting()
    {
        iTween.RotateTo(this.gameObject, iTween.Hash("x", 40f, "y", 90f, "z", 180f, "time", 0.35f,
            "easeType", "easeOutExpo", "isLocal", true));
        SetSoundEffect.Play();

    }

    void Slashing()
    {
        Collider cl = null;
        if (this.GetComponent<Collider>())
            cl = this.GetComponent<Collider>();
        else if (this.transform.GetChild(0).GetComponent<Collider>())
            cl = this.transform.GetChild(0).GetComponent<Collider>();

        cl.isTrigger = false;
        iTween.RotateAdd(this.gameObject, iTween.Hash("y", -150.0f, "time", 0.2f,
            "easeType", "easeOutQuart", "isLocal", true));
        StartCoroutine(OnKinematic(cl, 0.2f));

        // 飛ぶ斬撃
        GameObject zngk = Instantiate(zangeki, this.transform.position, this.transform.parent.rotation) as GameObject;
        Destroy(zngk, lifetime);// 飛ばしたのは寿命がある
        zngk.GetComponent<Flying>().Go();

        // SE再生
        SlashSoundEffect.Play();
    }

    IEnumerator OnKinematic(Collider cl, float delay)
    {
        yield return new WaitForSeconds(delay);
        cl.isTrigger = true;
    }
}
