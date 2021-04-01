﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAp : MonoBehaviour
{
    public static int armorPoint;
    int armorPointMax = 5000;

    int damage = 100;

    public Text armorText;
    int displayArmorPoint;

    public Color myWhite;
    public Color myYellow;
    public Color myRed;

    public Image gaugeImage;

    // Start is called before the first frame update
    void Start()
    {
        armorPoint = armorPointMax;
        displayArmorPoint = armorPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //体力をUI Textに表示する
        //armorText.text = armorPoint.ToString();

        //テキストをそのまま表示する場合
        //armorText.text = "Hello!";

        //テキストに追加して変数を表示する場合
        //armorText.text = "AP:" + armorPoint;

        //現在の体力と表示用体力が異なっていれば、現在の体力になるまで加減算する
        if(displayArmorPoint != armorPoint)
        {
            displayArmorPoint = (int)Mathf.Lerp(displayArmorPoint, armorPoint, 0.1F);
        }

        //現在の体力と最大体力をUI Textに表示する
        //armorText.text = armorPoint + "/" + armorPointMax;
        armorText.text = string.Format("{0:0000} / {1:0000}", displayArmorPoint, armorPointMax);

        //残り体力の割合により文字の色を変える
        float percentageArmorpoint = (float)displayArmorPoint / armorPointMax;
        if(percentageArmorpoint > 0.5F)
        {
            armorText.color = myWhite;
            gaugeImage.color = new Color(0.25F, 0.7F, 0.6F);
        }else if(percentageArmorpoint > 0.3F)
        {
            armorText.color = myYellow;
            gaugeImage.color = myYellow;
        }
        else
        {
            armorText.color = myRed;
            gaugeImage.color = myRed;
        }

        //ゲージの長さを体力の割合に合わせて伸縮させる
        gaugeImage.transform.localScale = new Vector3(percentageArmorpoint, 1, 1);
    }

    private void OnCollisionEnter(Collision collider)
    {
        //敵の弾と衝突したらダメージ
        if (collider.gameObject.tag == "ShotEnemy")
        {
            armorPoint -= damage;
            armorPoint = Mathf.Clamp(armorPoint, 0, armorPointMax);
        }
    }
}
