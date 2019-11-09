using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject BulletSpawner;
    public GameObject jiki;
    private bool resetFlag;
    private bool changeFlag;
    // Start is called before the first frame update
    void Start()
    {
        rg=this.gameObject.GetComponent<Rigidbody2D>();
        enemySpriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        if(type<100){
            this.gameObject.GetComponent<CircleCollider2D>().radius=2;
        }else if(type<200){
            this.gameObject.GetComponent<CircleCollider2D>().radius=2.5f;
        }else{
            this.gameObject.GetComponent<CircleCollider2D>().radius=3;
            this.transform.localScale=new Vector3(1.5f,1.5f,1f);
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
            case 201:
            hp=700;
            break;
        }
        blueCnt=10;
        enemyManager=GameObject.Find("EnemyManager");
        game=GameObject.Find("Game");
        explodeCnt=100;
        cnt=0;
        jiki=GameObject.Find("TegralK1");
        resetFlag=false;
        changeFlag=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp>0){
            switch(type){
                case 1:
                Move(1);
                if(cnt==210){
                    game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,3,1,30f,1,0,0);
                }
                break;
                case 2:
                Move(2);
                if(cnt>=150 && this.gameObject.transform.position.y>=-20 && this.gameObject.transform.position.y<=20)game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,4,120,30f,2,note,0);
                break;
                case 3:
                Move(3);
                if(cnt==210){
                    game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,5,1,30f,3,0,0);
                }
                break;
                case 201:
                Move(4);
                if(cnt>=300){
                    if(hp>350){
                        if(cnt==300){
                            GameObject b1=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().bulletSpawner.Add(b1);
                            b1.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b1.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b1.GetComponent<BulletSpawner>().type=1;
                            b1.GetComponent<BulletSpawner>().interval=2;
                            b1.GetComponent<BulletSpawner>().note=1;
                            GameObject b2=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().bulletSpawner.Add(b2);
                            b2.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b2.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b2.GetComponent<BulletSpawner>().type=1;
                            b2.GetComponent<BulletSpawner>().interval=2;
                            b2.GetComponent<BulletSpawner>().note=2;
                            GameObject b3=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().bulletSpawner.Add(b3);
                            b3.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b3.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b3.GetComponent<BulletSpawner>().type=1;
                            b3.GetComponent<BulletSpawner>().interval=2;
                            b3.GetComponent<BulletSpawner>().note=3;
                            GameObject b4=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().bulletSpawner.Add(b4);
                            b4.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b4.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b4.GetComponent<BulletSpawner>().type=1;
                            b4.GetComponent<BulletSpawner>().interval=2;
                            b4.GetComponent<BulletSpawner>().note=4;
                        }else if(cnt>600){
                            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletMove(1);
                        }
                        if(jiki.GetComponent<Jiki>().hitCnt<60 || jiki.GetComponent<Jiki>().bombCnt<180)resetFlag=true;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180){
                            cnt=299;
                            resetFlag=false;
                        }
                    }else{
                        if(!changeFlag){
                            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletDelete();
                            GameObject b=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().bulletSpawner.Add(b);
                            b.GetComponent<BulletSpawner>().boxX=this.gameObject.transform.position.x-1.7696286f;
                            b.GetComponent<BulletSpawner>().boxY=this.gameObject.transform.position.y+17.489627f;
                            b.GetComponent<BulletSpawner>().type=2;
                            b.GetComponent<BulletSpawner>().interval=2;
                            changeFlag=true;
                        }
                        if(jiki.GetComponent<Jiki>().hitCnt<60 || jiki.GetComponent<Jiki>().bombCnt<180)resetFlag=true;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180){
                            changeFlag=false;
                            resetFlag=false;
                        }
                    }
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
            if(type>200){
                animator.SetTrigger("BossExplode");
                this.transform.localScale=new Vector3(30f,30f,1f);
                Invoke("ChangeScene",1f);
            }else{
                animator.SetTrigger("Explode");
            }
            GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
            GetComponent<Renderer>().material.color=Color.white;
            GetComponent<AudioSource>().Play();
        }
        if((this.gameObject.transform.position.x<-80 || this.gameObject.transform.position.y>25 || this.gameObject.transform.position.y<-25 || this.gameObject.transform.position.x>80) && cnt>200){
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
            if(jiki.GetComponent<Jiki>().bombCnt<180){
                if(cnt%10==0)hp--;
            }else{
                hp--;
            }
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
            case 4:
            if(cnt<300){
                rg.velocity=new Vector2(-vx,0);
            }else{
                rg.velocity=new Vector2(0,0);
                if(explodeCnt>=100)animator.SetTrigger("Stop");
            }
            break;
        }
    }
    void ChangeScene(){
        SceneManager.LoadScene("Clear");
    }
}