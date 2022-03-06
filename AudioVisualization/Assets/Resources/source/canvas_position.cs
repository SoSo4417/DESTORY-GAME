using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas_position : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
