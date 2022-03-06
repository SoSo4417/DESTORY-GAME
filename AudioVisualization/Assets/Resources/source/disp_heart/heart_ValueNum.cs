using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heart_ValueNum : MonoBehaviour
{
    public Sprite heart_full;
    public Sprite heart_emp;
    public Image heart;
    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = heart.GetComponentsInChildren<Image>();
        value_num();
    }

    public void value_num()
    {
        float total = dataManager.value_all;
        float total_max = 154 * 15;
        float one_heart = total_max / 5;

        Debug.Log("価値合計:" + dataManager.value_all);

        if (total <= one_heart)
        {
            Debug.Log("価値ハート1");
        }
        else if (one_heart < total && total <= one_heart * 2)
        {
            Debug.Log("価値ハート2");
            hearts[1].sprite = heart_full;
        }
        else if (one_heart * 2 < total && total <= one_heart * 3)
        {
            Debug.Log("価値ハート3");
            for (int i = 1; i < 3; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else if (one_heart * 3 < total && total <= one_heart * 4)
        {
            Debug.Log("価値ハート4");
            for (int i = 1; i < 4; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else
        {
            Debug.Log("価値ハート5");
            for (int i = 1; i < 5; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
    }
}
