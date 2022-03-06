using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChangeDestroy : MonoBehaviour
{
    private GameObject St_M;
    private StageManager st_m;

    private int nowstage;

    // Start is called before the first frame update
    void Start()
    {
        St_M = GameObject.Find("StageManager");
        st_m = St_M.GetComponent<StageManager>();
        nowstage = st_m.GetNowStage();
    }

    // Update is called once per frame
    void Update()
    {
        int ns = st_m.GetNowStage();
        if (ns != nowstage)
            Destroy(this.gameObject);
    }
}
