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
    public int hp;
    private bool isBlue;
    private int blueCnt;
    public GameObject enemyManager;
    public GameObject game;
    private int explodeCnt;
    public int note;
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
            hp=20;
            break;
            case 2:
            hp=10;
            break;
            case 3:
            hp=30;
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
        if(hp>0){
            switch(type){
                case 1:
                Move(1);
                if(cnt==210){
                    game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,3,1,30f,1,0);
                }
                break;
                case 2:
                Move(2);
                if(cnt>=150 && this.gameObject.transform.position.y>=-20 && this.gameObject.transform.position.y<=20)game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,4,120,30f,2,note);
                break;
                case 3:
                Move(3);
                if(cnt==210){
                    game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,5,1,30f,3,0);
                }
                break;
            }
        }
        if(blueCnt>5){
            GetComponent<Renderer>().material.shader=Shader.Find("Unlit/Transparent Cutout");
        }else{
            GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
        }
        if(hp<=0 && explodeCnt>=100){
            explodeCnt=0;
            animator.SetTrigger("Explode");
            GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
            GetComponent<Renderer>().material.color=Color.white;
            GetComponent<AudioSource>().Play();
        }
        if((this.gameObject.transform.position.x<-50 || this.gameObject.transform.position.y>25 || this.gameObject.transform.position.y<-25) && cnt>200){
            enemyManager.GetComponent<EnemyManager>().enemy.Remove(this.gameObject);
            Destroy(this.gameObject);
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
        if(col.gameObject.tag=="JBullet" && this.gameObject.transform.position.x<=45 && this.gameObject.transform.position.x>=-45 && this.gameObject.transform.position.y>=-20 && this.gameObject.transform.position.y<=20 && hp>0){
            hp--;
            blueCnt=0;
            if(col.gameObject.GetComponent<Bullet>().type!=3){
                game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().bullet.Remove(col.gameObject);
                Destroy(col.gameObject);
            }
        }
    }
    private void Move(int typeM){
        switch(typeM){
            case 1:
            if(cnt<180){
                rg.velocity=new Vector2(-8,0);
            }else if(cnt<240){
                rg.velocity=new Vector2(0,0);
                if(explodeCnt>=100)animator.SetTrigger("Stop");
            }else{
                rg.velocity=new Vector2(10,0);
            }
            break;
            case 2:
            if(cnt<200){
                rg.velocity=new Vector2(-10,0);
            }else{
                rg.velocity=new Vector2(-10,vy);
            }
            break;
            case 3:
            if(cnt<180){
                rg.velocity=new Vector2(-vx,-vy);
            }else if(cnt<240){
                rg.velocity=new Vector2(0,0);
                if(explodeCnt>=100)animator.SetTrigger("Stop");
            }else{
                rg.velocity=new Vector2(vx,vy);
            }
            break;
        }
    }
}