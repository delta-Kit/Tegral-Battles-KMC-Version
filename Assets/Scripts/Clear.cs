using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public int stageC;
    private int cnt;
    public int lifeC;
    public int bombC;
    private KeyValuePair<int, int> lifeBomb;
    // Start is called before the first frame update
    void Start()
    {
        stageC=Player.GetStage();
        cnt = 0;
        lifeBomb = Jiki.GetLifeBomb();
    }

    // Update is called once per frame
    void Update()
    {
        if(cnt > 60 && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)))Invoke("ChangeScene",0);
        cnt++;
    }
    void ChangeScene(){
        if(stageC < 3){
            Player.ChangeStage(stageC + 1);
            Jiki.SetLifeBomb(lifeBomb.Key, lifeBomb.Value);
            SceneManager.LoadScene("Game");
        }else{
            Player.ChangeStage(1);
            SceneManager.LoadScene("Title");
        }
    }
}
