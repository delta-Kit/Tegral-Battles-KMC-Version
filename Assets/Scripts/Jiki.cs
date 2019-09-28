using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jiki : MonoBehaviour
{
    public GameObject game;
    private Rigidbody2D rg;
    private float walkSpeed=1f;
    private Animator animator;
    private bool flag=false;
    private int cnt;
    public int bombCnt;
    private Vector3 bombPosition;
    private int bomb;
    GameObject[] bombImage=new GameObject[3];
    public GameObject hitCircle;
    private int life;
    private int hitCnt;
    public GameObject hitEffect;
    GameObject[] lifeImage=new GameObject[5];
    public AudioClip hit,bombSound;
    // Start is called before the first frame update
    void Start()
    {
        rg=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        game=GameObject.Find("Game");
        cnt=0;
        bombCnt=300;
        bomb=3;
        bombImage[0]=GameObject.Find("Bomb1");
        bombImage[1]=GameObject.Find("Bomb2");
        bombImage[2]=GameObject.Find("Bomb3");
        life=6;
        hitCnt=200;
        lifeImage[0]=GameObject.Find("Life1");
        lifeImage[1]=GameObject.Find("Life2");
        lifeImage[2]=GameObject.Find("Life3");
        lifeImage[3]=GameObject.Find("Life4");
        lifeImage[4]=GameObject.Find("Life5");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            if(Input.GetAxisRaw("Horizontal")!=0 && Input.GetAxisRaw("Vertical")!=0){
                walkSpeed=(float)0.5/(float)Math.Sqrt(2);
            }else{
                walkSpeed=0.5f;
            }
            hitCircle.SetActive(true);
        }else{
             if(Input.GetAxisRaw("Horizontal")!=0 && Input.GetAxisRaw("Vertical")!=0){
                walkSpeed=(float)1/(float)Math.Sqrt(2);
            }else{
                walkSpeed=1f;
            }
            hitCircle.SetActive(false);
        }
        this.gameObject.transform.position+=new Vector3(walkSpeed*Input.GetAxisRaw("Horizontal"),0,0);
        this.gameObject.transform.position+=new Vector3(0,walkSpeed*Input.GetAxisRaw("Vertical"),0);
        if(!flag && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))){
            animator.SetTrigger("Move");
            flag=true;
        }
        if(flag && Input.GetAxisRaw("Horizontal")==0 && Input.GetAxisRaw("Vertical")==0){
            animator.SetTrigger("Stop");
            flag=false;
        }
        if(Input.GetKey(KeyCode.Z))game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,1,5,100f,0,0);
        if(Input.GetKeyDown(KeyCode.X) && bomb>0 && bombCnt>=180){
            bombCnt=0;
            bombPosition=this.gameObject.transform.position;
            bomb--;
            bombImage[bomb].GetComponent<Image>().enabled=false;
            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletDelete();
            if(hitCnt<8)hitCnt=200;
            gameObject.GetComponent<AudioSource>().clip=bombSound;
            GetComponent<AudioSource>().Play();
        }
        if(bombCnt<60)game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(bombPosition,2,5,0,102,0);
        if(bombCnt==180)game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletDelete();
        if(this.gameObject.transform.position.x<-43)this.gameObject.transform.position=new Vector3(-43,this.gameObject.transform.position.y,0);
        if(this.gameObject.transform.position.x>43)this.gameObject.transform.position=new Vector3(43,this.gameObject.transform.position.y,0);
        if(this.gameObject.transform.position.y<-17)this.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,-17,0);
        if(this.gameObject.transform.position.y>17)this.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,17,0);
        hitCircle.transform.position=this.gameObject.transform.position;
        if(hitCnt<60){
            hitEffect.SetActive(true);
        }else{
            hitEffect.SetActive(false);
        }
        if(hitCnt==8){
            life--;
            if(life>0){
                lifeImage[life-1].GetComponent<Image>().enabled=false;
            }else{
                Invoke("ChangeScene",0);
            }
            bomb=3;
            for(int i=0;i<3;i++)bombImage[i].GetComponent<Image>().enabled=true;
        }
        bombCnt++;
        cnt++;
        hitCnt++;
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag=="Bullet" && hitCnt>180 && bombCnt>=180){
            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletDelete();
            hitCnt=0;
            gameObject.GetComponent<AudioSource>().clip=hit;
            GetComponent<AudioSource>().Play();
        }
    }
    void ChangeScene(){
        SceneManager.LoadScene("GameOver");
    }
}