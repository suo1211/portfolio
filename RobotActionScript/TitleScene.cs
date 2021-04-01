using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Text blinkText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ボタンを押したら遷移
        if (Input.anyKeyDown)
        {
            Application.LoadLevel("main");
        }

        //ボタンを押させるためのメッセージを点滅させる
        blinkText.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
    }
}
