using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startbehavior : MonoBehaviour
{

    // joycon
    private List<Joycon> joycons;
    private Joycon j;
    public AudioSource SE;

    //startLevelTip.SetActive(true);
    //yield return new WaitForSeconds(3);
    //startLevelTip.SetActive(false);
    public void Click()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    void Start()
    {
        joycons = JoyconManager.Instance.j;
        j = joycons[0];
    }

    void Update()
    {
        if (j.GetButtonDown(Joycon.Button.DPAD_UP))
        {
            if (playmethod.Instance.ReadText() == true)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                SE.Play();
            }
        }
    }
}