using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Player : MonoBehaviour
{
    Entity_Sheet1 data;
    private int cnt;
    public int stage;
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        data=Resources.Load("data") as Entity_Sheet1;
        cnt=0;
        stage=1;
        game=GameObject.Find("Game");
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<data.sheets[stage-1].list.Count;i++){
            if(data.sheets[stage-1].list[i].time==cnt){
                game.GetComponent<Game>().GetEnemyManager().GetComponent<EnemyManager>().EnemyAppear(data.sheets[stage-1].list[i].type,new Vector3(data.sheets[stage-1].list[i].x,data.sheets[stage-1].list[i].y,0),data.sheets[stage-1].list[i].vx,data.sheets[stage-1].list[i].vy,data.sheets[stage-1].list[i].note);
            }
        }
        cnt++;
    }
}
