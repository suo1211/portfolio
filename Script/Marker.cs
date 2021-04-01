﻿using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    Image marker;
    public Image markerImage;
    GameObject compass;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("PlayerTarget");

        //マーカーをレーダー（コンパス）上に表示する
        compass = GameObject.Find("CompassMask");
        marker = Instantiate(markerImage, compass.transform.position, Quaternion.identity) as Image;
        marker.transform.SetParent(compass.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        //マーカーをプレイヤーの相対位置に配置する
        Vector3 position = transform.position - target.transform.position;
        marker.transform.localPosition = new Vector3(position.x, position.z, 0);

        /*
        //レーダーの範囲外に出たら表示しない
        if(Vector3.Distance(target.transform.position, transform.position) <= 150)
        {
            marker.enabled = true;
        }
        else
        {
            marker.enabled = false;
        }
        */
    }
    //敵が消滅したらマーカーも消滅させる
    void OnDestroy()
    {
        Destroy(marker);
    }
}
