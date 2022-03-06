using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTickTime : MonoBehaviour
{
    public Text timeText;
    public int totalSeconds = 60;

    private int tickTime;
    public int TickTime
    {
        get => tickTime;
        set
        {
            tickTime = value;
            int minute = tickTime / 60;
            int second = tickTime % 60;
            timeText.text = minute.ToString("00") + ":" + second.ToString("00");
        }
    }
    private void OnEnable()
    {
        TickTime = totalSeconds;
        StartCoroutine(AutoTickingTime());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator AutoTickingTime()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1);

        for (int i = 0; i < totalSeconds; i++)
        {
            yield return waitTime;
            TickTime--;
        }
    }
}
