using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))Invoke("ChangeScene",0);
    }
    void ChangeScene(){
        Player.ChangeStage(1);
        SceneManager.LoadScene("Game");
    }
}
