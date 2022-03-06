using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackStart : MonoBehaviour
{

    // joycon
    private List<Joycon> joycons;
    private Joycon j;
    public AudioSource SE;
    public bool Back;

    // Start is called before the first frame update
    void Start()
    {
        joycons = JoyconManager.Instance.j;
        j = joycons[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (j.GetButtonDown(Joycon.Button.DPAD_UP) || Back == true)
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            Reset();
            SE.Play();

        }
    }

    void Reset()
    {
        dataManager.Tag = new string[100];
        dataManager.VandS.Clear();

        dataManager.object_dict.Clear();
        dataManager.value_all = 0;
        dataManager.scale_all = 0;

        dataManager.weapon_x = 0;
        dataManager.weapon_y = 0;
        dataManager.weapon_z = 0;
        dataManager.weapon_all = 0;

        dataManager.Xaccel_max = 0.0f;
        dataManager.Yaccel_max = 0.0f;
        dataManager.Zaccel_max = 0.0f;
    }
}
