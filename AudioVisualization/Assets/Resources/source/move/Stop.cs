using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        StartCoroutine(OnKinematic(rb, 0.01f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnKinematic(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = true;
    }
}
