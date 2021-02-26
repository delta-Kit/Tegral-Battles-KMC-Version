using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //　ポーズした時に表示するUI
    [SerializeField]
	private GameObject pauseUI;
    Entity_Sheet1 data;
    private int cnt;
    public EnemyManager enemyManager;
    protected static int stage;
    public AudioClip[] bgm = new AudioClip[8];
    public GameObject back;
    public AudioSource source;
    bool pause, up;
    public Image pointer;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //FPSを60に設定
        data = Resources.Load("data") as Entity_Sheet1;
        stage = 3;
        cnt = 0;
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        source = gameObject.GetComponent<AudioSource>();
        source.clip = bgm[stage];
        source.Play();
        back.GetComponent<SetImage>().SetBack(stage);
        pause = false;
        up = true;
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<data.sheets[stage-1].list.Count;i++){
            if(data.sheets[stage-1].list[i].time==cnt){
               enemyManager.EnemyAppear(data.sheets[stage-1].list[i].type,new Vector3(data.sheets[stage-1].list[i].x,data.sheets[stage-1].list[i].y,0),data.sheets[stage-1].list[i].vx,data.sheets[stage-1].list[i].vy,data.sheets[stage-1].list[i].note);
            }
        }
        if (!pause && Input.GetKeyDown(KeyCode.Escape)) {
			pauseUI.SetActive (true);
            Time.timeScale = 0f;
            source.Pause();
            pause = true;
		}
        if (pause) {
            if(up){
                if(Input.GetKeyDown(KeyCode.DownArrow)){
                    pointer.gameObject.transform.position -= new Vector3(0, 140, 0);
                    up = false;
                }
                if(Input.GetKeyDown(KeyCode.Return)){
                    pauseUI.SetActive (false);
                    Time.timeScale = 1f;
                    source.UnPause();
                    pause = false;
                }
            }else{
                if(Input.GetKeyDown(KeyCode.UpArrow)){
                    pointer.gameObject.transform.position += new Vector3(0, 140, 0);
                    up = true;
                }
                if(Input.GetKeyDown(KeyCode.Return)){
                    Time.timeScale = 1f;
                    pause = false;
                    Invoke("ChangeScene", 0);
                }
            }
	    	return;
	    }else if(pause){
            source.UnPause();
        }
        cnt++;
    }
    public static int GetStage(){
        return stage;
    }
    public static void ChangeStage(int stageP){
        stage = stageP;
    }
    void ChangeScene(){
        SceneManager.LoadScene("Title");
    }
}
