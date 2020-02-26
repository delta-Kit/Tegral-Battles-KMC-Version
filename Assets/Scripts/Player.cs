using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Player : MonoBehaviour
{
    Entity_Sheet1 data;
    private int cnt;
    public GameObject enemyManager;
    protected static int stage;
    public AudioClip[] bgm = new AudioClip[8];
    // Start is called before the first frame update
    void Start()
    {
        stage = 2;
        data=Resources.Load("data") as Entity_Sheet1;
        cnt=800;
        enemyManager = GameObject.Find("EnemyManager");
        gameObject.GetComponent<AudioSource>().clip = bgm[stage];
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<data.sheets[stage-1].list.Count;i++){
            if(data.sheets[stage-1].list[i].time==cnt){
               enemyManager.GetComponent<EnemyManager>().EnemyAppear(data.sheets[stage-1].list[i].type,new Vector3(data.sheets[stage-1].list[i].x,data.sheets[stage-1].list[i].y,0),data.sheets[stage-1].list[i].vx,data.sheets[stage-1].list[i].vy,data.sheets[stage-1].list[i].note);
            }
        }
        cnt++;
    }
    public static int GetStage(){
        return stage;
    }
    public static void ChangeStage(int stageP){
        stage = stageP;
    }
}
