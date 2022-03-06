using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NunchakuSwing : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;
    private float rag = 0.0f;
    private bool Set;
    public int jc_ind = 0;
    private Vector3 CamP;
    public AudioSource SoundEffect;

    private GameObject grip;

    // Start is called before the first frame update
    void Start()
    {
        gyro = new Vector3(0.0f, 0.0f, 0.0f);
        accel = new Vector3(0.0f, 0.0f, 0.0f);
        Set = false;
        grip = this.transform.Find("pCylinder2").gameObject;
        iTween.Init(this.gameObject);
        iTween.Init(grip);

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
        // �b�����J�E���g
        rag += Time.deltaTime;

        // �J�����ʒu���X�V
        if (this.transform.parent != null)
            CamP = this.transform.parent.position;

        // �W���C�R���͈�Ɍ���
        Joycon j = joycons[0];

        // �����x�𓾂�
        accel = j.GetAccel();

        // ���U��(Joycon��ł�y)
        float X = accel.y;
        if (X > 1.0 && Set == false && rag > 0.55f)
        {
            // ->�����������ŃZ�b�g(���邮���)
            Setting();
            Set = true;
            rag = 0.0f;
        }
        if (X < -1.0 && Set == true && rag > 0.55f)
        {
            // <-�����������ɐU��
            Swing();
            Set = false;
            rag = 0.0f;
            GetComponent<weapon_count>().Xweapon_count();
            if (X > dataManager.Xaccel_max)
            { // ���Ȃ̂�*-1
                dataManager.Xaccel_max = X;
            }
        }
    }

    void Setting()
    {
        iTween.MoveTo(grip, iTween.Hash("x", CamP.x + 5f, "y", CamP.y, "z", CamP.z,
            "easeType", "easeInOutBack", "time", 0.5f, "isLocal", true));

        iTween.RotateTo(grip, iTween.Hash("x", 0f, "y", 0f, "z", 0f,
            "time", 0.5f, "isLocal", true));
    }

    void Swing()
    {
        // ������Ǝ΂ߏ��
        iTween.MoveAdd(grip, iTween.Hash("x", 0.0f, "y", -0.05f, "z", 0.15f,
            "easeType", "easeInOutBack", "time", 0.5f, "isLocal", true));

        iTween.MoveAdd(grip, iTween.Hash("x", 0.35f, "y", 0.05f, "z", -0.15f,
            "easeType", "easeInOutBack", "time", 0.5f, "isLocal", true));

        iTween.RotateAdd(grip, iTween.Hash("x", 60f, "y", -90f, "z", -160f,
            "easeType", "easeOutQuint", "time", 0.5f, "isLocal", true));
    }
}
