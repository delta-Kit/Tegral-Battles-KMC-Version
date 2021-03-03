using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{   public GameObject Bullet;
    public List<GameObject> bullet;
    public Jiki jiki;
    public GameObject jikiG;
    private int cnt;
    public GameObject effect;
    public AudioClip[] spawn=new AudioClip[3];
    public GameObject enemyManager;
    public List<GameObject> bulletSpawner;
    public void BulletCreate(Vector3 pos, int type, float v, int color, float rad, float r, bool isCircle, int note){
        GameObject b=Instantiate(Bullet,pos,Quaternion.identity);
        bullet.Add(b);
        b.GetComponent<Bullet>().type=type;
        b.GetComponent<Bullet>().v=v;
        b.GetComponent<Bullet>().color=color;
        b.GetComponent<Bullet>().rad=rad;
        b.GetComponent<Bullet>().r=r;
        b.GetComponent<Bullet>().isCircle=isCircle;
        b.GetComponent<Bullet>().note=note;
    }
    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
        jikiG = GameObject.Find("TegralK1");
        jiki = jikiG.GetComponent<Jiki>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cnt++;   
    }
    public void BulletAppear(Vector3 pos, int type, int interval, float v, int color, int note, float rad, float r){
        if(jiki.bombCnt >= 180 || type == 2){
            switch(type){
                case 1:
                if(cnt%interval==0){
                    BulletCreate(pos+new Vector3(4,2,0),1,v,color,Mathf.Deg2Rad*30,0,false,0);
                    BulletCreate(pos+new Vector3(4,-2,0),1,v,color,Mathf.Deg2Rad*(-30),0,false,0);
                    GetComponent<AudioSource>().PlayOneShot(spawn[0]);
                }
                if(cnt%(interval*2)==0)BulletCreate(pos+new Vector3(4,0,0),2,v,101,0,0,false,0);
                break;
                case 2:
                if(cnt%interval==0)BulletCreate(pos,3,0,color,0,16f,true,0);
                break;
                case 3:
                if(cnt%interval==0){
                    for(int i=-1;i<=1;i++)BulletCreate(pos,4,v,color,Mathf.Atan2(jiki.transform.position.y-pos.y,jiki.transform.position.x-pos.x)+Mathf.Deg2Rad*30*i,0.5f,true,0);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 4:
                if(cnt%interval==note){
                    BulletCreate(pos,4,v,color,rad,0.5f,true,0);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 5:
                if(cnt%interval==0){
                    for(int i=0;i<360;i+=30){
                        BulletCreate(pos,5,v,color,Mathf.Atan2(jiki.transform.position.y-pos.y,jiki.transform.position.x-pos.x)+Mathf.Deg2Rad*i,0.5f,true,0);
                    }
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 6:
                if(cnt%interval==0){
                    BulletCreate(pos, 9, v,color, rad, r,true,note);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 7:
                if(cnt%interval==0){
                    BulletCreate(pos,7,0,color,rad,0.5f,true, note);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 8:
                if(cnt % interval == 0){
                    BulletCreate(pos, 6, v, color, rad, 0.5f, true, note);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 9:
                if(cnt % interval == 0){
                    BulletCreate(pos, 4, v, color, rad, r, true, note);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 10:
                if(cnt % interval == 0){
                    BulletCreate(pos, 8, v, color, rad, 0.5f, true, note);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 11:
                BulletCreate(pos, 8, v, color, rad, 0.5f, true, note);
                GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                break;
                case 12:
                BulletCreate(pos, 10, v, color, rad, 0.5f, true, note);
                GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                break;
                case 201:
                if(cnt % interval == 0){
                    BulletCreate(pos, 201, v, color, rad, r, false, 0);
                    GetComponent<AudioSource>().PlayOneShot(spawn[2]);
                }
                break;
            }
        }
    }
    public void BulletDelete(){
        int bulletNum = bullet.Count;
        int bulletSpawnerNum = bulletSpawner.Count;
        int j = 0;
        for(int i = 0; i < bulletNum; i++){
            GameObject box = bullet[j];
            if(box.tag == "Bullet" && box.activeSelf){
                bullet.RemoveAt(j);
                Destroy(box);
                GameObject e = Instantiate(effect, box.transform.position, Quaternion.identity);
                e.GetComponent<Effect>().type = 1;
            }else{
                j++;
            }
        }
        for(int i = 0; i < bulletSpawnerNum; i++){
            GameObject box = bulletSpawner[0];
            bulletSpawner.RemoveAt(0);
            Destroy(box);
        }
    }
    public void BulletMove(int type, int time = 120){
        switch(type){
            case 1:
            for(int i=0;i<bullet.Count;i++){
                if(bullet[i].GetComponent<Bullet>().note==1)bullet[i].transform.RotateAround(enemyManager.GetComponent<EnemyManager>().GetEnemyPosition(),new Vector3(0,0,1),20*Mathf.Deg2Rad);
                if(bullet[i].GetComponent<Bullet>().note==2)bullet[i].transform.RotateAround(enemyManager.GetComponent<EnemyManager>().GetEnemyPosition(),new Vector3(0,0,1),-20*Mathf.Deg2Rad);
            }
            break;
            case 2:
            for(int i = 0; i < bullet.Count; i++){
                if(bullet[i].GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))bullet[i].GetComponent<Bullet>().v = 20f;
            }
            break;
            case 3:
            for(int i = 0; i < bullet.Count; i++){
                if(bullet[i].GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0)){
                    Bullet b = bullet[i].GetComponent<Bullet>();
                    b.type = 11;
                    b.cnt = 0;
                    b.note = time;
                }
            }
            break;
            case 4:
            for(int i = 0; i < bullet.Count; i++){
                float dx = jikiG.transform.position.x - bullet[i].transform.position.x, dy = jikiG.transform.position.y - bullet[i].transform.position.y;
                if(bullet[i].GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0) && dx * dx + dy * dy < (cnt - 300) * (cnt - 300)){
                    Bullet b = bullet[i].GetComponent<Bullet>();
                    b.type = 11;
                    b.cnt = 0;
                    b.rad = Mathf.Atan2(dy, dx);
                    b.note = time;
                }
            }
            break;
        }
    }
}