using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{

    // Start is called before the first frame update
    public Manager m;
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
        Debug.Log(m.startTime);
        m.startShoulderPos = m.sphereObjectShoulder.GetComponent<Transform> ();
        m.startWaistPos = m.sphereObjectWaist.GetComponent<Transform> ();
        Debug.Log(m.startWaistPos.position.y.ToString());
    }
}
