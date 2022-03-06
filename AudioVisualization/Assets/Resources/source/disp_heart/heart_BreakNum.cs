using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heart_BreakNum : MonoBehaviour
{
    public Sprite heart_full;
    public Sprite heart_emp;
    public Image heart;
    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = heart.GetComponentsInChildren<Image>();
        break_num();
        
    }

    public void break_num()
    {
        int total = 0;
        int total_max = 480; // 15 * 8 * 4
        float one_heart = total_max / 5;

        
        foreach (int value in dataManager.object_dict.Values)
        {
            total += value;
        }
        Debug.Log("�󂵂����v�񐔁F" + total);

        if (total <= one_heart)
        {
            Debug.Log("�󂵂����n�[�g1");
        }
        else if (one_heart < total && total <= one_heart * 2)
        {
            Debug.Log("�󂵂����n�[�g2");
            hearts[1].sprite = heart_full;
        }
        else if (one_heart * 2 < total && total <= one_heart * 3)
        {
            Debug.Log("�󂵂����n�[�g3");
            for (int i = 1; i < 3; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else if (one_heart * 3 < total && total <= one_heart * 4)
        {
            Debug.Log("�󂵂����n�[�g4");
            for (int i = 1; i < 4; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else
        {
            Debug.Log("�󂵂����n�[�g5");
            for (int i = 1; i < 5; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
    }
}
