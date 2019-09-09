using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnemyAppear(int type,Vector3 pos){
        GameObject enemy=Instantiate(Enemy,pos,Quaternion.identity);
        enemy.GetComponent<Enemy>().type=type;
    }
}
