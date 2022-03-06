using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThrust : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;

    private bool P = false;
    private float rag = 0.0f;
    private float rot_g;
    public int jc_ind = 0;
    private Vector3 CamP;

    // Start is called before the first frame update
    void Start()
    {
        gyro = new Vector3(0.0f, 0.0f, 0.0f);
        accel = new Vector3(0.0f, 0.0f, 0.0f);
        iTween.Init(this.gameObject);
        this.transform.Rotate(-170f, 0f, 0f);

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

        // 現在のジャイロを取得
        gyro = j.GetGyro();
        rot_g = gyro.y * -0.5f;
        if (P == false)
            this.transform.Rotate(rot_g, 0.0f, 0.0f, Space.World);

        CamP = this.transform.parent.position;

        // 加速度を得る
        accel = j.GetAccel();

        // 突き出し(Joycon上ではx)
        float Z = accel.x;
        if (Z > 1.0 && P == false && rag > 0.55f)
        {
            PushOut();
            P = true;
            rag = 0.0f;
            GetComponent<weapon_count>().Zweapon_count();
        }
        if (Z < -1.0 && P == true && rag > 0.55f)
        {
            PushOff();
            P = false;
            rag = 0.0f;
        }
    }

    void PushOut()
    {
        iTween.MoveAdd(this.gameObject, iTween.Hash("z", -0.50f, "time", 0.5f, "easeType", "easeInOutBack",
            "isLocal", true));
        //Debug.Log("Push Out!" + this.transform.position.z);
    }
    void PushOff()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("x",CamP.x,"y", CamP.y, "z", CamP.z, "time", 0.5f));
        //iTween.RotateTo(this.gameObject, iTween.Hash("x", rot_g, "time", 0.5f, "easeType", "waseInOutBack");
        //Debug.Log("Push Off.." + this.transform.position.z);
    }
}
