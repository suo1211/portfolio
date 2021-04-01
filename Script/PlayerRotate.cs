using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    GameObject CameraParent;
    Quaternion defaultCameraRot;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        CameraParent = Camera.main.transform.parent.gameObject;
        defaultCameraRot = CameraParent.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを回転させる
        transform.Rotate(0, Input.GetAxis("Horizontal2"), 0);

        //カメラを回転させる
        //CameraParent.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);

        //カメラの親オブジェクトの回転
        //GameObject CameraParent = Camera.main.transform.parent.gameObject;
        CameraParent.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);

        //カメラの回転をリセットする
        if (Input.GetButton("CamReset"))
        {
            timer = 0.5F;

            //スムーズにカメラの回転を戻す
            if(timer > 0)
            {
                CameraParent.transform.localRotation = Quaternion.Slerp(CameraParent.transform.localRotation, defaultCameraRot, Time.deltaTime * 10);

                timer -= Time.deltaTime;
            }
        }
    }
}
