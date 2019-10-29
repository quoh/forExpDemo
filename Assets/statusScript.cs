using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class statusScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject statusObject;
    public Manager m;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text statusText = statusObject.GetComponent<Text> ();
        statusText.text = "experimentStatus: " + m.expStatus;
    }
}
