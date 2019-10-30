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
    public Camera cam;

    int count;
    string usrname = "usr-trialCount";
    public int expStatus = 0;//0:実験前後 1:実験中
    public int LimitStatus = 0;//0:限界突破前 1:限界突破中
    public int countDown = 5;

    public int modeStatus = 0;//0:練習 1:本番

    //限界突破への道
    public int overCount;
    public bool isSeqFlag;
    public float rangeLimit = Mathf.Pow(2.5f,2);//限界とみなす幅:上下5㎝
    //public float rangeX = 0f;
    public float rangeY = 0f;

    public Transform startShoulderPos;
    public Transform startWaistPos;
    
    public float startTime;
    public float endTime;


    void Start()
    {
        count = 0;
        countDown = 5; 
        
        overCount = 0;
        isSeqFlag = false;

        string init = "count,dateTime,degree,shoulderX,shoulderY,shoulderZ,waistX,waistY,waistZ,expStatus,LimitStatus";
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo("./Assets/Resources/" + usrname +"res.csv");
        sw = fi.AppendText();
        sw.WriteLine(init);
        sw.Flush();
        sw.Close();

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        Text matrixText = matrixObject.GetComponent<Text> ();
        Transform trsShoulder = sphereObjectShoulder.GetComponent<Transform> ();
        Transform trsWaist = sphereObjectWaist.GetComponent<Transform> ();

        Vector2 shoulderPos = new Vector2(trsShoulder.position.x,trsShoulder.position.y);
        Vector2 waistPos = new Vector2(trsWaist.position.x,trsWaist.position.y);

        // Vector3 shoulderPosVec3 = new Vector3(trsShoulder.position.x,trsShoulder.position.y,trsShoulder.position.z);
        // Vector3 waistPosVec3 = new Vector3(trsWaist.position.x,trsWaist.position.y,trsWaist.position.z);
        
        // Vector3 shoulderPosWorldPos = cam.ScreenToWorldPoint(shoulderPosVec3);
        // Vector3 waistPosWorldPos = cam.ScreenToWorldPoint(waistPosVec3);


        Vector2 dif = shoulderPos - waistPos;
        float radian = Mathf.Atan2(dif.y, dif.x);
        float degree = radian * Mathf.Rad2Deg;
        
        // Vector3 dif = shoulderPosWorldPos - waistPosWorldPos;
        // float radian = Mathf.Atan2(dif.y, dif.x);
        // float degree = radian * Mathf.Rad2Deg;

        matrixText.text = degree.ToString() + "°\n肩X: " + trsShoulder.position.x.ToString() + "\n肩Y:"+ trsShoulder.position.y.ToString() + "\n腰X:" + trsWaist.position.x.ToString() + "\n腰Y:" + trsWaist.position.y.ToString();
        //matrixText.text = degree.ToString() + "°\n肩X: " + shoulderPosWorldPos.x.ToString() + "\n肩Y:"+ shoulderPosWorldPos.y.ToString() + "\n腰X:" + waistPosWorldPos.x.ToString() + "\n腰Y:" + waistPosWorldPos.y.ToString();
        StreamWriter sw;
        FileInfo fi;
        if (expStatus == 1){
            //judgeLimit();
            Debug.Log(overCount);
            Debug.Log(trsWaist.position.y - startWaistPos.position.y);
            rangeY = Mathf.Pow((float)(trsWaist.position.y - startWaistPos.position.y),2);
            if (rangeY > rangeLimit){
                isSeqFlag = true;
                overCount++;
            } else {
                isSeqFlag = false;
            }
            if (isSeqFlag == false){
                overCount = 0;
            }

            string datetime = DateTime.Now.ToString("yyyy/MM/dd ") + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString(); 
            string shoulderText = trsShoulder.position.x.ToString() + "," + trsShoulder.position.y.ToString() + "," + trsShoulder.position.z.ToString();
            string waistText = trsWaist.position.x.ToString() + "," + trsWaist.position.y.ToString() + "," + trsWaist.position.z.ToString();
            string txt = count + "," + datetime + "," + degree.ToString() + "," + shoulderText + "," + waistText + "," + expStatus + "," + LimitStatus;
            fi = new FileInfo("./Assets/Resources/" + usrname +"res.csv");
            sw = fi.AppendText();
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();
            count ++;
        }
    }
}
