using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonScript : MonoBehaviour
{
    public Manager m;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        Debug.Log("実験終了");
        m.expStatus = 0;
        m.endTime = Time.time;
        //Debug.Log(m.endTime);
        Debug.Log(m.endTime - m.startTime);
        Debug.Log(m.overCount);
    }
}
