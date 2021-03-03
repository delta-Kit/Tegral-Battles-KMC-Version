using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private bool htp, up;
    public Image pointer, TImage;
    public Sprite[] TSprite;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true, 60);
        htp = false;
        up = true;
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
            if(up && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)))Invoke("ChangeScene", 0);
            if(Input.GetKeyDown(KeyCode.DownArrow) && up){
                up = false;
                pointer.gameObject.transform.position -= new Vector3(0, 100, 0);
            }
            if(Input.GetKeyDown(KeyCode.UpArrow) && !up){
                up = true;
                pointer.gameObject.transform.position += new Vector3(0, 100, 0);
            }
            if(!up && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))){
                htp = true;
                pointer.gameObject.SetActive(false);
                TImage.sprite = TSprite[1];
            }
        }
    }
    void ChangeScene(){
        Player.ChangeStage(1);
        SceneManager.LoadScene("Game");
    }
}
