using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SetImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBack(int stage){
        switch(stage){
            case 1:
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("back1");
            break;
            case 2:
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("back2");
            break;
            case 3:
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("back3");
            break;
        }
    }
}
