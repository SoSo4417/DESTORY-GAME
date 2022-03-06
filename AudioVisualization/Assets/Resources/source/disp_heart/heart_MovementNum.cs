using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heart_MovementNum : MonoBehaviour
{
    public Sprite heart_full;
    public Sprite heart_emp;
    public Image heart;
    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = heart.GetComponentsInChildren<Image>();
        movement_num();
    }

    public void movement_num()
    {
        int total = dataManager.weapon_all;
        int total_max = 480; // 15 * 8 * 4
        float one_heart = total_max / 5;

        Debug.Log("動いた合計回数：" + total);

        if (total <= one_heart)
        {
            Debug.Log("動いた数ハート1");
        }
        else if (one_heart < total && total <= one_heart * 2)
        {
            Debug.Log("動いた数ハート2");
            hearts[1].sprite = heart_full;
        }
        else if (one_heart * 2 < total && total <= one_heart * 3)
        {
            Debug.Log("動いた数ハート3");
            for (int i = 1; i < 3; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else if (one_heart * 3 < total && total <= one_heart * 4)
        {
            Debug.Log("動いた数ハート4");
            for (int i = 1; i < 4; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else
        {
            Debug.Log("動いた数ハート5");
            for (int i = 1; i < 5; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
    }
}
