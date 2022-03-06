using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWepon : MonoBehaviour
{
    public GameObject Wepon1;
    public GameObject Wepon2;
    public GameObject Wepon3;
    public List<GameObject> wepons = new List<GameObject>();

    public int jc_ind = 0;
    private List<Joycon> joycons;
    private bool w1;
    private bool w2;
    private bool w3;
    private float timeOut;
    private float timing;

    // Start is called before the first frame update
    void Start()
    {
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
        w1 = false;
        w2 = false;
        w3 = false;
        timeOut = 0.25f;
        Generate_set();
    }

    // Update is called once per frame
    void Update()
    {
        Joycon j = joycons[0];
        timing += Time.deltaTime;

        // A button
        if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT) && timing >= timeOut)
        {
            Debug.Log("A pressed");
            Generate_1();
            //j.Recenter();
            timing = 0.0f;
        }

        // B button
        if (j.GetButtonDown(Joycon.Button.DPAD_DOWN) && timing >= timeOut)
        {
            Debug.Log("B pressed");
            Generate_2();
            //j.Recenter();
            timing = 0.0f;
        }

        // Y button
        if (j.GetButtonDown(Joycon.Button.DPAD_LEFT) && timing >= timeOut)
        {
            Debug.Log("Y pressed");
            Generate_3();
            // j.Recenter();
            timing = 0.0f;
        }
    }
    void Generate_set()
    {
        var parent = this.transform;
        GameObject on_wepon = Instantiate(Wepon1, this.transform.position, this.transform.rotation, parent) as GameObject;
        wepons.Add(on_wepon);
    }

    void Generate_1()
    {
        if (w1 == false)
        {
            Destroy(wepons[0]);
            var parent = this.transform;
            GameObject on_wepon = Instantiate(Wepon1, this.transform.position, this.transform.rotation, parent) as GameObject;
            wepons[0] = on_wepon;
            w1 = true;
            w2 = false;
            w3 = false;
        }
    }

    void Generate_2()
    {
        if (w2 == false)
        {
            Destroy(wepons[0]);
            var parent = this.transform;
            GameObject on_wepon = Instantiate(Wepon2, this.transform.position, this.transform.rotation,parent) as GameObject;
            wepons[0] = on_wepon;
            w2 = true;
            w1 = false;
            w3 = false;
        }
    }

    void Generate_3()
    {
        if (w3 == false)
        {
            Destroy(wepons[0]);
            var parent = this.transform;
            GameObject on_wepon = Instantiate(Wepon3, this.transform.position, this.transform.rotation,parent) as GameObject;
            wepons[0] = on_wepon;
            w3 = true;
            w1 = false;
            w2 = false;
        }
    }
}