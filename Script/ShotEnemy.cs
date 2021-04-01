using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    public GameObject explotion;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0F);
    }

    // Update is called once per frame
    void Update()
    {
        //弾を前進させる
        transform.position += transform.forward * Time.deltaTime * 100;
    }

    private void OnCollisionEnter(Collision collider)
    {
        //プレイヤーと衝突したら爆発して消滅する
        if(collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(explotion, transform.position, transform.rotation);
        }
    }
}
