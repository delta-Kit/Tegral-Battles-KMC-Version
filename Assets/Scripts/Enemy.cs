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
    public EnemyManager enemyManager;
    private int explodeCnt;
    public int note;
    public GameObject BulletSpawner;
    public GameObject jiki;
    private bool resetFlag;
    private bool changeFlag;
    private float rad;
    private int cnt2;
    public BulletManager bulletManager;
    // Start is called before the first frame update
    void Start()
    {
        rg=this.gameObject.GetComponent<Rigidbody2D>();
        enemySpriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        if(type<100){
            this.gameObject.GetComponent<CircleCollider2D>().radius=2;
        }else if(type<200){
            this.gameObject.GetComponent<CircleCollider2D>().radius=2.5f;
            this.transform.localScale=new Vector3(1.25f,1.25f,1f);
        }else{
            this.gameObject.GetComponent<CircleCollider2D>().radius=3;
            this.transform.localScale=new Vector3(1.5f,1.5f,1f);
        }
        animator=GetComponent<Animator>();
        switch(type){
            case 202:
            animator.SetTrigger("Move2");
            break;
        }
        switch(type){
            case 1:
            case 4:
            hp=20;
            break;
            case 2:
            case 6:
            hp=10;
            break;
            case 3:
            case 5:
            case 7:
            hp=30;
            break;
            case 101:
            hp = 300;
            break;
            case 201:
            hp=1500;
            break;
            case 202:
            hp = 1400;
            break;
        }
        blueCnt=10;
        enemyManager=GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        explodeCnt=100;
        cnt=0;
        jiki=GameObject.Find("TegralK1");
        resetFlag=false;
        changeFlag = false;
        rad=180*Mathf.Deg2Rad;
        cnt2=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp>0){
            switch(type){
                case 1:
                Move(1);
                if(cnt==210){
                    bulletManager.BulletAppear(this.gameObject.transform.position,3,1,30f,1,0,0, 0);
                }
                break;
                case 2:
                Move(2);
                if(cnt>=150 && this.gameObject.transform.position.y>=-20 && this.gameObject.transform.position.y<=20)bulletManager.BulletAppear(this.gameObject.transform.position,4,120,30f,2,note,Mathf.Atan2(jiki.transform.position.y - this.gameObject.transform.position.y, jiki.transform.position.x - this.gameObject.transform.position.x), 0);
                break;
                case 3:
                Move(3);
                if(cnt==210){
                    bulletManager.BulletAppear(this.gameObject.transform.position,5,1,30f,3,0,0, 0);
                }
                break;
                case 4:
                Move(1);
                if(cnt == 210){
                    bulletManager.BulletAppear(this.gameObject.transform.position, 201, 1, 30, 0, 0, Mathf.Atan2(jiki.transform.position.y - this.gameObject.transform.position.y, jiki.transform.position.x - this.gameObject.transform.position.x), 30);
                }
                break;
                case 5:
                Move(1);
                if(cnt >= 180 && cnt < 240){
                    bulletManager.BulletAppear(this.gameObject.transform.position, 201, 5, 20, 1, 0, cnt * 6 * Mathf.Deg2Rad, 30);
                }
                break;
                case 6:
                Move(6);
                if(cnt % 23 == 0){
                    bulletManager.BulletAppear(this.gameObject.transform.position, 9, 1, 20, 3, 0, Mathf.PI, 0.5f);
                }
                break;
                case 7:
                Move(3);
                if(cnt == 210){
                    for(int i = 0; i < 12; i ++){
                        bulletManager.BulletAppear(this.gameObject.transform.position, 201, 1, 30, 2, 0, Mathf.Atan2(jiki.gameObject.transform.position.y - this.gameObject.transform.position.y, jiki.gameObject.transform.position.x - this.gameObject.transform.position.y) + Mathf.Deg2Rad * i * 30, 20);
                    }
                }
                break;
                case 101:
                Move(5);
                if(cnt >= 240 && cnt < 840){
                    switch(cnt % 480){
                        case 240:
                        float rad0 = Random.Range(0, 2 * Mathf.PI);
                        for(int i = 0; i < 12; i ++){
                            bulletManager.BulletAppear(this.gameObject.transform.position, 8, 1, 30, 1, 1, i * Mathf.Deg2Rad * 30 + rad0, 0);
                        }
                        break;
                        case 360:
                        rad0 = Random.Range(0, 2 * Mathf.PI);
                        for(int i = 0; i < 6; i ++){
                            for(int j = 0; j < 7; j++){
                                bulletManager.BulletAppear(this.gameObject.transform.position, 10, 1, 16 + j * 2, 1, 1, i * Mathf.Deg2Rad * 60 + rad0, 0);
                            }
                        }
                        rad0 = Random.Range(0, 2 * Mathf.PI);
                        for(int i = 0; i < 6; i ++){
                            for(int j = 0; j < 7; j++){
                                bulletManager.BulletAppear(this.gameObject.transform.position, 10, 1, 16 + j * 3, 2, -1, i * Mathf.Deg2Rad * 60 + rad0 + 30 * Mathf.Deg2Rad, 0);
                            }
                        }
                        break;
                        case 0:
                        rad0 = Random.Range(0, 2 * Mathf.PI);
                        for(int i = 0; i < 12; i ++){
                            bulletManager.BulletAppear(this.gameObject.transform.position, 8, 1, 30, 1, -1, i * Mathf.Deg2Rad * 30 + rad0, 0);
                        }
                        break;
                        case 120:
                        rad0 = Random.Range(0, 2 * Mathf.PI);
                        for(int i = 0; i < 6; i ++){
                            for(int j = 0; j < 7; j++){
                                bulletManager.BulletAppear(this.gameObject.transform.position, 10, 1, 16 + j * 2, 1, -1, i * Mathf.Deg2Rad * 60 + rad0, 0);
                            }
                        }
                        for(int i = 0; i < 6; i ++){
                            for(int j = 0; j < 7; j++){
                                bulletManager.BulletAppear(this.gameObject.transform.position, 10, 1, 16 + j * 3, 2, 1, i * Mathf.Deg2Rad * 60 + rad0 + 30 * Mathf.Deg2Rad, 0);
                            }
                        }
                        break;
                    }
                }
                break;
                case 201:
                Move(4);
                if(cnt>=300){
                    if(hp>750){
                        if(cnt2<600)hp=1500;
                        if(cnt==300){
                            GameObject b1=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b1);
                            b1.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b1.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b1.GetComponent<BulletSpawner>().type=1;
                            b1.GetComponent<BulletSpawner>().interval=2;
                            b1.GetComponent<BulletSpawner>().note=1;
                            GameObject b2=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b2);
                            b2.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b2.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b2.GetComponent<BulletSpawner>().type=1;
                            b2.GetComponent<BulletSpawner>().interval=2;
                            b2.GetComponent<BulletSpawner>().note=2;
                            GameObject b3=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b3);
                            b3.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b3.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b3.GetComponent<BulletSpawner>().type=1;
                            b3.GetComponent<BulletSpawner>().interval=2;
                            b3.GetComponent<BulletSpawner>().note=3;
                            GameObject b4=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b4);
                            b4.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b4.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b4.GetComponent<BulletSpawner>().type=1;
                            b4.GetComponent<BulletSpawner>().interval=2;
                            b4.GetComponent<BulletSpawner>().note=4;
                        }else if(cnt>600){
                            bulletManager.BulletMove(1);
                        }
                        if(jiki.GetComponent<Jiki>().hitCnt<60 || jiki.GetComponent<Jiki>().bombCnt<180)resetFlag=true;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180){
                            cnt=299;
                            resetFlag=false;
                        }
                        if(cnt2 > 2400)hp =350;
                    }else{
                        if(!changeFlag){
                            bulletManager.BulletDelete();
                            GameObject b=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b);
                            b.GetComponent<BulletSpawner>().boxX=this.gameObject.transform.position.x - 10;
                            b.GetComponent<BulletSpawner>().boxY=this.gameObject.transform.position.y + 10;
                            b.GetComponent<BulletSpawner>().type=2;
                            b.GetComponent<BulletSpawner>().interval=3;
                            changeFlag=true;
                            cnt2=0;
                        }
                        if(cnt2<600)hp = 750;
                        if(jiki.GetComponent<Jiki>().hitCnt<60 || jiki.GetComponent<Jiki>().bombCnt<180)resetFlag=true;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180){
                            changeFlag=false;
                            resetFlag=false;
                        }
                        if(cnt2 > 2100)hp = 0;
                    }
                }
                break;
                case 202:
                Move(4);
                if(cnt >= 300){
                    if(hp > 2100){
                        if(cnt2 < 600)hp = 2450;
                        if(cnt % 300 == 0){
                            GameObject b1=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b1);
                            b1.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b1.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b1.GetComponent<BulletSpawner>().type=3;
                            b1.GetComponent<BulletSpawner>().interval=1;
                            b1.GetComponent<BulletSpawner>().note=1;
                            GameObject b2=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b2);
                            b2.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b2.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b2.GetComponent<BulletSpawner>().type=3;
                            b2.GetComponent<BulletSpawner>().interval=1;
                            b2.GetComponent<BulletSpawner>().note=2;
                            GameObject b3=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b3);
                            b3.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b3.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b3.GetComponent<BulletSpawner>().type=3;
                            b3.GetComponent<BulletSpawner>().interval=1;
                            b3.GetComponent<BulletSpawner>().note=3;
                            GameObject b4=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b4);
                            b4.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b4.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b4.GetComponent<BulletSpawner>().type=3;
                            b4.GetComponent<BulletSpawner>().interval=1;
                            b4.GetComponent<BulletSpawner>().note=4;   
                        }
                        if(cnt % 300 == 150){
                            GameObject b1=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b1);
                            b1.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b1.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b1.GetComponent<BulletSpawner>().type=3;
                            b1.GetComponent<BulletSpawner>().interval=1;
                            b1.GetComponent<BulletSpawner>().note=5;
                            GameObject b2=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b2);
                            b2.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b2.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b2.GetComponent<BulletSpawner>().type=3;
                            b2.GetComponent<BulletSpawner>().interval=1;
                            b2.GetComponent<BulletSpawner>().note=6;
                            GameObject b3=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b3);
                            b3.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b3.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b3.GetComponent<BulletSpawner>().type=3;
                            b3.GetComponent<BulletSpawner>().interval=1;
                            b3.GetComponent<BulletSpawner>().note=7;
                            GameObject b4=Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b4);
                            b4.GetComponent<BulletSpawner>().x0=this.gameObject.transform.position.x;
                            b4.GetComponent<BulletSpawner>().y0=this.gameObject.transform.position.y;
                            b4.GetComponent<BulletSpawner>().type=3;
                            b4.GetComponent<BulletSpawner>().interval=1;
                            b4.GetComponent<BulletSpawner>().note=8;
                        }
                        if(jiki.GetComponent<Jiki>().hitCnt<60 || jiki.GetComponent<Jiki>().bombCnt<180)resetFlag=true;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180)resetFlag=false;
                        if(cnt2 > 2100)hp = 2100;
                    }else if(hp > 1750){
                        if(!changeFlag){
                            bulletManager.BulletDelete();
                            GameObject b1 =Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b1);
                            b1.GetComponent<BulletSpawner>().y0 = b1.GetComponent<BulletSpawner>().boxY = this.gameObject.transform.position.y;
                            b1.GetComponent<BulletSpawner>().x0 = b1.GetComponent<BulletSpawner>().boxX = this.gameObject.transform.position.x;
                            b1.GetComponent<BulletSpawner>().type= 4;
                            b1.GetComponent<BulletSpawner>().interval= 3;
                            b1.GetComponent<BulletSpawner>().rad = Mathf.Atan2(jiki.transform.position.y - this.gameObject.transform.position.y, jiki.transform.position.x - this.gameObject.transform.position.x);
                            GameObject b2 =Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b2);
                            b2.GetComponent<BulletSpawner>().y0 = b2.GetComponent<BulletSpawner>().boxY = this.gameObject.transform.position.y;
                            b2.GetComponent<BulletSpawner>().x0 = b2.GetComponent<BulletSpawner>().boxX = this.gameObject.transform.position.x;
                            b2.GetComponent<BulletSpawner>().type= 4;
                            b2.GetComponent<BulletSpawner>().interval= 3;
                            b2.GetComponent<BulletSpawner>().rad = Mathf.Atan2(jiki.transform.position.y - this.gameObject.transform.position.y, jiki.transform.position.x - this.gameObject.transform.position.x) + 120 * Mathf.Deg2Rad;
                            GameObject b3 =Instantiate(BulletSpawner,this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b3);
                            b3.GetComponent<BulletSpawner>().y0 = b3.GetComponent<BulletSpawner>().boxY = this.gameObject.transform.position.y;
                            b3.GetComponent<BulletSpawner>().x0 = b3.GetComponent<BulletSpawner>().boxX = this.gameObject.transform.position.x;
                            b3.GetComponent<BulletSpawner>().type= 4;
                            b3.GetComponent<BulletSpawner>().interval= 3;
                            b3.GetComponent<BulletSpawner>().rad = Mathf.Atan2(jiki.transform.position.y - this.gameObject.transform.position.y, jiki.transform.position.x - this.gameObject.transform.position.x) + 240 * Mathf.Deg2Rad;
                            changeFlag=true;
                            cnt2=0;
                        }
                        if(cnt2<300)hp=2100;
                        if(jiki.GetComponent<Jiki>().hitCnt<60 || jiki.GetComponent<Jiki>().bombCnt<180)resetFlag=true;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180){
                            changeFlag=false;
                            resetFlag=false;
                        }
                        if(cnt2 > 2100)hp = 1750;
                    }else if(hp > 1400){
                        if(changeFlag){
                            bulletManager.BulletDelete();
                            changeFlag = false;
                            cnt2 = 0;
                            GameObject b = Instantiate(BulletSpawner, this.gameObject.transform.position,Quaternion.identity);
                            bulletManager.bulletSpawner.Add(b);
                            b.GetComponent<BulletSpawner>().boxX = this.gameObject.transform.position.x;
                            b.GetComponent<BulletSpawner>().boxY = this.gameObject.transform.position.y;
                            b.GetComponent<BulletSpawner>().type = 5;
                            b.GetComponent<BulletSpawner>().interval = 1;
                        }
                        if(cnt2 < 300)hp = 1750;
                        if(jiki.GetComponent<Jiki>().hitCnt<60 || jiki.GetComponent<Jiki>().bombCnt<180)resetFlag=true;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180){
                            changeFlag = true;
                            resetFlag=false;
                        }
                        if(cnt2 > 2100)hp = 1400;
                    }else if(hp > 1050){
                        if(!changeFlag){
                            bulletManager.BulletDelete();
                            changeFlag = true;
                            cnt2 = 0;
                        }
                        Move(8);
                        for(int i = 0; i < 360; i += 3){
                            float box1 = Mathf.Log(2 + Mathf.Cos(Mathf.Deg2Rad * i));
                            float box2 = Mathf.Log(2 + Mathf.Sin(Mathf.Deg2Rad * i));
                            float r = Mathf.Sqrt(box1 * box1 + box2 * box2);
                            float theta = Mathf.Atan2(box2, box1) + Mathf.Deg2Rad * 135;
                            float posX = -r * Mathf.Cos(theta) + 0.1f;
                            float posY = r * Mathf.Sin(theta) / 2;
                            r = Mathf.Sqrt(posX * posX + posY * posY);
                            theta = Mathf.Atan2(posY, posX) + Mathf.Atan2(jiki.transform.position.y - this.gameObject.transform.position.y, jiki.transform.position.x - this.gameObject.transform.position.x);
                            float x1 = r * Mathf.Cos(theta);
                            float y1 = r * Mathf.Sin(theta);
                            float angle2 = Mathf.Atan2(y1, x1);
                            bulletManager.BulletAppear(this.gameObject.transform.position + 8 * new Vector3(x1, y1, 0), 6, 120, 20 * Dist(new Vector3(0, 0, 0), new Vector3(x1, y1, 0)), 4, 0, angle2, 0.5f);
                        }
                        for(int i = 0; i < 12; i ++){
                            bulletManager.BulletAppear(this.gameObject.transform.position, 6, 110, 20, 1, note, Mathf.Deg2Rad * i * 30, 0.5f);
                        }
                        if(cnt2 < 300)hp = 1400;
                        if(resetFlag && jiki.GetComponent<Jiki>().hitCnt>=60 && jiki.GetComponent<Jiki>().bombCnt>=180){
                            changeFlag = false;
                            resetFlag=false;
                        }
                        if(cnt2 > 2100)hp = 1050;
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
        if(hp<=0 && explodeCnt >= 100){
            explodeCnt=0;
            if(type>200){
                animator.SetTrigger("BossExplode");
                this.transform.localScale=new Vector3(30f,30f,1f);
                Invoke("ChangeScene",1.0f);
            }else{
                animator.SetTrigger("Explode");
            }
            GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
            GetComponent<Renderer>().material.color=Color.white;
            GetComponent<AudioSource>().Play();
        }
        if((this.gameObject.transform.position.x<-80 || this.gameObject.transform.position.y>25 || this.gameObject.transform.position.y<-25 || this.gameObject.transform.position.x>80) && cnt>200){
            enemyManager.enemy.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        if(type > 200){
            if(explodeCnt == 95){
                enemyManager.enemy.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
        }else{
            if(explodeCnt==48){
                enemyManager.enemy.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
        }
        cnt++;
        cnt2++;
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
                bulletManager.bullet.Remove(col.gameObject);
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
            }else if(cnt<380){
                rad+=Mathf.Deg2Rad/2;
                rg.velocity=new Vector2(10*Mathf.Cos(rad),vy*10*Mathf.Sin(rad));
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
            case 5:
            if(cnt < 240){
                rg.velocity = new Vector2(-vx, 0);
            }else if(cnt < 840){
                rg.velocity = new Vector2(0, 0);
                if(explodeCnt > 100)animator.SetTrigger("Stop");
            }else{
                rg.velocity = new Vector2(2 * vx, 0);
            }
            break;
            case 6:
            rg.velocity = new Vector2(0, vy);
            break;
            case 7:
            if(this.gameObject.transform.position.x < 9){
                if(this.gameObject.transform.position.y < -1){
                    rg.velocity = new Vector2(5, 5);
                }else if(this.gameObject.transform.position.y > 1){
                    rg.velocity = new Vector2(5, -5);
                }else{
                    rg.velocity = new Vector2(5, 0);
                }
            }
            if(this.gameObject.transform.position.x > 11){
                if(this.gameObject.transform.position.y < -1){
                    rg.velocity = new Vector2(-5, 5);
                }else if(this.gameObject.transform.position.y > 1){
                    rg.velocity = new Vector2(-5, -5);
                }else{
                    rg.velocity = new Vector2(-5, 0);
                }
            }
            break;
            case 8:
            if(cnt2 % 960 < 60 || (cnt2 % 960 >= 720 && cnt2 % 960 < 780))rg.velocity = new Vector2(0, 15 * Mathf.Sin((Mathf.Deg2Rad * cnt2 % 60) * 3));
            if((cnt2 % 960 >= 240 && cnt2 % 960 < 300) || (cnt2 % 960 >= 480 && cnt2 % 960 < 540)){
                rg.velocity = new Vector2(0,-15 * Mathf.Sin((Mathf.Deg2Rad * cnt2 % 60) * 3));
            }
            break;
        }
    }
    void ChangeScene(){
        SceneManager.LoadScene("Clear");
    }

    float Dist(Vector3 x1, Vector3 x2){
        return Mathf.Sqrt((x1.x - x2.x) * (x1.x - x2.x) + (x1.y - x2.y) * (x1.y - x2.y));
    }
}