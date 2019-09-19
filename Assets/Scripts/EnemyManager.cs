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
    public void EnemyAppear(int type,Vector3 pos,int vx,int vy){
        GameObject e=Instantiate(Enemy,pos,Quaternion.identity);
        enemy.Add(e);
        e.GetComponent<Enemy>().type=type;
        e.GetComponent<Enemy>().vx=vx;
        e.GetComponent<Enemy>().vy=vy;
    }
    public Vector2 GetEnemyPosition(){
        if(enemy.Count>0){
            return new Vector2(enemy.First().GetComponent<Enemy>().transform.position.x,enemy.First().GetComponent<Enemy>().transform.position.y);
        }else{
            return new Vector2(-1,-1);
        }
    }
}
