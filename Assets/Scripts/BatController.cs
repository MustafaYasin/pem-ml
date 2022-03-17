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


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ball)
        {
            Debug.Log("================================\n         Hit Ball");
            //transform.parent.parent.parent.parent.gameObject.GetComponent<VolleyballAgent>().OnChildCollisionEnter(collision);
            transform.parent.parent.parent.gameObject.GetComponent<RobotAgent>().OnChildCollisionEnter(collision);
            batHit.gameObject.SetActive(true);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == ball)
        {
            batHit.gameObject.SetActive(false);
        }
    }
}
