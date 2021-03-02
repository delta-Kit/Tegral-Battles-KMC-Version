using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private float scale;
    public int type;
    private int cnt;
    SpriteRenderer sr;
    public Sprite effect, marker;
    // Start is called before the first frame update
    void Start()
    {
        scale = 0.1f;
        cnt = 0;
        sr = GetComponent<SpriteRenderer>();
        switch(type){
            case 1:
            sr.sprite = effect;
            break;
            case 2:
            sr.sprite = marker;
            break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(type){
            case 1:
            scale += 0.1f;
            this.transform.localScale = new Vector3(scale, scale, 1f);
            if(scale >= 2)Destroy(this.gameObject);
            break;
            case 2:
            sr.material.color = new Color(1, 1, 1, (1 + Mathf.Cos(cnt * Mathf.Deg2Rad * 8)) * 0.5f);
            if(cnt >= 60)Destroy(this.gameObject);
            break;
        }
        cnt++;
    }
}
