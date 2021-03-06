using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiate : MonoBehaviour
{
    float timer = 3;
    float instantiateInterval = 3; //生成する間隔

    public static int instantiateValue; //生成する残数

    public GameObject enemy;

    void Awake()
    {
        instantiateValue = 50;
    }

    // Start is called before the first frame update
    void Start()
    {
        //instantiateValue = 50;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //一定時間ごとに敵を生成
        if (timer < 0)
        {
            if(instantiateValue > 0)
            {
                //敵をランダムな位置に生成
                Instantiate(enemy, new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(7, 50.0f), Random.Range(-100.0f, 100.0f)), Quaternion.identity);
                instantiateValue--;
            }

            //生成感覚を減らしていく
            instantiateInterval -= 0.1F;
            instantiateInterval = Mathf.Clamp(instantiateInterval, 1.0F, float.MaxValue);

            timer = instantiateInterval;
        }
    }
}
