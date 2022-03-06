using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Fire : MonoBehaviour
{
    public GameObject Fire;
    public AudioSource ShootSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    void OnMouseDown()
    {
        GameObject FireInst = Instantiate(Fire, this.transform.position, Quaternion.identity) as GameObject;
        FireInst.GetComponent<Flying>().Set();
        FireInst.transform.rotation = this.transform.parent.rotation;
        FireInst.GetComponent<Flying>().Go();
        Destroy(FireInst, 5f);
        ShootSoundEffect.Play();
    }
}
