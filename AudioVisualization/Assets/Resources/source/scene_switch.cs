using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scene_switch : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public float limit;
    public string SceneName;
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > limit)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
