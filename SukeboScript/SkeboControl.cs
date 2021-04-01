using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeboControl : MonoBehaviour
{
    float speed = 0; //変数宣言

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //マウスの左ボタンを押したらスピード向上
        if (Input.GetMouseButtonDown(0))
        {
            this.speed = 0.1f;
        }

        //オブジェクトをx軸に移動
        transform.Translate(this.speed, 0, 0);

        //減速
        this.speed *= 0.98f;
    }
}
