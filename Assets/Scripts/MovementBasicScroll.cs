using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementBasicScroll : MonoBehaviour
{
    public float speed = 1.0f;
    public float speed1= 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    //how long did last frame take?at 60 fps =0.01666
        float DT = Time.deltaTime;
        //move every frame by x units
        transform.position = transform.position+new Vector3(speed, speed1,0)*DT;
    }
}
