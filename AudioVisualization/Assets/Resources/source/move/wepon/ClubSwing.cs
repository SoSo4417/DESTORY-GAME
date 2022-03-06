using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubSwing : MonoBehaviour
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
    public GameObject SwingBomb;
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
    }

    // Update is called once per frame
    void Update()
    {
        // �b�����J�E���g
        rag += Time.deltaTime;

        // �W���C�R���͈�Ɍ���
        Joycon j = joycons[0];

        // �J�����ʒu���X�V
        if (this.transform.parent != null)
        {
            CamP = this.transform.parent.position;
            CamR = this.transform.parent.rotation;
        }

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
        iTween.MoveTo(this.gameObject, iTween.Hash("x", 0.15f, "y", -0.20f, "z", 0.8f, "time", 0.35f,
    "easeType", "easeOutExpo", "isLocal", true));
        iTween.RotateTo(this.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", -100f, "time", 0.35f,
            "easeType", "easeOutExpo", "isLocal", true));

        SetSE.Play();
    }

    void Swing()
    {
        Collider cl = null;
        if (this.GetComponent<Collider>())
            cl = this.GetComponent<Collider>();
        else if (this.transform.GetChild(0).GetComponent<Collider>())
            cl = this.transform.GetChild(0).GetComponent<Collider>();
        cl.isTrigger = false;
        iTween.MoveAdd(this.gameObject, iTween.Hash("x", -0.25f, "z", 0.25f, "time", 0.45f,
            "easeType", "easeInOutBack", "isLocal", true));
        iTween.RotateAdd(this.gameObject, iTween.Hash("y", 30f, "z", 150.0f, "time", 0.5f,
            "easeType", "easeInOutBack", "isLocal", true));
        StartCoroutine(OnKinematic(cl, 0.5f));
        // SE�Đ�
        SoundEffect.Play();

        Vector3 EPos = CamR * new Vector3(0f, 0f, 1f);
        GameObject BOMB = Instantiate(SwingBomb, this.transform.position + EPos, CamR) as GameObject;
        BOMB.GetComponent<Flying>().Go();
        Destroy(BOMB, lifetime);
    }

    IEnumerator OnKinematic(Collider cl, float delay)
    {
        yield return new WaitForSeconds(delay);
        cl.isTrigger = true;
    }
}
