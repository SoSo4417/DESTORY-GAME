using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWpnEffect : MonoBehaviour
{
    public GameObject DefaultEffect;
    public AudioClip DefaultSE_Clip;
    private obj_count objc;

    // Start is called before the first frame update
    void Start()
    {
        objc = GetComponent<obj_count>();
        iTween.Init(this.gameObject);
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (collision.gameObject.GetComponent<Effect>())
            {
                GameObject efct = collision.gameObject.GetComponent<Effect>().BreakEffect;
                Instantiate(efct, this.transform.position, Quaternion.identity);
                //AudioSource SE = collision.gameObject.GetComponent<Effect>().BreakSE;
                AudioClip SE = collision.gameObject.GetComponent<Effect>().BreakSE_clip;
                //SE.Play();
                AudioSource.PlayClipAtPoint(SE, transform.position);
                if (this.GetComponent<Renderer>().material.HasProperty("_Color"))
                    this.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Effect>().BreakColor.color;

            }
            else
            {
                Instantiate(DefaultEffect, this.transform.position, Quaternion.identity);
                //DefaultSE.Play();
                AudioSource.PlayClipAtPoint(DefaultSE_Clip, transform.position);
            }
            objc.tag_count();
            if (this.GetComponent<GridnumOfObj>())
                this.GetComponent<GridnumOfObj>().DestroyAndOff();
            // フェードアウト
            iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 0, "time", 0.2));
            Destroy(this.gameObject, 0.2f);
        }
    }
}
