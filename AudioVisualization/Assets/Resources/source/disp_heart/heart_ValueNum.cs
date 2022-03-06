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

        Debug.Log("���l���v:" + dataManager.value_all);

        if (total <= one_heart)
        {
            Debug.Log("���l�n�[�g1");
        }
        else if (one_heart < total && total <= one_heart * 2)
        {
            Debug.Log("���l�n�[�g2");
            hearts[1].sprite = heart_full;
        }
        else if (one_heart * 2 < total && total <= one_heart * 3)
        {
            Debug.Log("���l�n�[�g3");
            for (int i = 1; i < 3; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else if (one_heart * 3 < total && total <= one_heart * 4)
        {
            Debug.Log("���l�n�[�g4");
            for (int i = 1; i < 4; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else
        {
            Debug.Log("���l�n�[�g5");
            for (int i = 1; i < 5; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
    }
}
