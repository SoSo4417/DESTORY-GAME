using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heart_MovementSize : MonoBehaviour
{
    public Sprite heart_full;
    public Sprite heart_emp;
    public Image heart;
    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = heart.GetComponentsInChildren<Image>();
        movement_size();
    }

    public void movement_size()
    {

        float x_size = dataManager.Xaccel_max;
        float y_size = dataManager.Yaccel_max;
        float z_size = dataManager.Zaccel_max;


        float all_size = x_size + y_size + z_size;

        float x_max = 8.0f;
        float y_max = 6.0f;
        float z_max = 8.0f;

        float max = x_max + y_max + z_max;

        float one_heart = max / 5;

        //Debug.Log("動いた合計回数：" + total);

        if (all_size <= one_heart)
        {
            Debug.Log("動き大きさハート1");
        }
        else if (one_heart < all_size && all_size <= one_heart * 2)
        {
            Debug.Log("動き大きさハート2");
            hearts[1].sprite = heart_full;
        }
        else if (one_heart * 2 < all_size && all_size <= one_heart * 3)
        {
            Debug.Log("動き大きさハート3");
            for (int i = 1; i < 3; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else if (one_heart * 3 < all_size && all_size <= one_heart * 4)
        {
            Debug.Log("動き大きさハート4");
            for (int i = 1; i < 4; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
        else
        {
            Debug.Log("動き大きさハート5");
            for (int i = 1; i < 5; i++)
            {
                hearts[i].sprite = heart_full;
            }
        }
    }
}
