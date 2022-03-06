using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_KeyCam : MonoBehaviour
{
    private Transform transform;
    public float moveSpeed = 5f;
    public float rotateSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        iTween.Init(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
            transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        // カメラリセット
        if (Input.GetMouseButtonDown(2))
        {
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }

        // 後ろを向く
        if (Input.GetMouseButtonDown(1))
        {
            iTween.RotateAdd(this.gameObject, iTween.Hash("y", 180.0f, "time", 0.5f,
            "easeType", "easeOutExpo", "isLocal", true));
        }
    }
}
