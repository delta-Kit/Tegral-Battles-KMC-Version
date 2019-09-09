using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jiki : MonoBehaviour
{
    public GameObject game;
    private Rigidbody2D rg;
    private float walkSpeed=1f;
    private Animator animator;
    private bool flag=false;
    private int cnt;
    // Start is called before the first frame update
    void Start()
    {
        rg=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        game=GameObject.Find("Game");
        cnt=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal")!=0 && Input.GetAxisRaw("Vertical")!=0)walkSpeed=(float)1/(float)Math.Sqrt(2);
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
        cnt++;
    }
}