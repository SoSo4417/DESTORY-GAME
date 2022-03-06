using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_test : MonoBehaviour
{
    public GameObject weapon01;
    public GameObject weapon02;
    public GameObject weapon03;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon01.SetActive(true);
            weapon02.SetActive(false);
            weapon03.SetActive(false);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon01.SetActive(false);
            weapon02.SetActive(true);
            weapon03.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon01.SetActive(false);
            weapon02.SetActive(false);
            weapon03.SetActive(true);
        }
    }
}
