using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public int stageC;
    // Start is called before the first frame update
    void Start()
    {
        stageC=Player.GetStage();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))Invoke("ChangeScene",0);
    }
    void ChangeScene(){
        if(stageC<6){
            Player.ChangeStage(stageC + 1);
            SceneManager.LoadScene("Game");
        }else{
            Player.ChangeStage(1);
            SceneManager.LoadScene("Title");
        }
    }
}
