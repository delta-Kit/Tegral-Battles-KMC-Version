using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rg;
    public int type;
    public Sprite sigmaK1;
    SpriteRenderer enemySpriteRenderer;
    private int cnt;
    // Start is called before the first frame update
    void Start()
    {
        rg=this.gameObject.GetComponent<Rigidbody2D>();
        enemySpriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        switch(type){
            case 1:
            this.gameObject.GetComponent<CircleCollider2D>().radius=3;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(type){
            case 1:
            if(cnt<180){
                rg.velocity=new Vector2(-10,0);
            }else{
                rg.velocity=new Vector2(0,0);
            }
            break;
        }
        cnt++;
    }
}
