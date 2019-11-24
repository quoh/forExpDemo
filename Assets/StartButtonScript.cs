﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{

    // Start is called before the first frame update
    public Manager m;
    public GameObject waistSphere;
    public GameObject startPosTex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        Debug.Log("実験開始");
        m.expStatus = 1;
        m.startTime = (float)Time.time;
        m.overTimeList.Add(m.startTime);
        Debug.Log("startTime" + m.startTime);
        m.startShoulderPos = m.sphereObjectShoulder.GetComponent<Transform> ();
        m.startWaistPos = m.sphereObjectWaist.GetComponent<Transform> ();

        // Transform startWaistTrs = startPosTex.GetComponent<Transform> ();
        // startWaistTrs.localPosition = m.startWaistPos.position;

        m.startVec = new Vector2(m.startWaistPos.position.x, m.startWaistPos.position.y);
        //Debug.Log(m.startShoulderPos.position.ToString());
        Debug.Log(m.startVec.x);
        Debug.Log("startWaist.position.y:" +m.startWaistPos.position.y.ToString());
    }
}
