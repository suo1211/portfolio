using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject shot;
    public GameObject muzzle;
    public GameObject muzzleFlash;

    float shotInterval = 0;
    float shotIntervalMax = 0.25F;

    // Update is called once per frame
    void Update()
    {
        //発射間隔を設定する
        shotInterval += Time.deltaTime;


        //弾を発射する
        if (Input.GetButton("Fire1"))
        {
            if(shotInterval > shotIntervalMax)
            {
                Instantiate(shot, muzzle.transform.position, Camera.main.transform.rotation);
                shotInterval = 0;

                //マズルフラッシュを表示する
                Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
            }
        }
    }
}
