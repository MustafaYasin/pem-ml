using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatHit : MonoBehaviour
{
    public RobotAgent robotAgent = null;

    private void OnCollisionEnter(Collision collision)
    {
        robotAgent.HandleCollisionEnter(collision);
    }
}
