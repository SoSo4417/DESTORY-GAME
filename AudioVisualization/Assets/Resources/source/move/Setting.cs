using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public Vector3 Rotation;
    public Vector3 Position;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        this.transform.Translate(Position);
        this.transform.Rotate(Rotation);
    }

    public Vector3 GetRot()
    {
        return Rotation;
    }
    public Vector3 GetPos()
    {
        return Position;
    }
}
