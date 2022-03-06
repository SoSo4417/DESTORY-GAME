using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_count : MonoBehaviour
{
    public void tag_count()
    {
        string[] keys = new string[dataManager.object_dict.Keys.Count];
        dataManager.object_dict.Keys.CopyTo(keys, 0);
        int i = 1;
        foreach (var key in keys)
        {
            if (this.gameObject.CompareTag(key))
            {
                dataManager.object_dict[key] += 1;
                Debug.Log("���́F" + key + " �C�󂵂���: " + dataManager.object_dict[key]);
                break;
            }
            else
            {
                i += 1;
                continue;
            }
        }
        /*
        foreach (KeyValuePair<string, int> pair in dataManager.object_dict)
        {
            Debug.Log("���́F" + pair.Key + " �C�󂵂���: " + pair.Value);
        }
        */
    }
    // �����ǉ���
    public int get_tag_count()
    {
        string[] keys = new string[dataManager.object_dict.Keys.Count];
        dataManager.object_dict.Keys.CopyTo(keys, 0);
        int i = 1;
        int getcount = 0;
        foreach (var key in keys)
        {
            if (this.gameObject.CompareTag(key))
            {
                getcount = dataManager.object_dict[key];
                break;
            }
            else
            {
                i += 1;
                continue;
            }
        }
        return getcount;
    }
}