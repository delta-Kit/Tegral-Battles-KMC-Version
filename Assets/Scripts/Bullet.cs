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
    public Sprite[] bullet = new Sprite[7], laser = new Sprite[7];
    SpriteRenderer  bulletSpriteRenderer;
    private bool isAdditive;
    Quaternion rot;
    public EnemyManager enemyManager;
    public int cnt;
    public int color;
    public BulletManager bulletManager;
    public int note;
    CircleCollider2D circle;
    BoxCollider2D box;
    CapsuleCollider2D cupsule;
    // Start is called before the first frame update
    void Start()
    {
        rg = this.gameObject.GetComponent<Rigidbody2D>();
        bulletSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isAdditive = true;
        transform.rotation = Quaternion.Euler(0,0,rad / Mathf.Deg2Rad);
        circle = this.gameObject.GetComponent<CircleCollider2D>();
        box = this.gameObject.GetComponent<BoxCollider2D>();
        cupsule = this.gameObject.GetComponent<CapsuleCollider2D>();
        if(type > 200){
            circle.enabled = false;
            box.enabled = false;
            cupsule.enabled = true;
            cupsule.size = new Vector2(24, 1);
        }
        else if(isCircle){
            circle.enabled=true;
            box.enabled=false;
            cupsule.enabled = false;
            switch(type){
                case 3:
                circle.radius=16;
                break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                circle.radius=2;
                break;
            }
        }else{
            circle.enabled=false;
            box.enabled=true;
            cupsule.enabled = false;
            switch(type){
                case 1:
                box.size=new Vector2(4,2);
                break;
                case 2:
                box.size=new Vector2(12,16);
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
            case 9:
            case 10:
            case 11:
            case 201:
            this.gameObject.tag="Bullet";
            break;
        }
        if(isAdditive){
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Surface");
        }else{
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Standard");
        }
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        switch(type){
            case 3:
            this.transform.localScale=new Vector3(r * 0.0625f, r * 0.0625f, 1f);
            break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            this.transform.localScale=new Vector3(r/2,r/2,1f);
            break;
            case 201:
            this.transform.localScale = new Vector3(1 / cupsule.size.x, 0.3f, 1f);
            break;
        }
        bulletManager=GameObject.Find("BulletManager").GetComponent<BulletManager>();
        if(isAdditive){
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Particles/Standard Surface");
        }else{
            gameObject.GetComponent<Renderer>().material.shader=Shader.Find("Standard");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(type){
            case 1:
            case 2:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 10:
            case 11:
            Delete(1);
            break;
            case 9:
            Delete(3);
            break;
            case 201:
            Delete(1.3f);
            break;
        }
        if(!isCircle)transform.rotation=Quaternion.Euler(0,0,rad/Mathf.Deg2Rad);
        switch(type){
            case 1:
            if(enemyManager.GetEnemyPosition().x>-60){
                if(rad>Mathf.Atan2(enemyManager.GetEnemyPosition().y-this.gameObject.transform.position.y,enemyManager.GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad-=Mathf.Deg2Rad*3;
                }
                if(rad<Mathf.Atan2(enemyManager.GetEnemyPosition().y-this.gameObject.transform.position.y,enemyManager.GetEnemyPosition().x-this.gameObject.transform.position.x)){
                    rad+=Mathf.Deg2Rad*3;
                }
            }
            break;
            case 3:
            if(cnt > 60){
                r *= 1.05f;
                if(r > 80)this.gameObject.SetActive(false);
            }
            break;
            case 5:
            if(v>-40)v--;
            break;
            case 6:
            if(cnt < 60)rad += note * Mathf.Deg2Rad;
            if(cnt == 40){
                for(int i = -3; i <= 3; i ++){
                    bulletManager.BulletAppear(this.gameObject.transform.position, 9, 1, v, 2, 0, rad + Mathf.Deg2Rad * (180 + i * 8) + Random.Range(-0.1f, 0.1f), 0.5f);
                }
                this.gameObject.SetActive(false);
            }
            break;
            case 7:
            if(cnt>=note){
                v=8f;
                bulletSpriteRenderer.sprite=bullet[4];
            }
            break;
            case 8:
            if(cnt < 60)rad += Mathf.Deg2Rad * note * 1.5f;
            break;
            case 10:
            if(cnt < 60)rad += note * Mathf.Deg2Rad;
            if(cnt == 40){
                bulletManager.BulletAppear(this.gameObject.transform.position, 201, 1, 30, 2, 0, rad + Mathf.Deg2Rad * 180, 20);
                this.gameObject.SetActive(false);
            }
            break;
            case 11:
            if(cnt < note)v += 0.2f;
            break;
        }
        rg.velocity=new Vector2(v*Mathf.Cos(rad),v*Mathf.Sin(rad));
        switch(type){
            case 3:
            this.transform.localScale=new Vector3(r/16,r/16,1f);
            break;
            case 201:
            if(cnt < r)this.transform.localScale += new Vector3(v / 1000, 0, 0);
            break;
        }
        cnt++;
    }
    private void Delete(float size){
        if(this.gameObject.transform.position.x<-45*size || this.gameObject.transform.position.x>45*size || this.gameObject.transform.position.y<-21*size || this.gameObject.transform.position.y>21*size){
            this.gameObject.SetActive(false);
        }
    }
}