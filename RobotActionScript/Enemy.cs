﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject target;

    public GameObject shot;

    float shotInterval = 0;
    float shotIntervalMax = 1.0F;

    public GameObject exprosion;

    public int armorPoint;
    public int armorPointMax = 1000;
    int damage = 100;

    float timer = 0;

    int enemyLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ターゲットを取得
        target = GameObject.Find("PlayerTarget");

        armorPoint = armorPointMax;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //経過時間に応じてレベルをあげる
        if(timer < 5)
        {
            enemyLevel = 1;
        }else if(timer < 10)
        {
            enemyLevel = 2;
        }else if(timer < 15)
        {
            enemyLevel = 3;
        }else if(timer >= 15)
        {
            enemyLevel = 4;
            //レベル4: 攻撃間隔が短くなる
            shotIntervalMax = 0.5F;
        }

        //レベル2:プレイヤーが一定範囲に近づいたら攻撃
        if (enemyLevel >= 2)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= 30)
            {
                //ターゲットのほうを徐々に向く
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * 5);

                //一定間隔でショット
                shotInterval += Time.deltaTime;

                if (shotInterval > shotIntervalMax)
                {
                    Instantiate(shot, transform.position, transform.rotation);
                    shotInterval = 0;
                }
            }
            else
            {
                //レベル3:プレイヤーに自分から近づく
                if (enemyLevel >= 3)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * 5);
                    transform.position += transform.forward * Time.deltaTime * 20;
                }
            }
        }

        if(Vector3.Distance (target.transform.position, transform.position) <= 30)
        {
            //スムーズにターゲットの方向を向く
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

            shotInterval += Time.deltaTime;

            if (shotInterval > shotIntervalMax)
            {
                Instantiate(shot, transform.position, transform.rotation);
                shotInterval = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
        //プレイヤーと衝突したら消滅する
        if(collider.gameObject.tag == "Shot")
        {
            //Destroy(gameObject);
            //Instantiate(exprosion, transform.position, transform.rotation);

            //ダメージをランダムで変える
            //damage = Random.Random.Range(50, 150);

            //プレイヤーの弾からダメージを取得する
            damage = collider.gameObject.GetComponent<ShotPlayer>().damage;

            //プレイヤーと衝突したらダメージ
            armorPoint -= damage;
            Debug.Log(armorPoint);

            //体力が0以下になったら消滅する
            if(armorPoint <= 0)
            {
                Destroy(gameObject);
                Instantiate(exprosion, transform.position, transform.rotation);

                //リザルト用のスコアを加算する
                BattleManager.score++;
            }
        }
    }
}
