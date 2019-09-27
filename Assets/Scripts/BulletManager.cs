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
    public void BulletAppear(Vector3 pos,int type,int interval,float v,int color){
        if(jiki.GetComponent<Jiki>().bombCnt>=180 || type<=3){
            switch(type){
                case 1:
                if(cnt%interval==0){
                    GameObject b=Instantiate(Bullet,pos+new Vector3(4,2,0),Quaternion.identity);
                    bullet.Add(b);
                    GameObject b2=Instantiate(Bullet,pos+new Vector3(4,-2,0),Quaternion.identity);
                    bullet.Add(b2);
                    b.GetComponent<Bullet>().type=1;
                    b.GetComponent<Bullet>().v=v;
                    b.GetComponent<Bullet>().color=color;
                    b.GetComponent<Bullet>().rad=Mathf.Deg2Rad*30;
                    b.GetComponent<Bullet>().isCircle=false;
                    b2.GetComponent<Bullet>().type=1;
                    b2.GetComponent<Bullet>().v=v;
                    b2.GetComponent<Bullet>().color=color;
                    b2.GetComponent<Bullet>().rad=Mathf.Deg2Rad*(-30);
                    b2.GetComponent<Bullet>().isCircle=false;
                }
                if(cnt%(interval*2)==0){
                    GameObject b=Instantiate(Bullet,pos+new Vector3(4,0,0),Quaternion.identity);
                    bullet.Add(b);
                    b.GetComponent<Bullet>().type=2;
                    b.GetComponent<Bullet>().v=v;
                    b.GetComponent<Bullet>().color=101;
                    b.GetComponent<Bullet>().rad=0;
                    b.GetComponent<Bullet>().isCircle=false;      
                }
                break;
                case 2:
                if(cnt%interval==0){
                    GameObject b=Instantiate(Bullet,pos,Quaternion.identity);
                    bullet.Add(b);
                    b.GetComponent<Bullet>().type=3;
                    b.GetComponent<Bullet>().v=0;
                    b.GetComponent<Bullet>().color=color;
                    b.GetComponent<Bullet>().rad=0;
                    b.GetComponent<Bullet>().r=16;
                    b.GetComponent<Bullet>().isCircle=true;
                }
                break;
                case 3:
                if(cnt%interval==0){
                    for(int i=-1;i<=1;i++){
                        GameObject b=Instantiate(Bullet,pos,Quaternion.identity);
                        bullet.Add(b);
                        b.GetComponent<Bullet>().type=4;
                        b.GetComponent<Bullet>().v=v;
                        b.GetComponent<Bullet>().color=color;
                        b.GetComponent<Bullet>().rad=Mathf.Atan2(jiki.transform.position.y-pos.y,jiki.transform.position.x-pos.x)+Mathf.Deg2Rad*30*i;
                        b.GetComponent<Bullet>().r=0.5f;
                        b.GetComponent<Bullet>().isCircle=true;
                    }
                }
                break;
                case 4:
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
            }else{
                j++;
            }
        }
    }
}