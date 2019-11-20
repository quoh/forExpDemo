using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{

    // Start is called before the first frame update
    public Manager m;
    public GameObject waistSphere;
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
        Debug.Log("startTime" + m.startTime);
        m.startShoulderPos = m.sphereObjectShoulder.GetComponent<Transform> ();
        m.startWaistPos = m.sphereObjectWaist.GetComponent<Transform> ();

        // Transform waistpos = waistSphere.GetComponent<Transform> ();
        // GameObject startObj = 
        // startObj.AddComponent<Sphere> ();

        m.startVec = new Vector2(m.startWaistPos.position.x, m.startWaistPos.position.y);
        //Debug.Log(m.startShoulderPos.position.ToString());
        Debug.Log(m.startVec.x);
        Debug.Log("startWaist.position.y:" +m.startWaistPos.position.y.ToString());
    }
}
