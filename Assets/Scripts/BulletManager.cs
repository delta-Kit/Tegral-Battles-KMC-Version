using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{   public GameObject Bullet;
    public List<GameObject> bullet;
    public int j;
    public GameObject jiki;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BulletAppear(int type,float v,float rad,float r,bool isCircle,Vector3 pos){
        if(jiki.GetComponent<Jiki>().bombCnt>=180 || type<=3){
            GameObject b=Instantiate(Bullet,pos,Quaternion.identity);
            bullet.Add(b);
            b.GetComponent<Bullet>().type=type;
            b.GetComponent<Bullet>().v=v;
            b.GetComponent<Bullet>().rad=rad;
            b.GetComponent<Bullet>().r=r;
            b.GetComponent<Bullet>().isCircle=isCircle;
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