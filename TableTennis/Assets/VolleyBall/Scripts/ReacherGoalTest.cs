using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReacherGoalTest : MonoBehaviour
{
    public float m_GoalHeight = 1.2f;
    
    public GameObject goal;
    float m_GoalRadius; //Radious of the goal zone
    float m_GoalDegree; //How much the goal rotates 
    float m_GoalSpeed;  //Speed of the goal rotation
    float m_GoalDeviation;  //How much goes up and down  
    float m_GoalDeviationFreq;  //Frequency of the goal up and down

    void Start()
    {
        SetResetParameters();
    }

    public void SetResetParameters()
    {
        m_GoalRadius = Random.Range(1f, 1.3f);
        m_GoalDegree = Random.Range(0f, 360f);
        m_GoalSpeed = Random.Range(-2f, 2f);
        m_GoalDeviation = Random.Range(-1f, 1f);
        m_GoalDeviationFreq = Random.Range(0f, 3.14f);
    }

    void Update()
    {
        m_GoalDegree += m_GoalSpeed;
        UpdateGoalPosition();
    }

    void UpdateGoalPosition()
    {
        var m_GoalDegree_rad = m_GoalDegree * Mathf.PI / 180f;
        var goalX = m_GoalRadius * Mathf.Cos(m_GoalDegree_rad);
        var goalZ = m_GoalRadius * Mathf.Sin(m_GoalDegree_rad);
        var goalY = m_GoalHeight + m_GoalDeviation * Mathf.Cos(m_GoalDeviationFreq * m_GoalDegree_rad);

        goal.transform.position = new Vector3(goalX, goalY, goalZ);
    }
}
