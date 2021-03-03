using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float x0, x1, y0, rad;
    public int type, interval;
    public int cnt;
    public BulletManager bulletManager;
    public int note;
    public float boxX, boxY;
    private float vertialZ;
    private int fact10;
    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        vertialZ=-15;
        fact10 = 1;
        for(int i = 1; i <= 10; i++)fact10 *= i;
        switch(type){
            case 7:
            x1 = UnityEngine.Random.Range(0, 20);
            x0 = x1 + Mathf.Sin(Mathf.Deg2Rad * 30.5f) * 0.5f / Mathf.Deg2Rad;
            y0 = 30.5f;
            break;
            case 8:
            x0 = 40;
            x1 = 0.01f * UnityEngine.Random.Range(0, 100);
            y0 = 1 / Mathf.Sin(Gamma((x0 + x1) * 0.125f));
            break;
            case 9:
            x0 = 40;
            x1 = 0.01f * UnityEngine.Random.Range(0, 100);
            break;
            case 10:
            y0 = 30.5f;
            break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
            case 6:     //ケイオティック・ファンクション1
            x0 = boxX + 30 - cnt;
            y0 = Mathf.Sin(x0 * x0 + Mathf.PI) + Mathf.Tan(x0 * Mathf.Pow(2, x0));
            r = Mathf.Sqrt((x0 - boxX) * (x0 - boxX) + y0 * y0);
            float rad1 = Mathf.Atan2(y0, x0);
            this.gameObject.transform.position = new Vector3(boxX, boxY, 0) + new Vector3(r * Mathf.Cos(rad + rad1), r * Mathf.Sin(rad + rad1), 0);
            float rad0 = UnityEngine.Random.Range(0, 2 * Mathf.PI);
            bulletManager.BulletAppear(this.gameObject.transform.position, 4, interval, 0, 5, 0, rad0, 0.5f);
            if(cnt > 180){
                bulletManager.BulletMove(3);
                bulletManager.bulletSpawner.RemoveAt(0);
                Destroy(this.gameObject);
            }
            break;
            case 7:
            if(cnt < 60){
                if(y0 != 0)bulletManager.BulletAppear(new Vector3(x0, y0, 0), 4, interval, 0, 6, 0, 0, 0.5f);
            }else{
                bulletManager.BulletMove(4);
                bulletManager.bulletSpawner.RemoveAt(0);
                Destroy(this.gameObject);
            }
            x0 = x1 - Mathf.Sin(Mathf.Deg2Rad * y0 * 9) * 15 / Mathf.Deg2Rad / y0 / 9;
            y0--;
            break;
            case 8:     //ケイオティック・ファンクション2
            if(cnt < 180){
                if(y0 < 30){
                    bulletManager.BulletAppear(new Vector3(x0, y0, 0), 4, interval, 0, 5, 0, UnityEngine.Random.Range(0, 2 * Mathf.PI), 0.5f);
                }
            }else{
                bulletManager.BulletMove(3, 60);
                bulletManager.bulletSpawner.RemoveAt(0);
                Destroy(this.gameObject);
            }
            x0--;
            y0 = 10 / Mathf.Sin(Gamma((x0 + x1) * 0.125f));
            break;
            case 9:     //ケイオティック・ファンクション3
            if(cnt < 180){
                bulletManager.BulletAppear(new Vector3(y0 + boxX, x0, 0), 4, interval, 0, 6, 0, UnityEngine.Random.Range(0, 2 * Mathf.PI), 0.5f);
            }else{
                bulletManager.BulletMove(3);
                bulletManager.bulletSpawner.RemoveAt(0);
                Destroy(this.gameObject);
            }
            x0--;
            float sum = 0;
            for(int i = 1; i <= 10; i++){
                p = Mathf.Pow((x0 + x1) * 0.4f, i);
                sum += Mathf.Cos(Mathf.PI * p) / p;
            }
            y0 = 1 / sum;
            break;
            case 10:
            if(cnt < 60){
                if(y0 != 0)bulletManager.BulletAppear(new Vector3(x0, y0, 0), 4, interval, 0, 6, 0, UnityEngine.Random.Range(0, 2 * Mathf.PI), 0.5f);
            }else{
                bulletManager.BulletMove(3);
                bulletManager.bulletSpawner.RemoveAt(0);
                Destroy(this.gameObject);
            }
            x0 = -(Mathf.Cos(Mathf.Deg2Rad * y0 * 9) - 1) * 15 / (Mathf.Deg2Rad * y0 * 9);
            y0--;
            break;
            case 11:
            if(cnt == 60){
                bulletManager.BulletAppear(this.gameObject.transform.position, 201, 1, 10, note, 0, UnityEngine.Random.Range(0, 2 * Mathf.PI), 40);
                bulletManager.bulletSpawner.RemoveAt(0);
                Destroy(this.gameObject);
            }
            break;
        }
        cnt++;
    }
    float Gamma(float x){
        float prod = 1;
        for(int i = 0; i < 10; i++)prod *= x + i;
        return fact10 * Mathf.Pow(10, x) / prod;
    }
}
