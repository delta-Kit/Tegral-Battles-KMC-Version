using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        scale=0.1f;   
    }

    // Update is called once per frame
    void Update()
    {
        scale+=0.1f;
        this.transform.localScale=new Vector3(scale,scale,1f);
        if(scale>=2)Destroy(this.gameObject);
    }
}
