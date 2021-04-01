using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakudanController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //フレームごとに落下
        transform.Translate(0, -0.2f, 0);

        //画面外に出たらオブジェクトを破壊
        if(transform.position.y < -10.0f){
            Destroy(gameObject);
        }
    }
}
