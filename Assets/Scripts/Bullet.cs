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
    public Sprite jBullet;
    public Sprite jBullet2;
    SpriteRenderer  bulletSpriteRenderer;
    private bool isAdditive;
    Quaternion rot;
    public GameObject game;
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
            bulletSpriteRenderer.sprite=jBullet;
            break;
            case 2:
            bulletSpriteRenderer.sprite=jBullet2;
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
            Delete(1);
            break;
        }
        if(!isCircle){
            transform.rotation=Quaternion.Euler(0,0,rad/Mathf.Deg2Rad);
        }
        switch(type){
            case 1:
            if(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x>0){
                if(rad>Mathf.Atan2(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad-=Mathf.Deg2Rad*2;
                }
                if(rad<Mathf.Atan2(game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().y-this.gameObject.transform.position.y,game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad+=Mathf.Deg2Rad*2;
                }
            }
            break;
        }
        rg.velocity=new Vector2(v*Mathf.Cos(rad),v*Mathf.Sin(rad));
    }
    private void Delete(float size){
        if(this.gameObject.transform.position.x<-55*size || this.gameObject.transform.position.x>55*size || this.gameObject.transform.position.y<-25*size || this.gameObject.transform.position.y>25*size){
            Destroy(this.gameObject);
        }
    }
}