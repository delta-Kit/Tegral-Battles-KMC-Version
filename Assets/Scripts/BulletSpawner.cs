using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float x0,y0, rad;
    public int type,interval;
    public int cnt;
    public BulletManager bulletManager;
    public int note;
    public float boxX,boxY;
    private float vertialZ;
    // Start is called before the first frame update
    void Start()
    {
        cnt=0;
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        vertialZ=-15;
    }

    // Update is called once per frame
    void Update()
    {
        switch(type){
            case 1:                   //Swastika curve
            float r = 0.007f * cnt;
            float theta = Mathf.Atan(-2 * r * r) / 2;
            switch(note){            
                case 1:
                this.transform.position=new Vector3(x0+ r *Mathf.Cos(theta) * 30,y0+ r *Mathf.Sin(theta) * 30,0);
                break;
                case 2:
                this.transform.position=new Vector3(x0+ r *Mathf.Sin(theta) * 30,y0- r *Mathf.Cos(theta) * 30,0);
                break;
                case 3:
                this.transform.position=new Vector3(x0- r *Mathf.Sin(theta) * 30,y0+ r *Mathf.Cos(theta) * 30,0);
                break;
                case 4:
                this.transform.position=new Vector3(x0- r *Mathf.Cos(theta) * 30,y0- r *Mathf.Sin(theta) * 30,0);
                break;
            }
            if(cnt%(interval*2)==0)bulletManager.BulletAppear(this.gameObject.transform.position,6,5,0,2,1,0, 0.5f);
            if(cnt%(interval*2)==interval)bulletManager.BulletAppear(this.gameObject.transform.position,6,5,0,2,2,0, 0.5f);
            if(cnt>300)Destroy(this.gameObject);
            break;
            case 2:                    //ローレンツ・アトラクター
            boxX+=(boxY-boxX)/10;
            boxY+=(28f*boxX-boxY-boxX*vertialZ)/100;
            vertialZ+=(boxX*boxY-8f/3*vertialZ)/100;
            this.gameObject.transform.position=new Vector3(boxX/1.5f + 10,boxY/1.5f ,0);
            if(cnt%interval==0){
                bulletManager.BulletAppear(this.gameObject.transform.position,7,1,0,3, 240,Mathf.Atan2((28f*boxX-boxY-boxX*vertialZ)/50,(boxY-boxX)/5), 0);
            }
            break;
            case 3:
            switch(note){
                case 1:
                this.transform.position=new Vector3(x0, y0, 0) + new Vector3(((float)cnt / 9 * Mathf.Exp((float)-cnt / 9)),((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2((float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9), Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
                case 2:
                this.transform.position=new Vector3(x0, y0, 0) + new Vector3(-((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),(float)cnt / 9 * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2(Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9), -(float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
                case 3:
                this.transform.position=new Vector3(x0, y0, 0) + new Vector3(((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),-(float)cnt / 9 * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2(-Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9), (float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
                case 4:
                this.transform.position=new Vector3(x0, y0, 0) - new Vector3((float)cnt / 9 * Mathf.Exp((float)-cnt / 9),((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2(-(float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9), -Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
                case 5:
                this.transform.position=new Vector3(x0, y0, 0) + new Vector3((-(float)cnt / 9 * Mathf.Exp((float)-cnt / 9)),((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2((float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9), -Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
                case 6:
                this.transform.position=new Vector3(x0, y0, 0) + new Vector3(-((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),-(float)cnt / 9 * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2(-Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9), -(float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
                case 7:
                this.transform.position=new Vector3(x0, y0, 0) + new Vector3(((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),(float)cnt / 9 * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2(Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9), (float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
                case 8:
                this.transform.position=new Vector3(x0, y0, 0) - new Vector3(-(float)cnt / 9 * Mathf.Exp((float)-cnt / 9),((float)cnt / 9) * ((float)cnt / 9) * Mathf.Exp((float)-cnt / 9),0) * 30;
                bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 4, 72, Mathf.Atan2(-(float)cnt / 9 * Mathf.Exp((float)-cnt / 9) * (2 - (float)cnt / 9), Mathf.Exp((float)-cnt / 9) * (1 - (float)cnt / 9)) + cnt * Mathf.Deg2Rad * 6, 0.5f);
                break;
            }
            if(cnt == 72){
                Destroy(this.gameObject);
                bulletManager.BulletMove(2);
            }
            break;
            case 4:
            if(cnt == 0)this.gameObject.transform.position = new Vector3(x0, y0, 0);
            if(cnt < 60){
                rad += 1.5f * Mathf.Deg2Rad;
                this.transform.position += new Vector3(0.2f * Mathf.Cos(rad), 0.2f * Mathf.Sin(rad), 0);
            }else{
                rad = Mathf.Atan2(y0 - this.gameObject.transform.position.y, x0 - this.gameObject.transform.position.x) - 90 * Mathf.Deg2Rad;
                this.transform.position += new Vector3(0.4f * Mathf.Cos(rad), 0.4f * Mathf.Sin(rad), 0);
            }
            bulletManager.BulletAppear(this.gameObject.transform.position, 4, interval, 8, 4, 0, rad + 180 * Mathf.Deg2Rad, 0.5f);
            break;
            case 5:
            float p = 0;
            theta = Mathf.Deg2Rad * cnt * 6;
            switch(note){
                case 0:
                if(cnt % 720 < 240){
                    p = 0.3871f;
                }else if(cnt % 720 < 480){
                    p = 0.7233f;
                }else{
                    p = 1.5237f;
                }
                this.gameObject.transform.position = new Vector3(boxX, boxY, 0) + 7 * new Vector3(p * Mathf.Cos(1f / Mathf.Sqrt(p * p * p) * theta) - Mathf.Cos(theta), p * Mathf.Sin(1f / Mathf.Sqrt(p * p * p) * theta) - Mathf.Sin(theta), 0);
                break;
                case 1:
                if(cnt % 1000 < 240){
                    p = 5.2026f;
                    this.gameObject.transform.position = new Vector3(boxX, boxY, 0) + new Vector3(p * Mathf.Cos(1f / Mathf.Sqrt(p * p * p) * theta * 5) - Mathf.Cos(theta * 5), p * Mathf.Sin(1f / Mathf.Sqrt(p * p * p) * theta * 5) - Mathf.Sin(theta * 5), 0);
                }else if(cnt % 1000 < 480){
                    p = 9.5549f;
                    this.gameObject.transform.position = new Vector3(boxX, boxY, 0) + new Vector3(p * Mathf.Cos(1f / Mathf.Sqrt(p * p * p) * theta * 7) - Mathf.Cos(theta * 7), p * Mathf.Sin(1f / Mathf.Sqrt(p * p * p) * theta * 7) - Mathf.Sin(theta * 7), 0);
                }else{
                    p = 19.2184f;
                    this.gameObject.transform.position = new Vector3(boxX, boxY, 0) + new Vector3(p * Mathf.Cos(1f / Mathf.Sqrt(p * p * p) * theta * 10) - Mathf.Cos(theta * 10), p * Mathf.Sin(1f / Mathf.Sqrt(p * p * p) * theta * 10) - Mathf.Sin(theta * 10), 0);
                }
                break;
                case 2:
                if(cnt % 1200 < 700){
                    p = 30.1104f;
                    this.gameObject.transform.position = new Vector3(boxX, boxY, 0) + 0.6f * new Vector3(p * Mathf.Cos(1f / Mathf.Sqrt(p * p * p) * theta * 15) - Mathf.Cos(theta * 15), p * Mathf.Sin(1f / Mathf.Sqrt(p * p * p) * theta * 15) - Mathf.Sin(theta * 15), 0);
                }else{
                    p = 4;
                    this.gameObject.transform.position = new Vector3(boxX, boxY, 0) + 3 * new Vector3(p * Mathf.Cos(1f / Mathf.Sqrt(p * p * p) * theta) - Mathf.Cos(theta), p * Mathf.Sin(1f / Mathf.Sqrt(p * p * p) * theta) - Mathf.Sin(theta), 0);
                }
                break;
            }
            bulletManager.BulletAppear(this.gameObject.transform.position, 7, interval, 0, 2, 60, Mathf.Atan2(1f / Mathf.Sqrt(p) * Mathf.Cos(1f / Mathf.Sqrt(p * p * p) * theta) - Mathf.Cos(theta), -1f / Mathf.Sqrt(p) * Mathf.Sin(1f / Mathf.Sqrt(p * p * p) * theta) + Mathf.Sin(theta)), 0.5f);
            break;
        }
        cnt++;
    }
}
