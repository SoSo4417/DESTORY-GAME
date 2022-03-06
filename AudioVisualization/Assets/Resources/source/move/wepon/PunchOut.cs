using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchOut : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

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

        // ジョイコンは一個に限定
        Joycon j = joycons[0];

        // カメラ位置を更新
        if (this.transform.parent != null)
            CamP = this.transform.parent.position;

        // 加速度を得る
        accel = j.GetAccel();

        // 突き出し(JoyCon上ではx)
        float Z = accel.x;
        if (Z < -1.00 && Set == false && rag > 0.25f)
        {
            Setting();
            Set = true;
            rag = 0.0f;
            GetComponent<weapon_count>().Zweapon_count();
        }
        if (Z > 1.00 && Set == true && rag > 0.25f)
        {
            Punching();
            Set = false;
            rag = 0.0f;
            if (Z > dataManager.Zaccel_max)
            {
                dataManager.Zaccel_max = Z;
            }
        }

    }

    void Setting()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("x", CamP.x, "y", CamP.y, "z", CamP.z, "time", 0.20f));
    }

    void Punching()
    {
        iTween.MoveAdd(this.gameObject, iTween.Hash("z", 0.25f, "time", 0.20f));
        SoundEffect.Play();
    }
}
