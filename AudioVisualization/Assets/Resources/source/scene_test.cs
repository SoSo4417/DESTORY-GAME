using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string scene_name = SceneManager.GetActiveScene().name;

        switch (scene_name)
        {
            case "SampleScene":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("SecondScene");
                }
                break;
            case "SecondScene":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("3rdScene");
                }
                break;
            case "3rdScene":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("4thScene");
                }
                break;
            case "4thScene":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("ResultScene");
                }
                break;
        }
    }
}
