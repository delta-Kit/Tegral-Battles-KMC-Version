using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject bullet;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        bullet=GameObject.Find("BulletManager");
        enemy=GameObject.Find("EnemyManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetBulletManager(){
        return bullet;
    }
    public GameObject GetEnemyManager(){
        return enemy;
    }
}
