using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public class RobotAgent : Agent
{
    public GameObject area;
    BehaviorParameters behaviorParameters;
    public Team teamId;

    // To get ball's location for observations
    public GameObject ball;
    Rigidbody ballRb;
    public int agentRot;
    public GameObject racket;

    VolleyballSettings volleyballSettings;
    VolleyballEnvController envController;

    public GameObject pendulumA;
    public GameObject pendulumB;
    public GameObject pendulumC;
    public GameObject pendulumD;
   // public GameObject pendulumE;
   // public GameObject pendulumF;

    Rigidbody m_RbA;
    Rigidbody m_RbB;
    Rigidbody m_RbC;
    Rigidbody m_RbD;
   // Rigidbody m_RbE;
  // Rigidbody m_RbF;

    //public GameObject bat;
    //Rigidbody batRb;

    public Collider[] hitGroundColliders = new Collider[3];
    EnvironmentParameters resetParams;
    void Start()
    {
        envController = area.GetComponent<VolleyballEnvController>();
    }

    public override void Initialize()
    {
        volleyballSettings = FindObjectOfType<VolleyballSettings>();
        behaviorParameters = gameObject.GetComponent<BehaviorParameters>();

        m_RbA = pendulumA.GetComponent<Rigidbody>();
        m_RbB = pendulumB.GetComponent<Rigidbody>();
        m_RbC = pendulumC.GetComponent<Rigidbody>();
        m_RbD = pendulumD.GetComponent<Rigidbody>();
       // m_RbE = pendulumE.GetComponent<Rigidbody>();
      //  m_RbF = pendulumF.GetComponent<Rigidbody>();

        ballRb = ball.GetComponent<Rigidbody>();
      //  batRb = bat.GetComponent<Rigidbody>();

        if (teamId == Team.Blue)
        {
            agentRot = -1;
        }
        else
        {
            agentRot = 1;
        }

        resetParams = Academy.Instance.EnvironmentParameters;
    }

    void OnCollisionEnter(Collision c)
    {

        if (c.gameObject.CompareTag("ball"))
        {
            // Debug.Log("OnCoiision with ball is called");
            envController.UpdateLastHitter(teamId);
        }
    }


    public void OnChildCollisionEnter(Collision c)
    {

        if (c.gameObject.CompareTag("ball"))
        {
            Debug.Log("Team ID: " + teamId + ":  [Robot Agent] OnChildCoiision with ball is called\n ===============================");
            envController.UpdateLastHitter(teamId);
        }
    }


    public override void OnEpisodeBegin()
    {
        pendulumA.transform.position = new Vector3(0f, 0.55f, 0f) + transform.position;
        pendulumA.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbA.velocity = Vector3.zero;
        m_RbA.angularVelocity = Vector3.zero;

        pendulumB.transform.position = new Vector3(0f, 0.55f, 0f) + transform.position;
        pendulumB.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbB.velocity = Vector3.zero;
        m_RbB.angularVelocity = Vector3.zero;

        pendulumC.transform.position = new Vector3(-0.15f, 0.55f, 0f) + transform.position;
        pendulumC.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbC.velocity = Vector3.zero;
        m_RbC.angularVelocity = Vector3.zero;

        pendulumD.transform.position = new Vector3(-0.15f, 1.375f, 0f) + transform.position;
        pendulumD.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbD.velocity = Vector3.zero;
        m_RbD.angularVelocity = Vector3.zero;


       // pendulumE.transform.position = new Vector3(-0.15f, 2f, 0f) + transform.position;
       // pendulumE.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
       // m_RbE.velocity = Vector3.zero;
      //  m_RbE.angularVelocity = Vector3.zero;

      //  pendulumF.transform.position = new Vector3(0.15f, 2.11f, 0f) + transform.position;
      //  pendulumF.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
     //   m_RbF.velocity = Vector3.zero;
     //   m_RbF.angularVelocity = Vector3.zero;

       // bat.transform.position = new Vector3(-0.0009999871f, 0f, 0.501f) + transform.position;
       // batRb.velocity = Vector3.zero;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(pendulumA.transform.localPosition);
        sensor.AddObservation(pendulumA.transform.rotation);
        sensor.AddObservation(m_RbA.angularVelocity);
        sensor.AddObservation(m_RbA.velocity);

        sensor.AddObservation(pendulumB.transform.localPosition);
        sensor.AddObservation(pendulumB.transform.rotation);
        sensor.AddObservation(m_RbB.angularVelocity);
        sensor.AddObservation(m_RbB.velocity);

        sensor.AddObservation(pendulumC.transform.localPosition);
        sensor.AddObservation(pendulumC.transform.rotation);
        sensor.AddObservation(m_RbC.angularVelocity);
        sensor.AddObservation(m_RbC.velocity);

        sensor.AddObservation(pendulumD.transform.localPosition);
        sensor.AddObservation(pendulumD.transform.rotation);
        sensor.AddObservation(m_RbD.angularVelocity);
        sensor.AddObservation(m_RbD.velocity);

        //sensor.AddObservation(pendulumE.transform.localPosition);
        //sensor.AddObservation(pendulumE.transform.rotation);
        //sensor.AddObservation(m_RbE.angularVelocity);
        //sensor.AddObservation(m_RbE.velocity);

        //sensor.AddObservation(pendulumF.transform.localPosition);
        //sensor.AddObservation(pendulumF.transform.rotation);
        //sensor.AddObservation(m_RbF.angularVelocity);
        //sensor.AddObservation(m_RbF.velocity);

        sensor.AddObservation(ballRb.transform.position);
        sensor.AddObservation(ballRb.velocity);

        //sensor.AddObservation(batRb.transform.position);
        //sensor.AddObservation(batRb.velocity);

        // Vector from agent to ball (direction to ball) (3 floats)
        //Vector3 toBall = new Vector3((ballRb.transform.position.x - bat.transform.position.x) * agentRot,
        //(ballRb.transform.position.y - bat.transform.position.y),
        //(ballRb.transform.position.z - bat.transform.position.z) * agentRot);

        //sensor.AddObservation(toBall.normalized);
        //// Distance from the ball (1 float)
        //sensor.AddObservation(toBall.magnitude);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var torque = Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f) * 150f;
        m_RbA.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f) * 150f;
        m_RbB.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(actions.ContinuousActions[2], -1f, 1f) * 150f;
        m_RbC.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(actions.ContinuousActions[3], -1f, 1f) * 150f;
        m_RbD.AddTorque(new Vector3(0f, torque, 0f));

        //torque = Mathf.Clamp(actions.ContinuousActions[4], -1f, 1f) * 150f;
        //m_RbE.AddTorque(new Vector3(0f, 0f, torque));

        //torque = Mathf.Clamp(actions.ContinuousActions[5], -1f, 1f) * 150f;
        //m_RbF.AddTorque(new Vector3(0f, torque, 0f));
    }

}
