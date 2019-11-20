using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//閾値決めて、限界突破タイムに突入するコードを書かないとね


public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject matrixObject; //text
    public GameObject sphereObjectShoulder; //sphere肩
    public GameObject sphereObjectWaist; //sphere腰
    public GameObject LimitText;//ずれていることを知らせる
    // public Camera cam;

    int count;
    string usrname = "nonaka-PreTest-Day2-01";
    public int expStatus = 0;//0:実験前後 1:実験中
    public int LimitStatus = 0;//0:限界突破前 1:限界突破中

    public int modeStatus;//0:練習 1:本番

    //限界突破への道
    public int overCount;
    public bool isSeqFlag;
    public float rangeVec= 0f;

    public Transform startShoulderPos;
    public Transform startWaistPos;
    
    public float startTime;
    public float endTime;

    public Vector2 startVec;
    public Vector2 expeVec;

    void Start()
    {
        count = 0;
        modeStatus = 1;//0：練習 1：本番
        overCount = 0;
        isSeqFlag = false;

        string init = "count,dateTime,waistX,waistY,waistZ,expStatus,LimitStatus";
        //"count,dateTime,degree,shoulderX,shoulderY,shoulderZ,waistX,waistY,waistZ,expStatus,LimitStatus";
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo("./Assets/Resources/Pretest/" + usrname +"res.csv");
        sw = fi.AppendText();
        sw.WriteLine(init);
        sw.Flush();
        sw.Close();

        // cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        Text matrixText = matrixObject.GetComponent<Text> ();
        Transform trsShoulder = sphereObjectShoulder.GetComponent<Transform> ();
        Transform trsWaist = sphereObjectWaist.GetComponent<Transform> ();

        Vector2 shoulderPos = new Vector2(trsShoulder.position.x,trsShoulder.position.y);
        Vector2 waistPos = new Vector2(trsWaist.position.x,trsWaist.position.y);

        Vector2 dif = shoulderPos - waistPos;
        float radian = Mathf.Atan2(dif.y, dif.x);
        float degree = radian * Mathf.Rad2Deg;

        matrixText.text = degree.ToString() + "°\n肩X: " + trsShoulder.position.x.ToString() + "\n肩Y:"+ trsShoulder.position.y.ToString() + "\n腰X:" + trsWaist.position.x.ToString() + "\n腰Y:" + trsWaist.position.y.ToString();
        //matrixText.text = degree.ToString() + "°\n肩X: " + shoulderPosWorldPos.x.ToString() + "\n肩Y:"+ shoulderPosWorldPos.y.ToString() + "\n腰X:" + waistPosWorldPos.x.ToString() + "\n腰Y:" + waistPosWorldPos.y.ToString();
        StreamWriter sw;
        FileInfo fi;
        //実験中であるか
        if (expStatus == 1){
            //judgeLimit();
            //Debug.Log(overCount);
            //Debug.Log("trsWaist.position.y:" + trsWaist.position.y);
            //double sep = Mathf.Abs(trsWaist.position.y) - Mathf.Abs(startWaistPos.position.y);
            //Debug.Log(waistPos.y);
            rangeVec = Mathf.Abs(Mathf.Abs(startVec.y)-Mathf.Abs(waistPos.y));//Vector2.Distance(startVec, waistPos);
            Debug.Log("差:" + rangeVec);
            // Debug.Log("LimitStatus:" + LimitStatus);
            // Debug.Log("OverCount:" + overCount);     
            //閾値超えてたらカウント開始
            if (rangeVec > 30f){//50fだと余裕ありすぎ25fはわからん30fあたりがいい感じかもしれぬ
                isSeqFlag = true;
                overCount++;
            } else {
                isSeqFlag = false;
            }

            if (isSeqFlag == false){
                overCount = 0;
                //ずれている表示を消す
                LimitText.SetActive(false);
            }
            if (isSeqFlag == true && LimitStatus == 0){
                //ずれていることを表示
                Text limText = LimitText.GetComponent<Text> ();
                limText.text = "腰がずれています！";
                LimitText.SetActive(true);
            }

            //３秒超えたら限界突破開始
            if (overCount > 90){
                LimitStatus = 1;
                LimitText.SetActive(false);
            }

            //csvファイル書き出し
            string datetime = DateTime.Now.ToString("yyyy/MM/dd ") + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString(); 
            string shoulderText = trsShoulder.position.x.ToString() + "," + trsShoulder.position.y.ToString() + "," + trsShoulder.position.z.ToString();
            string waistText = trsWaist.position.x.ToString() + "," + trsWaist.position.y.ToString() + "," + trsWaist.position.z.ToString();
            string txt = count + "," + datetime + "," /*+ degree.ToString() + "," + shoulderText + "," */+ waistText + "," + expStatus + "," + LimitStatus;
            fi = new FileInfo("./Assets/Resources/Pretest/" + usrname +"res.csv");
            sw = fi.AppendText();
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();
            count ++;
        }
    }
}
