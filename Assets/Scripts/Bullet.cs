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
    public Sprite[] bullet=new Sprite[4];
    SpriteRenderer  bulletSpriteRenderer;
    private bool isAdditive;
    Quaternion rot;
    public GameObject game;
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
        if(isCircle){
            this.gameObject.GetComponent<CircleCollider2D>().enabled=true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
            switch(type){
                case 3:
                this.gameObject.GetComponent<CircleCollider2D>().radius=16;
                break;
                case 4:
                case 5:
                case 6:
                this.gameObject.GetComponent<CircleCollider2D>().radius=2;
                break;
            }
        }else{
            this.gameObject.GetComponent<CircleCollider2D>().enabled=false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled=true;
            switch(type){
                case 1:
                this.gameObject.GetComponent<BoxCollider2D>().size=new Vector2(4,2);
                break;
                case 2:
                this.gameObject.GetComponent<BoxCollider2D>().size=new Vector2(12,16);
                break;
            }
        }
        if(color>100){
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
            this.gameObject.tag="Bullet";
            break;
        }
        if(isAdditive){
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Surface");
        }else{
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Standard");
        }
        game=GameObject.Find("Game");
        switch(type){
            case 3:
            this.transform.localScale=new Vector3(r/16,r/16,1f);
            break;
            case 4:
            case 5:
            case 6:
            this.transform.localScale=new Vector3(r/2,r/2,1f);
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
            Delete(1);
            break;
            case 6:
            Delete(3);
            break;
        }
        if(!isCircle)transform.rotation=Quaternion.Euler(0,0,rad/Mathf.Deg2Rad);
        switch(type){
            case 1:
            if(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x>-60){
                if(rad>Mathf.Atan2(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad-=Mathf.Deg2Rad*3;
                }
                if(rad<Mathf.Atan2(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
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
            case 4:
            break;
            case 5:
            if(v<40)v--;
            break;
            case 6:
            break;
        }
        rg.velocity=new Vector2(v*Mathf.Cos(rad),v*Mathf.Sin(rad));
        switch(type){
            case 3:
            this.transform.localScale=new Vector3(r/16,r/16,1f);
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