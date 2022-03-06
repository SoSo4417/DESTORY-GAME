using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public GameObject WeponEffect;
    public GameObject BreakEffect;
    public Vector3 GeneratePoint;
    public AudioClip BreakSE_clip;
    public Material BreakColor;

    // Start is called before the first frame update
    void Start()
    {
        var wepon = this.transform;
        Vector3 pos = new Vector3(wepon.position.x + GeneratePoint.x,
            wepon.position.y + GeneratePoint.y,
            wepon.position.z + GeneratePoint.z);
        Instantiate(WeponEffect, pos, Quaternion.identity, wepon);
    }

    // Update is called once per frame
    void Update()
    {
        var wepon = this.transform;
    }
}
