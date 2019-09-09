using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{   public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BulletAppear(int type,float v,float rad,float r,bool isCircle,Vector3 pos){
        GameObject bullet=Instantiate(Bullet,pos,Quaternion.identity);
        bullet.GetComponent<Bullet>().type=type;
        bullet.GetComponent<Bullet>().v=v;
        bullet.GetComponent<Bullet>().rad=rad;
        bullet.GetComponent<Bullet>().r=r;
        bullet.GetComponent<Bullet>().isCircle=isCircle;
    }
}
