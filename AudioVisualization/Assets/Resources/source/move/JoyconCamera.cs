using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconCamera : MonoBehaviour
{
    private Transform transform;
    public float moveSpeed;
    public float rotateSpeed;
    public float UpDownLimit;

    // 
    private List<Joycon> joycons;
    public float[] stick;
    public int jc_ind = 0;

    // Start is called before the first frame update
    void Start()
    {
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Joycon j = joycons[0];
        stick = j.GetStick();
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * stick[0]);
        transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime * stick[1]);
        if (transform.rotation.y >= -25f && transform.rotation.y <= 25f)

            // カメラリセット
            if (j.GetButtonDown(Joycon.Button.STICK))
            {
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            }
    }
}
