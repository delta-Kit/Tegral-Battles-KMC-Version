using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jiki : MonoBehaviour
{
    public BulletManager bulletManager;
    private Rigidbody2D rg;
    private float walkSpeed=1f;
    private Animator animator;
    private bool flag=false;
    private int cnt;
    public int bombCnt;
    private Vector3 bombPosition;
    protected static int bomb;
    Image[] bombImage=new Image[3];
    public GameObject hitCircle;
    protected static int life;
    public int hitCnt;
    public GameObject hitEffect;
    Image[] lifeImage=new Image[5];
    public AudioClip hit,bombSound;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        cnt = 0;
        bombCnt = 300;
        bombImage[0] = GameObject.Find("Bomb1").GetComponent<Image>();
        bombImage[1] = GameObject.Find("Bomb2").GetComponent<Image>();
        bombImage[2] = GameObject.Find("Bomb3").GetComponent<Image>();
        hitCnt = 200;
        lifeImage[0] = GameObject.Find("Life1").GetComponent<Image>();
        lifeImage[1] = GameObject.Find("Life2").GetComponent<Image>();
        lifeImage[2] = GameObject.Find("Life3").GetComponent<Image>();
        lifeImage[3] = GameObject.Find("Life4").GetComponent<Image>();
        lifeImage[4] = GameObject.Find("Life5").GetComponent<Image>();
        if(life > 0)for(int i = life - 1; i < 5; i++)lifeImage[i].enabled = false;
        for(int i = bomb; i < 3; i++)bombImage[i].enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            if(Input.GetAxisRaw("Horizontal")!=0 && Input.GetAxisRaw("Vertical")!=0){
                walkSpeed=(float)0.3/(float)Math.Sqrt(2);
            }else{
                walkSpeed=0.2f;
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
        if(Input.GetKey(KeyCode.Z))bulletManager.BulletAppear(this.gameObject.transform.position,1,5,100f,0,0,0, 0);
        if(Input.GetKeyDown(KeyCode.X) && bomb>0 && bombCnt>=180 && hitCnt>=180){
            bombCnt=0;
            bombPosition=this.gameObject.transform.position;
            bomb--;
            bombImage[bomb].enabled=false;
            bulletManager.BulletDelete();
            if(hitCnt<8)hitCnt=200;
            gameObject.GetComponent<AudioSource>().clip=bombSound;
            GetComponent<AudioSource>().Play();
        }
        if(bombCnt<60)bulletManager.BulletAppear(bombPosition,2,5,0,102,0,0, 0);
        if(bombCnt==180)bulletManager.BulletDelete();
        if(this.gameObject.transform.position.x < -34.4f)this.gameObject.transform.position = new Vector3(-34.4f, this.gameObject.transform.position.y, 0);
        if(this.gameObject.transform.position.x > 34.4f)this.gameObject.transform.position = new Vector3(34.4f, this.gameObject.transform.position.y, 0);
        if(this.gameObject.transform.position.y < -18.3f)this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -18.3f, 0);
        if(this.gameObject.transform.position.y > 18.3f)this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 18.3f, 0);
        hitCircle.transform.position=this.gameObject.transform.position;
        if(hitCnt<60){
            hitEffect.SetActive(true);
        }else{
            hitEffect.SetActive(false);
        }
        if(hitCnt==8){
            life--;
            if(life>0){
                lifeImage[life-1].enabled=false;
            }else{
                Invoke("ChangeScene",0);
            }
            bomb=3;
            for(int i=0;i<3;i++)bombImage[i].enabled=true;
            bulletManager.BulletDelete();
        }
        bombCnt++;
        cnt++;
        hitCnt++;
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Bullet" && hitCnt > 180 && bombCnt > 180){
            hitCnt = 0;
            gameObject.GetComponent<AudioSource>().clip = hit;
            GetComponent<AudioSource>().Play();
        }
    }
    void ChangeScene(){
        SceneManager.LoadScene("GameOver");
    }
    public static KeyValuePair<int, int> GetLifeBomb(){
        return new KeyValuePair<int, int> (life, bomb);
    }
    public static void SetLifeBomb(int Life, int Bomb){
        life = Life;
        bomb = Bomb;
    }
}