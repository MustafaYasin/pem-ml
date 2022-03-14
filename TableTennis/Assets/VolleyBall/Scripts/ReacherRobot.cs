using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public class ReacherRobot : Agent
{
    public GameObject area;


    // 
    public GameObject pendulumA;
    public GameObject pendulumB;
    public GameObject pendulumC;
    public GameObject pendulumD;
    public GameObject pendulumE;
    public GameObject pendulumF;

    Rigidbody m_RbA;
    Rigidbody m_RbB;
    Rigidbody m_RbC;
    Rigidbody m_RbD;
    Rigidbody m_RbE;
    Rigidbody m_RbF;

    public GameObject hand;
    public GameObject ball;

    public float m_GoalHeight = 1.2f;

    float m_GoalRadius; //Radious of the goal zone
    float m_GoalDegree; //How much the goal rotates 
    float m_GoalSpeed;  //Speed of the goal rotation
    float m_GoalDeviation;  //How much goes up and down  
    float m_GoalDeviationFreq;  //Frequency of the goal up and down

    public bool blHeuristic = false;

    public override void Initialize()
    {
        m_RbA = pendulumA.GetComponent<Rigidbody>();
        m_RbB = pendulumB.GetComponent<Rigidbody>();
        m_RbC = pendulumC.GetComponent<Rigidbody>();
        m_RbD = pendulumD.GetComponent<Rigidbody>();
        m_RbE = pendulumE.GetComponent<Rigidbody>();
        m_RbF = pendulumF.GetComponent<Rigidbody>();

        SetResetParameters();
    }

    public override void OnEpisodeBegin()
    {
        pendulumA.transform.position = new Vector3(0f, 0.55f, 0f) + transform.position;
        pendulumA.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbA.velocity = Vector3.zero;
        m_RbA.angularVelocity = Vector3.zero;

        pendulumB.transform.position = new Vector3(-0.15f, 0.55f, 0f) + transform.position;
        pendulumB.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbB.velocity = Vector3.zero;
        m_RbB.angularVelocity = Vector3.zero;

        pendulumC.transform.position = new Vector3(-0.15f, 1.375f, 0f) + transform.position;
        pendulumC.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbC.velocity = Vector3.zero;
        m_RbC.angularVelocity = Vector3.zero;

        pendulumD.transform.position = new Vector3(-0.15f, 1.375f, 0f) + transform.position;
        pendulumD.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbD.velocity = Vector3.zero;
        m_RbD.angularVelocity = Vector3.zero;

        pendulumE.transform.position = new Vector3(-0.15f, 2f, 0f) + transform.position;
        pendulumE.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbE.velocity = Vector3.zero;
        m_RbE.angularVelocity = Vector3.zero;

        pendulumF.transform.position = new Vector3(-0.15f, 2.11f, 0f) + transform.position;
        pendulumF.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbF.velocity = Vector3.zero;
        m_RbF.angularVelocity = Vector3.zero;

        SetResetParameters();

        m_GoalDegree += m_GoalSpeed;
        UpdateGoalPosition();
    }
    public void SetResetParameters()
    {
        m_GoalRadius = Random.Range(1f, 1.3f);
        m_GoalDegree = Random.Range(0f, 360f);
        m_GoalSpeed = Random.Range(-2f, 2f);
        m_GoalDeviation = Random.Range(-1f, 1f);
        m_GoalDeviationFreq = Random.Range(0f, 3.14f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(pendulumA.transform.localPosition);
        sensor.AddObservation(pendulumA.transform.rotation);
        sensor.AddObservation(m_RbA.velocity);
        sensor.AddObservation(m_RbA.angularVelocity);

        sensor.AddObservation(pendulumB.transform.localPosition);
        sensor.AddObservation(pendulumB.transform.rotation);
        sensor.AddObservation(m_RbB.velocity);
        sensor.AddObservation(m_RbB.angularVelocity);

        sensor.AddObservation(pendulumC.transform.localPosition);
        sensor.AddObservation(pendulumC.transform.rotation);
        sensor.AddObservation(m_RbC.velocity);
        sensor.AddObservation(m_RbC.angularVelocity);

        sensor.AddObservation(pendulumD.transform.localPosition);
        sensor.AddObservation(pendulumD.transform.rotation);
        sensor.AddObservation(m_RbD.velocity);
        sensor.AddObservation(m_RbD.angularVelocity);

        sensor.AddObservation(pendulumE.transform.localPosition);
        sensor.AddObservation(pendulumE.transform.rotation);
        sensor.AddObservation(m_RbE.velocity);
        sensor.AddObservation(m_RbE.angularVelocity);

        sensor.AddObservation(pendulumF.transform.localPosition);
        sensor.AddObservation(pendulumF.transform.rotation);
        sensor.AddObservation(m_RbF.velocity);
        sensor.AddObservation(m_RbF.angularVelocity);

        sensor.AddObservation(ball.transform.position);
        sensor.AddObservation(hand.transform.localPosition);

        sensor.AddObservation(m_GoalSpeed); 
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //
        var actions = actionBuffers.ContinuousActions;

        var torque = Mathf.Clamp(actions[0], -1f, 1f) * 150f;
        m_RbA.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(actions[1], -1f, 1f) * 150f;
        m_RbB.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(actions[2], -1f, 1f) * 150f;
        m_RbC.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(actions[3], -1f, 1f) * 150f;
        m_RbD.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(actions[4], -1f, 1f) * 150f;
        m_RbE.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(actions[5], -1f, 1f) * 150f;
        m_RbF.AddTorque(new Vector3(0f, torque, 0f));

        m_GoalDegree += m_GoalSpeed;
        UpdateGoalPosition();
    }

    void UpdateGoalPosition()
    {
        if (!blHeuristic)
        {
            var m_GoalDegree_rad = m_GoalDegree * Mathf.PI / 180f;
            var goalX = m_GoalRadius * Mathf.Cos(m_GoalDegree_rad);
            var goalZ = m_GoalRadius * Mathf.Sin(m_GoalDegree_rad);
            var goalY = m_GoalHeight + m_GoalDeviation * Mathf.Cos(m_GoalDeviationFreq * m_GoalDegree_rad);

            ball.transform.position = new Vector3(goalX, goalY, goalZ) + transform.position;
        }
       
    }
}
