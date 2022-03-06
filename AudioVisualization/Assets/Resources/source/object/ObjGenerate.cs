using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ObjectEveryStages
{
    public GameObject[] Objects;
    public int generate_num;
    public int generate_sec;
}

public class ObjGenerate : MonoBehaviour
{
    public static ObjGenerate Instance;

    public ObjectEveryStages[] Stages;
    public int GenerateLimit;
    public Vector3 GenerateRange;
    public float time;
    public bool gen_flag;
    public int GenerateNum;
    private GameObject St_M;
    private StageManager st_m;
    private GameObject Gr_M;
    private GridChecker gr_m;
    private int gridsize;
    private int stgnum;
    private int LastStage;
    private bool LastStageSet;
    public AudioClip SE;


    public Vector3 originalSize;
    public Vector3 realSize;

    // Start is called before the first frame update
    void Start()
    {
        time = 2.5f;

        stgnum = 0;
        St_M = GameObject.Find("StageManager");
        st_m = St_M.GetComponent<StageManager>();
        LastStage = st_m.GetLastStage();

        Gr_M = GameObject.Find("GridManager");
        gr_m = Gr_M.GetComponent<GridChecker>();
        gridsize = (int)((GenerateRange.x * 2 + 1) * (GenerateRange.z * 2 + 1));
        gr_m.SetGridNum(gridsize);
        gr_m.OnGrid(gridsize / 2);
        LastStageSet = false;
    }
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 campos = this.transform.parent.position;
        if (gen_flag == true)
            time += Time.deltaTime;

        if (stgnum < st_m.GetNowStage())
        {
            stgnum = st_m.GetNowStage();
            gr_m.ResetGrid();
            gr_m.OnGrid(gridsize / 2);
            time = 2.5f;
        }
        if (stgnum < LastStage)
        {
            if (stgnum == LastStage - 1 && LastStageSet == false)
            {
                Set_LastStage();
                LastStageSet = true;
            }

            // 現在のステージのオブジェクト
            ObjectEveryStages nowObj = Stages[stgnum];
            int objnum = nowObj.Objects.Length;

            // 設定秒数ごとに一回生成
            if (time > nowObj.generate_sec)
            {
                int grid = 0;
                int randObj = 0;
                float randx = 0, randy = 0, randz = 0;

                for (int i = 0; i < nowObj.generate_num; i++)
                {
                    // ランダムで座標を指定
                    randx = UnityEngine.Random.Range(-GenerateRange.x - 1, GenerateRange.x + 1);
                    randy = UnityEngine.Random.Range(-GenerateRange.y - 1, GenerateRange.y + 1);
                    randz = UnityEngine.Random.Range(-GenerateRange.z - 1, GenerateRange.z + 1);

                    // グリッド番号の計算
                    int gridx = (int)randx + (int)GenerateRange.x;
                    int gridz = (int)randz + (int)GenerateRange.z;
                    grid = gridx + gridz * (int)(GenerateRange.x * 2 + 1);


                    if (gr_m.CheckGrid(grid) == false)
                    {
                        //gr_m.OnGrid(grid);

                        // ランダムでオブジェクトの設定
                        bool f = false;
                        //while (f == false)
                        //{
                        randObj = UnityEngine.Random.Range(0, objnum);
                        //int count = nowObj.Objects[randObj].GetComponent<obj_count>().get_tag_count();
                        //if (count < GenerateLimit)
                        //    f = true;
                        //if (nowObj.Count[randObj] < GenerateLimit)
                        //{
                        //    nowObj.Count[randObj]++;
                        //    f = true;
                        //}
                        //}
                        // オブジェクトを指定
                        GameObject gen_obj = nowObj.Objects[randObj];
                        //Debug.Log("生成したオブジェクト" + randObj);
                        gen_obj.GetComponent<GridnumOfObj>().SetGridNum(grid);

                        // カメラの周りに生成
                        Vector3 genpos = new Vector3(campos.x + randx, campos.y + randy, campos.z + randz);
                        //Vector3 genpos = new Vector3(campos.x + randx, campos.y, campos.z + randz);
                        GameObject secen_obj = (GameObject)Instantiate(gen_obj, genpos, Quaternion.identity);

                        // 向きを調整
                        Transform Tgt = this.transform.parent.Find("LookingTarget");
                        secen_obj.transform.LookAt(Tgt);

                        // 生成時に音が鳴る
                        AudioSource.PlayClipAtPoint(SE, secen_obj.transform.position);

                        // メッシュの（バウンズ）サイズを取得
                        Mesh mesh = secen_obj.transform.GetComponent<MeshFilter>().mesh;
                        Bounds bounds = mesh.bounds;
                        originalSize = bounds.size;

                        // スケールを掛け合わせた実際のサイズを取得
                        realSize = bounds.size;
                        realSize.x *= transform.localScale.x;
                        realSize.y *= transform.localScale.y;
                        realSize.z *= transform.localScale.z;

                        print(string.Format("name = {0} : Original Size(m) = ({1}, {2}, {3}), Real Size(m) = ({4}, {5}, {6})",
                            this.name,
                            Mathf.Ceil(originalSize.x), Mathf.Ceil(originalSize.y), Mathf.Ceil(originalSize.z),
                            realSize.x, realSize.y, realSize.z));

                        int x_width = (int)(randx + realSize.x / 2 + GenerateRange.x);
                        int z_width = (int)(randz + realSize.z / 2 + GenerateRange.z);
                        int add_grid = x_width + z_width * (int)(GenerateRange.x * 2 + 1);
                        if (add_grid > gridsize)
                        {
                            add_grid = gridsize;
                        }
                        x_width = (int)(randx - realSize.x / 2 + GenerateRange.x);
                        z_width = (int)(randz - realSize.z / 2 + GenerateRange.x);
                        int back_grid = x_width + z_width * (int)(GenerateRange.x * 2 + 1);
                        if (back_grid < 0)
                        {
                            back_grid = 0;
                        }


                        Debug.Log("grid:" + grid);
                        for (int j = grid; j < add_grid; j++)
                        {
                            gr_m.OnGrid(j);
                            Debug.Log("addgrid" + j);
                        }
                        for (int k = grid - 1; k > back_grid; k--)
                        {
                            gr_m.OnGrid(k);
                            Debug.Log("backgrid" + k);
                        }

                    }
                }
                time = 0;
            }
        }
    }

    public void Set_LastStage()
    {
        GameObject LS = GameObject.Find("Stage5Manager");
        stage5_obj sm = LS.GetComponent<stage5_obj>();
        Stages[LastStage - 1].Objects = sm.Set_Generator();
        if (Stages[LastStage - 1].Objects.Length == 0)
        {
            Stages[LastStage - 1].Objects = Stages[0].Objects;
            Debug.Log("最終ステージの生成に失敗．ステージ１を生成します");
        }
    }

    public void Gen_Flag(bool flg)
    {
        gen_flag = flg;
    }

}
