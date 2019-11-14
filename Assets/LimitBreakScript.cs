using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LimitBreakScript : MonoBehaviour
{
    public Manager m;

    float totalTime;
    int minute = 0;
    float seconds = 3f;
    float oldSeconds;
    public GameObject LimitBreakText;
    // Start is called before the first frame update
    void Start()
    {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Text timerText = LimitBreakText.GetComponent<Text> ();
        //限界突破していないとき投げる
        if (m.LimitStatus == 0){
            return;
        }

        //5秒（60fps）を超えたら限界とみなす
        if (m.overCount > 180){
            m.LimitStatus = 1;
        }

        if(totalTime < 0f){
            m.LimitStatus = 0;
            return;
        } 

        totalTime = minute * 60 + seconds;
        totalTime -= Time.deltaTime;

        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;
        if ((int)seconds != (int)oldSeconds){
            timerText.text = minute.ToString("00") + ":" + ((int)(seconds % 60)).ToString("00");
        }
        oldSeconds = seconds;

    }   

    public void OnClick()
    {
        m.LimitStatus = 1;
    }
}
