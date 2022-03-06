using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int StageNum;
    public float[] TimeLimit;
    public int nowstage;
    public string ResultSceneName;
    public float time;

    private bool pauseTime = true;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        nowstage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (pauseTime)
        //    return;

        //time += Time.deltaTime;

        //if (time > TimeLimit[nowstage])
        //{

        //    if (nowstage < StageNum)
        //    {
        //        nowstage++;
        //        time = 0f;
        //    }
        //}
        //if (nowstage >= StageNum)
        //    SceneManager.LoadScene(ResultSceneName);
    }
    // For WUI
    public void GoNext()
    {
        nowstage++;
    }
    public int GetNowStage()
    {
        return nowstage;
    }
    public int GetLastStage()
    {
        return StageNum;
    }

    public void ResetTime()
    {
        time = 0f;
        pauseTime = true;
    }
    public void StartTickTime()
    {
        pauseTime = false;
    }

    public float GetLimit(int nowstg)
    {
        return TimeLimit[nowstage];
    }
}
