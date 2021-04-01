﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMove : MonoBehaviour
{
    public float speed = 15.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    int boostPoint;
    int boostPointMax = 100;

    public Image gaugeImage;

    Vector3 moveSpeed;

    const float addNormalSpeed = 1; //通常時の加速度
    const float addBoostSpeed = 2; //ブースト時の加速度
    const float moveSpeedMax = 20; //通常時の最大速度
    const float boostSpeedMax = 40; //ブースト時の最大速度

    bool isBoost;

    // Start is called before the first frame update
    void Start()
    {
        boostPoint = boostPointMax;

        moveSpeed = Vector3.zero;

        isBoost = false;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを移動させる
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection.y = 0;

            //ブーストボタンが押されていればフラグを立て、ブーストポイントを消費
            if (Input.GetButton("Boost") && boostPoint > 1)
            {
                boostPoint -= 1;

                isBoost = true;
            }
            else
            {
                isBoost = false;
            }

            Vector3 targetSpeed = Vector3.zero; //目標速度
            Vector3 addSpeed = Vector3.zero; //加算速度

            //左右移動時の目標速度と加算速度
            if (Input.GetAxis("Horizontal") == 0)
            {
                //押していないときは目標速度を0にする
                targetSpeed.x = 0;

                //設置している時と空中にいる時は減少値を変える
                if (controller.isGrounded)
                {
                    addSpeed.x = addNormalSpeed;
                }
                else
                {
                    addSpeed.x = addNormalSpeed / 4;
                }
            }
            else
            {
                //通常時とブースト時で変化
                if (isBoost)
                {
                    targetSpeed.x = boostSpeedMax;
                    addSpeed.x = addBoostSpeed;
                }
                else
                {
                    targetSpeed.x = moveSpeedMax;
                    addSpeed.x = addNormalSpeed;
                }

                targetSpeed.x *= Mathf.Sign(Input.GetAxis("Horizontal"));
            }

            //左右移動の速度
            moveSpeed.x = Mathf.MoveTowards(moveSpeed.x, targetSpeed.x, addSpeed.x);
            moveDirection.x = moveSpeed.x;

            //前後移動の目標速度と加算速度
            if (Input.GetAxis("Vertical") == 0)
            {
                targetSpeed.z = 0;

                if (controller.isGrounded)
                {
                    addSpeed.z = addNormalSpeed;
                }
                else
                {
                    addSpeed.z = addNormalSpeed / 4;
                }
            }
            else
            {
                if (isBoost)
                {
                    targetSpeed.z = boostSpeedMax;
                    addSpeed.z = addBoostSpeed;
                }
                else
                {
                    targetSpeed.z = moveSpeedMax;
                    addSpeed.z = addNormalSpeed;
                }

                targetSpeed.z *= Mathf.Sign(Input.GetAxis("Vertical"));
            }

            //水平移動の速度
            moveSpeed.z = Mathf.MoveTowards(moveSpeed.z, targetSpeed.z, addSpeed.z);
            moveDirection.z = moveSpeed.z;

            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //ブーストキーによる高速移動
            if (Input.GetButton("Boost") && boostPoint > 1)
            {
                moveDirection.x *= 2;
                moveDirection.z *= 2;

                boostPoint -= 1;
            }
        }

        //ジャンプキーによる上昇
        if (Input.GetButton("Jump") && boostPoint > 1)
        {
            if (transform.position.y > 100)
            {
                moveDirection.y = 0;
            }
            else
            {
                moveDirection.y += gravity * Time.deltaTime;
                boostPoint -= 1;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if(!Input.GetButton("Boost") && !Input.GetButton("Jump"))
        {
            boostPoint += 2;

            boostPoint = Mathf.Clamp(boostPoint, 0, boostPointMax);
        }

        controller.Move(moveDirection * Time.deltaTime);

        //ブーストゲージの伸縮
        //gaugeImage.transform.localScale = new Vector3((float)boostPoint / boostPointMax, 1, 1);
    }
}
