﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{   public GameObject Bullet;
    public List<GameObject> bullet;
    public Jiki jiki;
    private int cnt;
    private int num;
    public GameObject effect;
    public AudioClip[] spawn=new AudioClip[3];
    public GameObject enemyManager;
    public List<GameObject> bulletSpawner;
    public void BulletCreate(Vector3 pos, int type, float v, int color, float rad, float r, bool isCircle, int note){
        if(num < bullet.Count){
            GameObject b = bullet[num];
            Bullet b2 = b.GetComponent<Bullet>();
            b2.type = type;
            b2.v = v;
            b2.color = color;
            b2.rad = rad;
            b2.r = r;
            b2.isCircle = isCircle;
            b2.note = note;
            b.gameObject.transform.position = pos;
            b.SetActive(true);
            num++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
        num = 0;
        jiki=GameObject.Find("TegralK1").GetComponent<Jiki>();
        for(int i = 0; i < 10000; i++){
            GameObject b = Instantiate(Bullet, new Vector3(1000, 0, 0), Quaternion.identity);
            b.SetActive(false);
            bullet.Add(b);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cnt++;   
    }
    public void BulletAppear(Vector3 pos, int type, int interval, float v, int color, int note, float rad, float r){
        if(jiki.bombCnt>=180 || type==2){
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
        int bulletNum=bullet.Count;
        int bulletSpawnerNum=bulletSpawner.Count;
        for(int i = 0;i < bulletNum; i++){
            if(bullet[i].tag == "Bullet")bullet[i].SetActive(false);
        }
        for(int i = 0; i < bulletSpawnerNum; i++){
            GameObject box = bulletSpawner[0];
            bulletSpawner.RemoveAt(0);
            Destroy(box);
        }
    }
    public void BulletMove(int type){;
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
        }
    }
}