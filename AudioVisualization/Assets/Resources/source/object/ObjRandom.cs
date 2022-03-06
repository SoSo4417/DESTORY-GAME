using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRandom : MonoBehaviour
{
    public float time;
    public int num;
    public float minx;
    public float miny;
    public float minz;
    public float MaxX;
    public float MaxY;
    public float MaxZ;
    public GameObject[] obj = new GameObject[8];
    private int size = 7;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // Œã‚Å’Ç‹LDD‰ó‚ê‚Ä‚½‚ç¶¬

        if (time > 3)
        {
            float rndx = Random.Range(minx, MaxX);
            float rndy = Random.Range(miny, MaxY);
            float rndz = Random.Range(minz, MaxZ);
            Vector3 rv = new Vector3(rndx, rndy, rndz);
            int rndobj = Random.Range(0, size);

            Instantiate(obj[rndobj], rv, Quaternion.identity);

            time = 0;
        }
    }
}
