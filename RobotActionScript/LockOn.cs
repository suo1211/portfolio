﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour
{
    GameObject target = null;

    bool isSearch;

    public Image lockOnImage;

    public GameObject enemyAp;

    public Image gaugeImage;

    public Text textDistance;

    // Start is called before the first frame update
    void Start()
    {
        isSearch = false;

        lockOnImage.enabled = false;

        enemyAp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Lock"))
        {
            //ロックオンモード切り替え
            isSearch = !isSearch;

            //ロックを解除する
            if (!isSearch)
            {
                target = null;
            }
            else
            {
                //一番近いターゲットを取得する
                target = FindClosestEnemy();

                //ターゲットを取得する
                //target = GameObject.FindWithTag("Enemy");
            }
        }

        //ロックオンモードで敵がいれば敵の方向を向く
        if (isSearch == true)
        {
            if (target != null)
            {
                //ターゲットの方向を向く
                //transform.LookAt(target.transform);

                //スムーズにターゲットの方を向く
                Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

                //カメラをターゲットに向ける
                Transform cameraParent = Camera.main.transform.parent;
                Quaternion targetRotation2 = Quaternion.LookRotation(target.transform.position - cameraParent.position);
                cameraParent.localRotation = Quaternion.Slerp(cameraParent.localRotation, targetRotation2, Time.deltaTime * 10);
                cameraParent.localRotation = new Quaternion(cameraParent.localRotation.x, 0, 0, cameraParent.localRotation.w);
            }
            else
            {
                //ロックオンモードでロックしてなければ敵を探す
                target = FindClosestEnemy();
            }
        }

        if (target != null)
        {
            //距離が離れたらロックを解除する
            if (Vector3.Distance(target.transform.position, transform.position) > 100)
            {
                target = null;
            }
        }

        //ターゲットがいたらロックオンカーソルを表示する
        bool isLocked = false;

        if(target != null)
        {
            isLocked = true;

            lockOnImage.transform.rotation = Quaternion.identity;

            //ターゲットの表示位置にロックオンカーソルを合わせる
            lockOnImage.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);

            //敵の体力をゲージに反映させる
            Enemy targetScript = target.GetComponent<Enemy>();
            gaugeImage.transform.localScale = new Vector3((float)targetScript.armorPoint / targetScript.armorPointMax, 1, 1);

            //敵との距離を表示する
            textDistance.text = Vector3.Distance(target.transform.position, transform.position).ToString();
        }
        else
        {
            //ロックオンモード時はカーソルを回転させる
            lockOnImage.transform.Rotate(0, 0, Time.deltaTime * 200);
        }

        lockOnImage.enabled = isSearch;

        //敵の体力ゲージを切り替え可能にする
        enemyAp.SetActive(isLocked);

    }

    //一番近い敵を探して取得
    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if(closest != null)
        {
            //一番近くの敵がロックオン範囲外ならロックしない
            if(Vector3.Distance(closest.transform.position, transform.position) > 100)
            {
                closest = null; 
            }
        }
        return closest;
    }
}
