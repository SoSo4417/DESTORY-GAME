using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class stage5_obj : MonoBehaviour
{
    private GameObject[] obj;   // �^�O�ɊY������Ώە��z��
    private string[] top3_obj = new string[3];  // ���3�̃^�O�z��
    public ranking ranking_script;
    IOrderedEnumerable<KeyValuePair<string, int>> object_rank;


    // Start is called before the first frame update
    void Start()
    {
        //get_top3();
        //picup_obj();
    }

    // ���3��top3_obj�Ɋi�[
    public void get_top3()
    {
        int i = 0;
        object_rank = ranking_script.obj_ranking();
        foreach (KeyValuePair<string, int> pair in object_rank)
        {
            if (i == 3)
            {
                break;
            }
            top3_obj[i] = pair.Key;
            Debug.Log(i + 1 + "�ԉ󂵂����́F" + top3_obj[i]);
            i += 1;
        }
    }

    // ���3�̃^�O�ɊY������Ώە���\��
    // �^�O���ƂɃt�H���_��ݒu���C�^�O�������v����t�H���_��ǂݍ��݃I�u�W�F�N�g����
    public void picup_obj()
    {
        for (int i = 0; i < 3; i++)
        {
            obj = Resources.LoadAll<GameObject>("Using/5/prehabs/" + top3_obj[i]);
            for (int j = 0; j < obj.Length; j++)
            {
                GameObject instance = (GameObject)Instantiate(obj[j], new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, 0.0f), Quaternion.identity);
            }

        }
    }

    // OBJ_Generate�ɓn��
    public GameObject[] Set_Generator()
    {
        GameObject[] obj_1 = Resources.LoadAll<GameObject>("Using/5/prehabs/" + top3_obj[0]);
        GameObject[] obj_2 = Resources.LoadAll<GameObject>("Using/5/prehabs/" + top3_obj[1]);
        GameObject[] obj_3 = Resources.LoadAll<GameObject>("Using/5/prehabs/" + top3_obj[2]);


        GameObject[] arr = new GameObject[obj_1.Length + obj_2.Length + obj_3.Length];
        System.Array.Copy(obj_1, arr, obj_1.Length);
        System.Array.Copy(obj_2, 0, arr, obj_1.Length, obj_2.Length);
        System.Array.Copy(obj_3, 0, arr, obj_1.Length + obj_2.Length, obj_3.Length);

        return arr;
    }
}
