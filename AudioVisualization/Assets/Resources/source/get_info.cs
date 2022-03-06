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
        // •¨‘Ì‚Æ“Á’¥ƒJƒEƒ“ƒg‚Ì«‘ì¬
        for (int i = 0; i < dataManager.Tag.Length; i++)
        {
            dataManager.object_dict.Add(dataManager.Tag[i], 0);
        }
    }
}
