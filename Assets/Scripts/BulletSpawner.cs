using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float x0,y0;
    public int type,interval;
    private int cnt;
    public GameObject game;
    public int note;
    public float boxX,boxY;
    private float vertialZ;
    // Start is called before the first frame update
    void Start()
    {
        cnt=0;
        game=GameObject.Find("Game");
        vertialZ=-6.3241062f;
    }

    // Update is called once per frame
    void Update()
    {
        switch(type){
            case 1:                   //Swastika curve
            switch(note){
                case 1:
                this.transform.position=new Vector3(x0+(float)cnt*Mathf.Cos(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,y0+(float)cnt*Mathf.Sin(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,0);
                break;
                case 2:
                this.transform.position=new Vector3(x0+(float)cnt*Mathf.Sin(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,y0-(float)cnt*Mathf.Cos(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,0);
                break;
                case 3:
                this.transform.position=new Vector3(x0-(float)cnt*Mathf.Sin(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,y0+(float)cnt*Mathf.Cos(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,0);
                break;
                case 4:
                this.transform.position=new Vector3(x0-(float)cnt*Mathf.Cos(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,y0-(float)cnt*Mathf.Sin(Mathf.Atan(-0.001f*cnt*cnt)/2)/5,0);
                break;
            }
            if(cnt%(interval*2)==0)game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,6,5,0,2,1,0);
            if(cnt%(interval*2)==interval)game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,6,5,0,2,2,0);
            if(cnt>300)Destroy(this.gameObject);
            break;
            case 2:
            boxX+=(boxY-boxX)/5;
            boxY+=(28f*boxX-boxY-boxX*vertialZ)/50;
            vertialZ+=(boxX*boxY-8f/3*vertialZ)/50;
            this.gameObject.transform.position=new Vector3(boxX/1.5f+10,boxY/1.5f ,0);
            if(cnt%interval==0){
                game.GetComponent<Game>().GetBulletManager().GetComponent<BulletManager>().BulletAppear(this.gameObject.transform.position,7,1,0,3,0,Mathf.Atan2((28f*boxX-boxY-boxX*vertialZ)/50,(boxY-boxX)/5));
            }
            break;
        }
        cnt++;
    }
}
