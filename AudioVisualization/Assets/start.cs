using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startbutton_Click : MonoBehaviour
{

    public Button start;

    // Use this for initialization
    void Start()
    {
        //Gets ButtonMount
        Button btnMount = start.GetComponent<Button>();
        //add a listener to ButtonMount, executing TaskOnClick() when click ButtonMount
        start.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Loading Scene1
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}