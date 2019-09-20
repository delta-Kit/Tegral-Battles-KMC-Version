using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Jiki : MonoBehaviour
{
    public GameObject game;
    private Rigidbody2D rg;
    private float walkSpeed=1f;
    private Animator animator;
    private bool flag=false;
    private int cnt;
    private int bombCnt;
    private Vector3 bombPosition;
    private int bomb;
    GameObject[] bombImage= new GameObject[3];
    public GameObject hitCircle;
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
        if(Input.GetKey(KeyCode.Z) && cnt%5==0){
            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(1,100f,Mathf.Deg2Rad*30,0,false,this.gameObject.transform.position+new Vector3(4,2,0));
            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(1,100f,Mathf.Deg2Rad*(-30),0,false,this.gameObject.transform.position+new Vector3(4,-2,0));
        }
        if(Input.GetKey(KeyCode.Z) && cnt%10==0){
            game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(2,120f,0,0,false,this.gameObject.transform.position+new Vector3(4,0,0));
        }
        if(Input.GetKeyDown(KeyCode.X) && bomb>0 && bombCnt>180){
            bombCnt=0;
            bombPosition=this.gameObject.transform.position;
            bomb--;
            bombImage[bomb].GetComponent<Image>().enabled=false;
        }
        if(bombCnt<60 && bombCnt%5==0)game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(3,0,0,16,true,bombPosition);
        if(this.gameObject.transform.position.x<-43)this.gameObject.transform.position=new Vector3(-43,this.gameObject.transform.position.y,0);
        if(this.gameObject.transform.position.x>43)this.gameObject.transform.position=new Vector3(43,this.gameObject.transform.position.y,0);
        if(this.gameObject.transform.position.y<-17)this.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,-17,0);
        if(this.gameObject.transform.position.y>17)this.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,17,0);
        hitCircle.transform.position=this.gameObject.transform.position;
        bombCnt++;
        cnt++;
    }
}