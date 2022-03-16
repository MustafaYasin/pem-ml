using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    Color batColor;
    public GameObject ball;
    public GameObject batHit;

    // Start is called before the first frame update
    void Start()
    {
        batColor = GetComponent<Renderer>().material.color;
        batHit.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == ball)
        {
            Debug.Log("hit ball");
            transform.parent.parent.parent.parent.gameObject.GetComponent<VolleyballAgent>().OnChildTriggerEntered(other);

        }
        //if (other.gameObject == ball)
        //{
        //    // GetComponent<Renderer>().material.color = Color.red;

        //    batHit.gameObject.SetActive(true);
        //    Debug.Log("hit ball");
        //}
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject == ball)
    //    {
    //        // GetComponent<Renderer>().material.color = Color.red;

    //        batHit.gameObject.SetActive(true);
    //        Debug.Log("hit ball");
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject == ball)
    //    {
    //        gameObject.GetComponent<Renderer>().material.color = batColor;
    //        batHit.gameObject.SetActive(false);
    //    }
    //}
}