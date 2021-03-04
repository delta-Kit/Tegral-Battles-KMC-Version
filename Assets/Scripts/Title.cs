using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private bool htp;
    public Image pointer, TImage;
    public Sprite[] TSprite;
    private int p;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true, 60);
        htp = false;
        p = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(htp){
            if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X)){
                htp = false;
                pointer.gameObject.SetActive(true);
                TImage.sprite = TSprite[0];
            }
        }else{
            if(p == 0 && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)))Invoke("ChangeScene", 0);
            if(Input.GetKeyDown(KeyCode.DownArrow) && p < 2){
                p++;
                pointer.gameObject.transform.position -= new Vector3(0, 100, 0);
            }
            if(Input.GetKeyDown(KeyCode.UpArrow) && p > 0){
                p--;
                pointer.gameObject.transform.position += new Vector3(0, 100, 0);
            }
            if(p == 1 && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))){
                htp = true;
                pointer.gameObject.SetActive(false);
                TImage.sprite = TSprite[1];
            }
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)){
                switch(p){
                    case 0:
                    Invoke("ChangeScene", 0);
                    break;
                    case 1:
                    htp = true;
                    pointer.gameObject.SetActive(false);
                    TImage.sprite = TSprite[1];
                    break;
                    case 2:
                    UnityEngine.Application.Quit();
                    break;
                }
            }
        }
    }
    void ChangeScene(){
        Player.ChangeStage(1);
        SceneManager.LoadScene("Game");
    }
}
