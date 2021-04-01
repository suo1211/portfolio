﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    int battleStatus;

    const int BATTLE_START = 0;
    const int BATTLE_PLAY = 1;
    const int BATTLE_END = 2;

    float timer;

    public Image messageStart;
    public Image messageWin;
    public Image messageLose;

    public static int score; //敵を倒した数。Enemyスクリプトでカウントアップ
    int clearScore; //クリア条件となるスコア

    // Start is called before the first frame update
    void Start()
    {
        battleStatus = BATTLE_START;

        timer = 0;

        messageStart.enabled = true;
        messageWin.enabled = false;
        messageLose.enabled = false;

        score = 0;

        //敵の最大生成数をクリア数にする
        clearScore = EnemyInstantiate.instantiateValue;
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStatus)
        {
            case BATTLE_START:

                timer += Time.deltaTime;

                if(timer > 3)
                {
                    messageStart.enabled = false;

                    battleStatus = BATTLE_PLAY;

                    timer = 0;
                }

                break;

            case BATTLE_PLAY:

                //スコアが出現数に到達したら勝利
                if(score >= clearScore)
                {
                    battleStatus = BATTLE_END;
                    messageWin.enabled = true;
                }

                if(PlayerAp.armorPoint <= 0)
                {
                    battleStatus = BATTLE_END;
                    messageLose.enabled = true;
                }
                break;

            case BATTLE_END:

                //一定時間経過したら遷移可能にする
                timer += Time.deltaTime;

                if(timer > 3)
                {
                    //動きを止める
                    Time.timeScale = 0;

                    if (Input.GetButtonDown("Fire1"))
                    {
                        Application.LoadLevel("Title");

                        //動きを再開する
                        Time.timeScale = 1;
                    }
                }
                break;

            default:
                break;
                
        }
    }
}
