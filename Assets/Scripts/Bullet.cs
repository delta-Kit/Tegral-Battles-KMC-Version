using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{   private Rigidbody2D rg;
    public int type;
    public float v;
    public float rad;
    public float r;
    public bool isCircle;
    public Sprite jBullet2;
    public Sprite jBullet3;
    public Sprite[] bullet=new Sprite[4], laser = new Sprite[2];
    SpriteRenderer  bulletSpriteRenderer;
    private bool isAdditive;
    Quaternion rot;
    public GameObject enemyManager;
    private int cnt;
    public int color;
    public GameObject bulletManager;
    public int note;
    // Start is called before the first frame update
    void Start()
    {
        rg=this.gameObject.GetComponent<Rigidbody2D>();
        bulletSpriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        isAdditive=true;
        transform.rotation=Quaternion.Euler(0,0,rad/Mathf.Deg2Rad);
        if(type > 200){
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            this.gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(24, 2);
        }
        else if(isCircle){
            this.gameObject.GetComponent<CircleCollider2D>().enabled=true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            switch(type){
                case 3:
                this.gameObject.GetComponent<CircleCollider2D>().radius=16;
                break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                this.gameObject.GetComponent<CircleCollider2D>().radius=2;
                break;
            }
        }else{
            this.gameObject.GetComponent<CircleCollider2D>().enabled=false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled=true;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            switch(type){
                case 1:
                this.gameObject.GetComponent<BoxCollider2D>().size=new Vector2(4,2);
                break;
                case 2:
                this.gameObject.GetComponent<BoxCollider2D>().size=new Vector2(12,16);
                break;
            }
        }
        if(type>200){
                bulletSpriteRenderer.sprite = laser[color];
        }
        else if(color>100){
            switch(color){
                case 101:
                bulletSpriteRenderer.sprite=jBullet2;
                break;
                case 102:
                bulletSpriteRenderer.sprite=jBullet3;
                break;
            }
        }else{
            bulletSpriteRenderer.sprite=bullet[color];
        }
        switch(type){
            case 1:
            case 2:
            case 3:
            this.gameObject.tag="JBullet";
            break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 201:
            this.gameObject.tag="Bullet";
            break;
        }
        if(isAdditive){
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Surface");
        }else{
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Standard");
        }
        enemyManager = GameObject.Find("EnemyManager");
        switch(type){
            case 3:
            this.transform.localScale=new Vector3(r/16,r/16,1f);
            break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            this.transform.localScale=new Vector3(r/2,r/2,1f);
            break;
            case 201:
            this.transform.localScale = new Vector3(1 / this.gameObject.GetComponent<CapsuleCollider2D>().size.x, 0.5f, 1f);
            break;
        }
        bulletManager=GameObject.Find("BulletManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(isAdditive){
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Surface");
        }else{
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Standard");
        }
        switch(type){
            case 1:
            case 2:
            case 4:
            case 5:
            case 7:
            case 8:
            Delete(1);
            break;
            case 6:
            Delete(3);
            break;
            case 201:
            Delete(1.3f);
            break;
        }
        if(!isCircle)transform.rotation=Quaternion.Euler(0,0,rad/Mathf.Deg2Rad);
        switch(type){
            case 1:
            if(enemyManager.GetComponent<EnemyManager>().GetEnemyPosition().x>-60){
                if(rad>Mathf.Atan2(enemyManager.GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,enemyManager.GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad-=Mathf.Deg2Rad*3;
                }
                if(rad<Mathf.Atan2(enemyManager.GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,enemyManager.GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad+=Mathf.Deg2Rad*3;
                }
            }
            break;
            case 3:
            if(cnt>60){
                r=Mathf.Pow(2f,(float)cnt/15);
                if(r>2000){
                    bulletManager.GetComponent<BulletManager>().bullet.Remove(this.gameObject);
                    Destroy(this.gameObject);
                }
            }
            break;
            case 5:
            if(v<40)v--;
            break;
            case 6:
            if(cnt < 60)rad += note * Mathf.Deg2Rad;
            if(cnt == 40){
                for(int i = -3; i <= 3; i ++){
                    bulletManager.GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position, 9, 1, v, 2, 0, rad + Mathf.Deg2Rad * (180 + i * 8) + Random.Range(-0.1f, 0.1f));
                }
                bulletManager.GetComponent<BulletManager>().bullet.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
            break;
            case 7:
            if(cnt>=120)v=8f;
            break;
            case 8:
            if(cnt < 60)rad += Mathf.Deg2Rad * note * 1.5f;
            break;
        }
        rg.velocity=new Vector2(v*Mathf.Cos(rad),v*Mathf.Sin(rad));
        switch(type){
            case 3:
            this.transform.localScale=new Vector3(r/16,r/16,1f);
            break;
            case 201:
            if(cnt < 30)this.transform.localScale += new Vector3(v / 1000, 0, 0);
            break;
        }
        cnt++;
    }
    private void Delete(float size){
        if(this.gameObject.transform.position.x<-45*size || this.gameObject.transform.position.x>45*size || this.gameObject.transform.position.y<-21*size || this.gameObject.transform.position.y>21*size){
            bulletManager.GetComponent<BulletManager>().bullet.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}