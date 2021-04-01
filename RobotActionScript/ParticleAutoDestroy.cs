using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //パーティクル終了時に自動的に消滅させる
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
