using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{

    public float speed;
    public bool fly;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.AddForce(0, 0, force, ForceMode.Impulse);
        //print(this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (fly == true)
        {
            this.gameObject.transform.parent = null;
            Vector3 vec = gameObject.transform.rotation * new Vector3(0f, 0f, speed);
            this.transform.position += vec * Time.deltaTime;
        }
    }

    public void Set()
    {
        fly = false;
    }

    public void Go()
    {
        fly = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Untagged")
        {
            if (this.gameObject.GetComponent<Collider>())
                Destroy(this.gameObject.GetComponent<Collider>());
            else if (this.transform.GetChild(0).gameObject.GetComponent<Collider>())
                Destroy(this.transform.GetChild(0).gameObject.GetComponent<Collider>());
        }
    }
}
