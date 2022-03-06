using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heart_ScaleNum : MonoBehaviour
{
    public Sprite heart_full;
    public Sprite heart_emp;
    public Image heart;
    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = heart.GetComponentsInChildren<Image>();
        scale_num();
    }

    public void scale_num()
    {
        float total = dataManager.scale_all;
        float total_max = 160 * 15;
        float one_heart = total_max / 5;

        Debug.Log("規模合計:" + dataManager.scale_all);

        if (total <= one_heart)
        {
            Debug.Log("規模ハート1");
        }
        else if (one_heart < total && total <= one_heart * 2)
        {
            Debug.Log("規模ハート2");
            hearts[1].sprite = heart_full;
        }
        else if (one_heart * 2 < total && total <= one_heart * 3)
        {
            Debug.Log("規模ハート3");
            for (int i = 1; i < 3; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else if (one_heart * 3 < total && total <= one_heart * 4)
        {
            Debug.Log("規模ハート4");
            for (int i = 1; i < 4; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else
        {
            Debug.Log("規模ハート5");
            for (int i = 1; i < 5; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
    }
}
