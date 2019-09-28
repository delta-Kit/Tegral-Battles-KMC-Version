using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{   public GameObject Bullet;
    public List<GameObject> bullet;
    public int j;
    public GameObject jiki;
    private int cnt;
    public GameObject game;
    public GameObject effect;
    public AudioClip[] spawn=new AudioClip[2];
    public void BulletCreate(Vector3 pos,int type,float v,int color,float rad,float r,bool isCircle){
        GameObject b=Instantiate(Bullet,pos,Quaternion.identity);
        bullet.Add(b);
        b.GetComponent<Bullet>().type=type;
        b.GetComponent<Bullet>().v=v;
        b.GetComponent<Bullet>().color=color;
        b.GetComponent<Bullet>().rad=rad;
        b.GetComponent<Bullet>().r=r;
        b.GetComponent<Bullet>().isCircle=isCircle;
    }
    // Start is called before the first frame update
    void Start()
    {
        cnt=0;
        game=GameObject.Find("Game");
    }

    // Update is called once per frame
    void Update()
    {
        cnt++;   
    }
    public void BulletAppear(Vector3 pos,int type,int interval,float v,int color,int note){
        if(type==1){
            gameObject.GetComponent<AudioSource>().clip=spawn[0];
        }else{
            gameObject.GetComponent<AudioSource>().clip=spawn[1];
        }
        if(jiki.GetComponent<Jiki>().bombCnt>=180 || type==2){
            switch(type){
                case 1:
                if(cnt%interval==0){
                    BulletCreate(pos+new Vector3(4,2,0),1,v,color,Mathf.Deg2Rad*30,0,false);
                    BulletCreate(pos+new Vector3(4,-2,0),1,v,color,Mathf.Deg2Rad*(-30),0,false);
                    GetComponent<AudioSource>().PlayOneShot(spawn[0]);
                }
                if(cnt%(interval*2)==0)BulletCreate(pos+new Vector3(4,0,0),2,v,101,0,0,false);
                break;
                case 2:
                if(cnt%interval==0)BulletCreate(pos,3,0,color,0,16f,true);
                break;
                case 3:
                if(cnt%interval==0){
                    for(int i=-1;i<=1;i++)BulletCreate(pos,4,v,color,Mathf.Atan2(jiki.transform.position.y-pos.y,jiki.transform.position.x-pos.x)+Mathf.Deg2Rad*30*i,0.5f,true);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
                case 4:
                if(cnt%interval==note){
                    BulletCreate(pos,4,v,color,Mathf.Atan2(jiki.transform.position.y-pos.y,jiki.transform.position.x-pos.x),0.5f,true);
                    GetComponent<AudioSource>().PlayOneShot(spawn[1]);
                }
                break;
            }
        }
    }
    public void BulletDelete(){
        int bulletNum=bullet.Count;
        j=0;
        for(int i=0;i<bulletNum;i++){
            GameObject box=bullet[j];
            if(box.tag=="Bullet"){
                bullet.RemoveAt(j);
                Destroy(box);
                GameObject e=Instantiate(effect,box.transform.position,Quaternion.identity);
            }else{
                j++;
            }
        }
    }
}