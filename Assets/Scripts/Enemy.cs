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
    private Animator animator;
    public int vx;
    public int vy;
    private int hp;
    private bool isBlue;
    private int blueCnt;
    public GameObject enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        rg=this.gameObject.GetComponent<Rigidbody2D>();
        enemySpriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        if(type<100){
            this.gameObject.GetComponent<CircleCollider2D>().radius=3;
        }else{
            this.gameObject.GetComponent<CircleCollider2D>().radius=6;
        }
        animator=GetComponent<Animator>();
        switch(type){
            case 1:
            hp=5;
            break;
        }
        blueCnt=10;
        enemyManager=GameObject.Find("EnemyManager");
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
                animator.SetTrigger("Stop");
            }
            break;
        }
        if(blueCnt>5)GetComponent<Renderer>().material.shader=Shader.Find("Unlit/Transparent");
        if(hp<=0){
            enemyManager.GetComponent<EnemyManager>().enemy.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        cnt++;
        blueCnt++;
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag=="Bullet"){
            hp--;
            GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
            blueCnt=0;
        }
    }
}