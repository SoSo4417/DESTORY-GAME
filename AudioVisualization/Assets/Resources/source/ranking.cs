using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ranking : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        obj_ranking();
        int i = 0;
        foreach (KeyValuePair<string, int> pair in object_rank)
        {
            Debug.Log("ˆê”Ô‰ó‚µ‚½•¨‘ÌF" + pair.Key + "C‰ñ” : " + pair.Value);
            i += 1;
            if (i == 3)
            {
                i = 0;
                break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    public IOrderedEnumerable<KeyValuePair<string, int>> obj_ranking()
    {
        IOrderedEnumerable<KeyValuePair<string, int>> object_rank;
        object_rank = dataManager.object_dict.OrderByDescending((x) => x.Value);
        return object_rank;
    }
}
