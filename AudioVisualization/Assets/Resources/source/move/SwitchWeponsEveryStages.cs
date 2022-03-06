using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeponsEveryStages
{
    public GameObject[] Wepons;
}

public class SwitchWeponsEveryStages : MonoBehaviour
{
    public static SwitchWeponsEveryStages Instance;

    public WeponsEveryStages[] Stages;

    public int jc_ind = 0;
    private List<Joycon> joycons;
    private float timeOut;
    private float timing;
    private GameObject St_M;
    private StageManager st_m;
    public GameObject on_wepon;
    private int stgnum;
    private int LastStage;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
        timeOut = 0.25f;
        St_M = GameObject.Find("StageManager");
        st_m = St_M.GetComponent<StageManager>();
        stgnum = 0;
        LastStage = st_m.GetLastStage();
        //Generate_set();
    }

    // Update is called once per frame
    void Update()
    {
        Joycon j = joycons[0];
        timing += Time.deltaTime;
        if (stgnum < st_m.GetNowStage())
        {
            stgnum = st_m.GetNowStage();

            if (stgnum == LastStage - 1)
            {
                LastChoice();
                //Generate_set();
            }

            //if (LastStage > stgnum)
                //Generate_set();
        }

        // A button
        if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT) && timing >= timeOut)
        {
            Debug.Log("A pressed");
            Generate_1();
            j.Recenter();
            timing = 0.0f;
        }

        // B button
        if (j.GetButtonDown(Joycon.Button.DPAD_DOWN) && timing >= timeOut)
        {
            Debug.Log("B pressed");
            Generate_2();
            j.Recenter();
            timing = 0.0f;
        }

        // Y button
        if (j.GetButtonDown(Joycon.Button.DPAD_LEFT) && timing >= timeOut)
        {
            Debug.Log("Y pressed");
            Generate_3();
            j.Recenter();
            timing = 0.0f;
        }
    }
    void Generate_set()
    {
        var parent = this.transform;
        if (on_wepon != null)
            Destroy(on_wepon);
        on_wepon = Instantiate(Stages[stgnum].Wepons[0], this.transform.position, this.transform.rotation, parent)
            as GameObject;
        if (on_wepon.GetComponent<Setting>())
            on_wepon.GetComponent<Setting>().Set();
    }

    void Generate_1()
    {

        Destroy(on_wepon);
        var parent = this.transform;
        on_wepon = Instantiate(Stages[stgnum].Wepons[0], this.transform.position, this.transform.rotation, parent)
            as GameObject;
        if (on_wepon.GetComponent<Setting>())
            on_wepon.GetComponent<Setting>().Set();
    }

    void Generate_2()
    {
        Destroy(on_wepon);
        var parent = this.transform;
        on_wepon = Instantiate(Stages[stgnum].Wepons[1], this.transform.position, this.transform.rotation, parent)
            as GameObject;
        if (on_wepon.GetComponent<Setting>())
            on_wepon.GetComponent<Setting>().Set();
    }

    void Generate_3()
    {
        Destroy(on_wepon);
        var parent = this.transform;
        on_wepon = Instantiate(Stages[stgnum].Wepons[2], this.transform.position, this.transform.rotation, parent)
            as GameObject;
        if (on_wepon.GetComponent<Setting>())
            on_wepon.GetComponent<Setting>().Set();
    }

    public void DestroyWeapon()
    {
        Destroy(on_wepon);
    }

    void LastChoice()
    {
        if (dataManager.weapon_x > dataManager.weapon_all / 3 && dataManager.weapon_x > dataManager.weapon_y && dataManager.weapon_x > dataManager.weapon_z)
        {
            Debug.Log("ïêäÌÇâ°êUÇËÇ…ê›íË");
        }
        else if (dataManager.weapon_y > dataManager.weapon_all / 3 && dataManager.weapon_y > dataManager.weapon_x && dataManager.weapon_y > dataManager.weapon_z)
        {
            stgnum += 1;
            Debug.Log("ïêäÌÇècêUÇËÇ…ê›íË");
        }
        else if (dataManager.weapon_z > dataManager.weapon_all / 3 && dataManager.weapon_z > dataManager.weapon_x && dataManager.weapon_z > dataManager.weapon_y)
        {
            stgnum += 2;
            Debug.Log("ïêäÌÇâúêUÇËÇ…ê›íË");
        }
        else
        {
            Debug.Log("ïêäÌÇÉfÉtÉHÉãÉgÅiâ°êUÇËÅjÇ…ê›íË");
        }
    }
}
