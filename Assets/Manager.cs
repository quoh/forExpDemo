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
    string usrname = "nanri-Debug-frequency-01";
    public int expStatus = 0;//0:実験前後 1:実験中
    public int LimitStatus = 0;//0:限界突破前 1:限界突破中

    public int modeStatus;//0:練習 1:本番
    public int overStatus;//-1:下がっている 0:大丈夫 1:上がっている
    //public int overCounter;

    //限界突破への道
    public int overCount;
    public bool isSeqFlag;
    public float rangeVec= 0f;
    public float rangeVecSigned = 0;

    public Transform startShoulderPos;
    public Transform startWaistPos;
    
    public float startTime;
    public float endTime;

    public Vector2 startVec;
    public Vector2 expeVec;

    //頻度を使った限界判定
    public List<float> overTimeList;
    float frequency;

    void Start()
    {
        count = 0;
        modeStatus = 0;//0：練習 1：本番
        overStatus = 0;
        overCount = 0;
        isSeqFlag = false;

        overTimeList = new List<float>();
        frequency = 100;

        string init = "count,dateTime,waistX,waistY,waistZ,expStatus,LimitStatus,overStatus,freq";
        //"count,dateTime,degree,shoulderX,shoulderY,shoulderZ,waistX,waistY,waistZ,expStatus,LimitStatus";
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo("./Assets/Resources/Pretest/" + usrname +".csv");
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
            rangeVecSigned = startVec.y-waistPos.y;
            //Debug.Log("差:" + rangeVec);
            // Debug.Log("LimitStatus:" + LimitStatus);
            // Debug.Log("OverCount:" + overCount);
            //閾値超えてたらカウント開始
            if (rangeVec > 25f){
                //限界開始時間 Time.timeかな
                if (isSeqFlag == false){
                    overTimeList.Add((float)Time.time);
                }

                isSeqFlag = true;
                overCount++;
                //overCounter++;
            } else {
                isSeqFlag = false;
            }

            if (isSeqFlag == false){
                overCount = 0;
                //ずれている表示を消す
                LimitText.SetActive(false);
                overStatus = 0;
            }
            if (isSeqFlag == true && LimitStatus == 0){
                //ずれていることを表示
                Text limText = LimitText.GetComponent<Text> ();
                if (rangeVec > 0){
                    limText.text = "腰が下がっています！";
                    overStatus = -1;
                } else if (rangeVec < 0){
                    limText.text = "腰が上がっています！";
                    overStatus = 1;
                }
                LimitText.SetActive(true);
            }
            if (overTimeList.Count > 5){
                // Debug.Log("overTimeListMAX: " + overTimeList[overTimeList.Count - 1]);
                // Debug.Log("overTimeListMAX-3: " + overTimeList[overTimeList.Count - 4]);
                // Debug.Log((float)(overTimeList[overTimeList.Count - 1] - overTimeList[overTimeList.Count - 4]));
                // Debug.Log((float)(overTimeList[overTimeList.Count - 1] - overTimeList[overTimeList.Count - 4])/3);

                frequency = (float)(overTimeList[overTimeList.Count - 1] - overTimeList[overTimeList.Count - 6])/5;
                Debug.Log("frequency: " + frequency.ToString());
            }

            //2.5秒超えたら限界突破開始
            if (overCount > 75 || frequency <  1.0){//
                LimitStatus = 1;
                LimitText.SetActive(false);
            }

            //csvファイル書き出し
            string datetime = DateTime.Now.ToString("yyyy/MM/dd ") + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString(); 
            string shoulderText = trsShoulder.position.x.ToString() + "," + trsShoulder.position.y.ToString() + "," + trsShoulder.position.z.ToString();
            string waistText = trsWaist.position.x.ToString() + "," + trsWaist.position.y.ToString() + "," + trsWaist.position.z.ToString();
            string txt = count + "," + datetime + "," /*+ degree.ToString() + "," + shoulderText + "," */+ waistText + "," + expStatus + "," + LimitStatus + "," + overStatus + "," + frequency;
            fi = new FileInfo("./Assets/Resources/Pretest/" + usrname +".csv");
            sw = fi.AppendText();
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();
            count ++;
        }
    }
}
