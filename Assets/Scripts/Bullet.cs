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
            this.gameObject.GetComponent<CircleCollider2D>().radius=r;
        }else{
            this.gameObject.GetComponent<CircleCollider2D>().enabled=false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled=true;
            switch(type){
                case 1:
                this.gameObject.GetComponent<BoxCollider2D>().size=new Vector2(4,2);
                break;
                case 2:
                this.gameObject.GetComponent<BoxCollider2D>().size=new Vector2(16,16);
                break;
            }
        }
        switch(type){
            case 1:
            bulletSpriteRenderer.sprite=bullet[0];
            break;
            case 2:
            bulletSpriteRenderer.sprite=jBullet2;
            break;
            case 3:
            bulletSpriteRenderer.sprite=jBullet3;
            break;
            case 4:
            bulletSpriteRenderer.sprite=bullet[3];
            break;
        }
        switch(type){
            case 1:
            case 2:
            case 3:
            this.gameObject.tag="JBullet";
            break;
            case 4:
            this.gameObject.tag="Bullet";
            break;
        }
        if(isAdditive){
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Unlit");
        }else{
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Unlit/Texture");
        }
        game=GameObject.Find("Game");
    }

    // Update is called once per frame
    void Update()
    {
        switch(type){
            case 1:
            case 2:
            case 4:
            Delete(1);
            break;
        }
        if(!isCircle){
            transform.rotation=Quaternion.Euler(0,0,rad/Mathf.Deg2Rad);
        }
        switch(type){
            case 1:
            if(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x>-60){
                if(rad>Mathf.Atan2(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad-=Mathf.Deg2Rad*2;
                }
                if(rad<Mathf.Atan2(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad+=Mathf.Deg2Rad*2;
                }
            }
            break;
            case 3:
            if(cnt>60){
                r=Mathf.Pow(2f,(float)cnt/15);
                if(r>2000)Destroy(this.gameObject);
            }
            break;
        }
        rg.velocity=new Vector2(v*Mathf.Cos(rad),v*Mathf.Sin(rad));
        if(isCircle)this.gameObject.GetComponent<CircleCollider2D>().radius=r;
        switch(type){
            case 3:
            this.transform.localScale=new Vector3(r/16,r/16,1);
            break;
        }
        cnt++;
    }
    private void Delete(float size){
        if(this.gameObject.transform.position.x<-55*size || this.gameObject.transform.position.x>55*size || this.gameObject.transform.position.y<-25*size || this.gameObject.transform.position.y>25*size){
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.tag=="Enemy" && type!=3 && this.gameObject.tag=="JBullet")Destroy(this.gameObject);
    }
}