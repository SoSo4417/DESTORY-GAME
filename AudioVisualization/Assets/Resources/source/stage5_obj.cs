using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class stage5_obj : MonoBehaviour
{
    private GameObject[] obj;   // タグに該当する対象物配列
    private string[] top3_obj = new string[3];  // 上位3つのタグ配列
    public ranking ranking_script;
    IOrderedEnumerable<KeyValuePair<string, int>> object_rank;


    // Start is called before the first frame update
    void Start()
    {
        //get_top3();
        //picup_obj();
    }

    // 上位3つをtop3_objに格納
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
            Debug.Log(i + 1 + "番壊したもの：" + top3_obj[i]);
            i += 1;
        }
    }

    // 上位3つのタグに該当する対象物を表示
    // タグごとにフォルダを設置し，タグ名を合致するフォルダを読み込みオブジェクト生成
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

    // OBJ_Generateに渡す
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
