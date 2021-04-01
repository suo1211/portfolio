using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UIシステムを使うときに必要なライブラリ
using UnityEngine.UI;
//Scene関係の処理を行うときに必要なライブラリ
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRetry(){
        //Sceneを読み込む
        SceneManager.LoadScene("Main");
    }
}
