using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Image compassImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //方角の回転
        compassImage.transform.rotation = Quaternion.Euler(compassImage.transform.rotation.x, compassImage.transform.rotation.y, transform.eulerAngles.y);
    }
}
