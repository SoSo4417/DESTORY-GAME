using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTgt : MonoBehaviour
{
    private float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3)
        {
            float randX = Random.Range(-2, 2);
            float randZ = Random.Range(-2, 2);
            this.transform.localPosition += new Vector3(randX, 0f, randZ);
            time = 0f;
        }
        else if (time > 1 && time < 1.1)
        {
            this.transform.position = this.transform.parent.position;
        }
    }
}
