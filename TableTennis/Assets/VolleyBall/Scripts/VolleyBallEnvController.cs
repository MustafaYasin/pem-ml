using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyTeam
{
    Blue = 0,
    Red = 1,
    Default = 2
}

public enum Event
{
    HitRedGoal = 0,
    HitBlueGoal = 1,
    HitOutofBounds = 2,
    HitIntoBlueArea = 3,
    HitIntoRedArea = 4
}

public class VolleyBallEnvController : MonoBehaviour
{
    int ballSpawnSide;

    VolleyballSettings volleyballSettings;

    public VolleyBallAgent blueAgent;
    public VolleyBallAgent redAgent;

    public List<VolleyBallAgent> AgentsList = new List<VolleyBallAgent>();
    List<Renderer> RenderersList = new List<Renderer>();

    Rigidbody blueAgentRb;
    Rigidbody redAgentRb;

    public GameObject ball;
    Rigidbody ballRb;

    public GameObject blueGoal;
    public GameObject redGoal;

    Renderer blueGoalRenderer;
    Renderer purpleGoalRenderer;

    MyTeam lastHitter;

    private int resetTimer;
    public int MaxEnvironmetSteps;

    // Start is called before the first frame update
    void Start()
    {
        // Used to control agent & ball starting positions
        blueAgentRb = blueAgent.GetComponent<Rigidbody>();
        redAgentRb = redAgent.GetComponent<Rigidbody>();
        ballRb = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
