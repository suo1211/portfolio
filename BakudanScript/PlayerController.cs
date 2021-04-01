    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        GameObject retryButton;
        public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        this.retryButton = GameObject.Find("RetryButton");
        retryButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
        //右矢印ボタンを押したら移動するメソッド
        public void RightArrowDown(){
            transform.Translate(2,0,0);
        }

        //左矢印ボタンを押したら移動するメソッド
        public void LeftArrowDown(){
            transform.Translate(-2,0,0);
        }
        
        //衝突した瞬間にオブジェクトを破壊
        void OnTriggerEnter2D(Collider2D collision){
            AudioSource.PlayClipAtPoint(clip, new Vector3(0,-3,0));
            retryButton.SetActive(true);
            Destroy(gameObject);
        }

    }