using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class get_info : MonoBehaviour
{
    void Start()
    {
        make_dict();
    }

    public void make_dict()
    {
        // 物体と特徴カウントの辞書作成
        for (int i = 0; i < dataManager.Tag.Length; i++)
        {
            dataManager.object_dict.Add(dataManager.Tag[i], 0);
        }
    }
}
