using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class smartballscript : MonoBehaviour
{
    GameObject[] ob_cubes;
    GameObject[] goals;
    float power = 0f;
    bool flg = true;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        ob_cubes = GameObject.FindGameObjectsWithTag("ob_cube");
        goals = GameObject.FindGameObjectsWithTag("goal");
        int n = 0;

        foreach(GameObject obj in goals){
            Renderer renderer = obj.GetComponent<Renderer>();
            renderer.material.SetFloat("_Mode", 3f);
            renderer.material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.DisableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            renderer.material.renderQueue = 3000;
            renderer.material.color = new Color(0f, 0.15f * n, 1f - 0.15f * n++, 0.5f);
        }

        foreach(GameObject obj in ob_cubes){
            Vector3 move = obj.transform.position;
            AnimationClip clip = new AnimationClip();
            clip.legacy = true;
            Keyframe[] keysX = new Keyframe[2];
            keysX[0] = new Keyframe(0f, move.x - 5);
            keysX[1] = new Keyframe(1f, move.x + 3);
            AnimationCurve curveX = new AnimationCurve(keysX);
            clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
            clip.wrapMode = WrapMode.PingPong;

            Keyframe[] keysY = new Keyframe[2];
            keysY[0] = new Keyframe(0f, move.y);
            keysY[1] = new Keyframe(1f, move.y);
            AnimationCurve curveY = new AnimationCurve(keysY);
            clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);

            Keyframe[] keysZ = new Keyframe[2];
            keysZ[0] = new Keyframe(0f, move.z);
            keysZ[1] = new Keyframe(1f, move.z);
            AnimationCurve curveZ = new AnimationCurve(keysZ);
            clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

            Animation animation = obj.GetComponent<Animation>();
            animation.AddClip(clip, "clip1");
            animation.Play("clip1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Renderer renderer = GetComponent<Renderer>();

        MoveCube();

        rigidbody.AddForce(0f,0f,-1f);
        if(flg){
            if(Input.GetKey(KeyCode.Space)){
                power += 0.01f;
                if(power > 1f){
                    power = 0.25f;
                }
                renderer.material.color = new Color(1f, power, 0f);
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            rigidbody.AddForce(new Vector3(0f, 0f, power * 2500f));
            power = 0f;
            renderer.material.color = Color.red;
            flg = false;
        }
    }
    void MoveCube(){
        foreach(GameObject obj in ob_cubes){
            obj.transform.Rotate(new Vector3(0f, 2f, 0f));
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "ob_cube"){
            Behaviour b = (Behaviour)collision.gameObject.GetComponent("Halo");
            b.enabled = true;
        }
    }

    void OnCollisionExit(Collision collision){
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if(collision.gameObject.tag == "ob_cube"){
            Behaviour b = (Behaviour)collision.gameObject.GetComponent("Halo");
            b.enabled = false;
            Vector3 v = rigidbody.velocity;
            if(v.magnitude < 15){
                v *= 2.0f;
                if(v.magnitude < 5){
                    v *= 2.0f;
                }
                rigidbody.velocity = v;
            }
        }

        if(collision.gameObject.tag == "ob_wall"){
            Vector3 v = rigidbody.velocity;
            if(v.magnitude < 15){
                v *= 2.0f;
                if(v.magnitude < 5){
                    v *= 2.0f;
                }
                rigidbody.velocity = v;
            }
        }
    }

    void OnTriggerEnter(Collider collider){
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        int n = 1;
        foreach(GameObject obj in goals){
            if(obj == collider.gameObject){
                score.text = "point:" + (n * 100);
                ParticleSystem ps = collider.gameObject.GetComponent<ParticleSystem>();
                ps.Play();
            }
            n++;
        }
    }
}
