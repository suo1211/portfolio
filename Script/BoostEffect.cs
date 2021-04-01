using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    public GameObject boostLight;

    // Start is called before the first frame update
    void Start()
    {
        boostLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool flgBoost = false;

        //ジャンプorブースト
        if (Input.GetButton("Boost") || Input.GetButton("Jump"))
        {
            flgBoost = true;

            boostLight.SetActive(flgBoost);
        }
        else
        {
            boostLight.SetActive(false);
        }
    }
}
