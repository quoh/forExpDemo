﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//ここは表示非表示のスクリプト
//実験前：実験開始ボタンのみ表示
//実験中：実験開始ボタン以外表示
//リミット前：カウントダウンは非表示
//リミット中：カウントダウンは表示


public class viewScript : MonoBehaviour
{
    public Manager m;
    public GameObject LimitButton;
    public GameObject startButton;
    public GameObject endButton;
    public GameObject countDownText;
    public GameObject moveActionText;
    public GameObject matrixText;
    public GameObject startPosText;
    
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (m.modeStatus == 0){
            matrixText.SetActive(true);
        } else if (m.modeStatus == 1) {
            matrixText.SetActive(false); 
        }

        if (m.expStatus == 0)
        {
            LimitButton.SetActive(false);
            startButton.SetActive(true);
            endButton.SetActive(false);
            countDownText.SetActive(false);
            moveActionText.SetActive(false);
            //startPosText.SetActive(false);
        }
        else if (m.expStatus == 1)
        {
            LimitButton.SetActive(true);
            startButton.SetActive(false);
            endButton.SetActive(true);
            countDownText.SetActive(false);
            //startPosText.SetActive(true);
        }

        if (m.LimitStatus == 1){
            countDownText.SetActive(true);
        }
        else if (m.LimitStatus == 0){
            countDownText.SetActive(false);
        }
    }
}
