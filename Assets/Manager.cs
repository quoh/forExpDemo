using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject matrixObject; //text
    public GameObject sphereObjectShoulder; //sphere肩
    public GameObject sphereObjectWaist; //sphere腰
    

    int count;
    string usrname = "usr-trialCount";
    public int expStatus = 0;//0:実験前後 1:実験中

    void Start()
    {
        count = 0;
        string init = "count,dateTime,degree,shoulderX,shoulderY,shoulderZ,waistX,waistY,waistZ";
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo("./Assets/Resources/" + usrname +"res.csv");
        sw = fi.AppendText();
        sw.WriteLine(init);
        sw.Flush();
        sw.Close();
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
        

        matrixText.text = degree.ToString() + "°\n肩X: " + trsShoulder.position.x.ToString() + "\n肩Y:"+ trsShoulder.position.y.ToString() + "\n腰X" + trsWaist.position.x.ToString() + "\n腰Y" + trsWaist.position.y.ToString();
        StreamWriter sw;
        FileInfo fi;
        if (expStatus == 1){
            string datetime = DateTime.Now.ToString("yyyy/MM/dd ") + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString(); 
            string shoulderText = trsShoulder.position.x.ToString() + "," + trsShoulder.position.y.ToString() + "," + trsShoulder.position.z.ToString();
            string waistText = trsWaist.position.x.ToString() + "," + trsWaist.position.y.ToString() + "," + trsWaist.position.z.ToString();
            string txt = count + "," + datetime + "," + degree.ToString() + "," + shoulderText + "," + waistText;
            fi = new FileInfo("./Assets/Resources/" + usrname +"res.csv");
            sw = fi.AppendText();
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();
            count ++;
        }
    }
}
