using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playmethod : MonoBehaviour
{
    public GameObject method_1;
    public GameObject method_2;
    public AudioSource SE;
    private bool Read;
    public static playmethod Instance;

    // joycon
    private List<Joycon> joycons;
    private Joycon j;

    private bool m1;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        joycons = JoyconManager.Instance.j;
        j = joycons[0];

        method_1.SetActive(true);
        StartCoroutine(PlayerAttack1());
        StartCoroutine(PlayerAttack2());
    }

    IEnumerator PlayerAttack1()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => j.GetButtonDown(Joycon.Button.DPAD_UP));
        SE.Play();
        method_1.SetActive(false);
        m1 = true;
    }
    IEnumerator PlayerAttack2()
    {
        yield return new WaitUntil(() => m1);
        method_2.SetActive(true);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => j.GetButtonDown(Joycon.Button.DPAD_UP));
        SE.Play();
        method_2.SetActive(false);
        Read = true;
    }

    public bool ReadText()
    {
        return Read;
    }
}