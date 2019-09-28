using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public GameObject Enemy;
    public List<GameObject> enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnemyAppear(int type,Vector3 pos,int vx,int vy,int note){
        GameObject e=Instantiate(Enemy,pos,Quaternion.identity);
        enemy.Add(e);
        e.GetComponent<Enemy>().type=type;
        e.GetComponent<Enemy>().vx=vx;
        e.GetComponent<Enemy>().vy=vy;
        e.GetComponent<Enemy>().note=note;
    }
    public Vector2 GetEnemyPosition(){
        for(int i=0;i<enemy.Count;i++){
            if(enemy[i].GetComponent<Enemy>().hp>0)return new Vector2(enemy[i].GetComponent<Enemy>().transform.position.x,enemy[i].GetComponent<Enemy>().transform.position.y);
        }
        return new Vector2(-100,-100);
    }
}
