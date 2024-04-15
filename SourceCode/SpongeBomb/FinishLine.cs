using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    void OnTriggerEnter(Collider coll) 
    {
        if(coll.GetComponent<Collider>().tag == "FinishLine") 
        {
            SceneManager.LoadScene("Clear");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
