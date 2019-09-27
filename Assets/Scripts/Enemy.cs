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
    public GameObject game;
    private int explodeCnt;
    // Start is called before the first frame update
    void Start()
    {
        rg=this.gameObject.GetComponent<Rigidbody2D>();
        enemySpriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        if(type<100){
            this.gameObject.GetComponent<CircleCollider2D>().radius=2;
        }else if(type<200){
            this.gameObject.GetComponent<CircleCollider2D>().radius=4;
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
        game=GameObject.Find("Game");
        explodeCnt=100;
        cnt=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(explodeCnt>100){
            switch(type){
                case 1:
                Move(1);
                if(cnt==210){
                    game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,3,1,30f,1);
                }
                if(cnt>240)rg.velocity=new Vector2(10,0);
                break;
            }
        }
        if(blueCnt>5)GetComponent<Renderer>().material.shader=Shader.Find("Unlit/Transparent Cutout");
        if(hp<=0 && explodeCnt>=100){
            explodeCnt=0;
            animator.SetTrigger("Explode");
            GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
            GetComponent<Renderer>().material.color=Color.white;
        }
        if(explodeCnt==48){
            enemyManager.GetComponent<EnemyManager>().enemy.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        cnt++;
        blueCnt++;
        explodeCnt++;
    }
    public void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.tag=="JBullet"){
            hp--;
            GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
            blueCnt=0;
        }
    }
    private void Move(int typeM){
        switch(typeM){
            case 1:
            if(cnt<180){
                rg.velocity=new Vector2(-10,0);
            }else{
                rg.velocity=new Vector2(0,0);
                if(explodeCnt>=100)animator.SetTrigger("Stop");
            }
            break;
        }
    }
}