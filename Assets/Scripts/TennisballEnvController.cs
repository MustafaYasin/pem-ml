using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Blue = 0,
    Purple = 1,
    Default = 2
}

public enum Event
{
    HitPurpleGoal = 0,
    HitBlueGoal = 1,
    HitOutOfBounds = 2,
    HitIntoBlueArea = 3,
    HitIntoPurpleArea = 4
}

public class TennisballEnvController : MonoBehaviour
{
    int ballSpawnSide;

    TennisballSettings volleyballSettings;

    //public VolleyballAgent blueAgent;
    //public VolleyballAgent purpleAgent;

    public RobotAgent blueAgent;
    public RobotAgent purpleAgent;

    public List<RobotAgent> AgentsList = new List<RobotAgent>();
    List<Renderer> RenderersList = new List<Renderer>();

    Rigidbody blueAgentRb;
    Rigidbody purpleAgentRb;

    public GameObject ball;
    Rigidbody ballRb;

    public GameObject blueGoal;
    public GameObject purpleGoal;

    Renderer blueGoalRenderer;

    Renderer purpleGoalRenderer;

    Team lastHitter;
    Team penultHitter;

    private int resetTimer;
    public int MaxEnvironmentSteps;

    void Start()
    {

        // Used to control agent & ball starting positions
        blueAgentRb = blueAgent.GetComponent<Rigidbody>();
        purpleAgentRb = purpleAgent.GetComponent<Rigidbody>();
        ballRb = ball.GetComponent<Rigidbody>();

        // Starting ball spawn side
        // -1 = spawn blue side, 1 = spawn purple side
        var spawnSideList = new List<int> { -1, 1 };
        ballSpawnSide = spawnSideList[Random.Range(0, 2)];

        // Render ground to visualise which agent scored
        blueGoalRenderer = blueGoal.GetComponent<Renderer>();
        purpleGoalRenderer = purpleGoal.GetComponent<Renderer>();
        RenderersList.Add(blueGoalRenderer);
        RenderersList.Add(purpleGoalRenderer);

        volleyballSettings = FindObjectOfType<TennisballSettings>();

        ResetScene();
    }

    /// <summary>
    /// Tracks which agent last had control of the ball
    /// </summary>
    public void UpdateLastHitter(Team team)
    {
        penultHitter = lastHitter;
        lastHitter = team;
    }

    /// <summary>
    /// Resolves scenarios when ball enters a trigger and assigns rewards.
    /// Example reward functions are shown below.
    /// To enable Self-Play: Set either Purple or Blue Agent's Team ID to 1.
    /// </summary>
    public void ResolveEvent(Event triggerEvent)
    {
        switch (triggerEvent)
        {
            case Event.HitOutOfBounds:
                if (lastHitter == Team.Blue)
                {
                    // apply penalty to blue agent
                    //blueAgent.AddReward(-0.1f);
                    // purpleAgent.AddReward(0.1f);
                }
                else if (lastHitter == Team.Purple)
                {
                    // apply penalty to purple agent
                    // purpleAgent.AddReward(-0.1f);
                    // blueAgent.AddReward(0.1f);
                }

                // end episode
                blueAgent.EndEpisode();
                purpleAgent.EndEpisode();
                ResetScene();
                break;

            case Event.HitBlueGoal:
                Debug.Log(lastHitter);
                if (lastHitter == Team.Purple) //purple hit the ball into it's own side 
                {
                    //Debug.Log("purple hits the ball into blueGoal");
                    purpleAgent.AddReward(-2);
                }
                if (lastHitter == Team.Blue)
                { //purple didn't catch the ball from blue
                    purpleAgent.AddReward(-1);
                   // Debug.Log("purple didn't catch the ball from blue");

                }
                StartCoroutine(GoalScoredSwapGroundMaterial(volleyballSettings.blueGoalMaterial, RenderersList, .5f));
                blueAgent.EndEpisode();
                purpleAgent.EndEpisode();
                ResetScene();
                break;
            // end episode


            case Event.HitPurpleGoal:
                if (lastHitter == Team.Blue)//blue hit the ball into it's own side 
                {
                    blueAgent.AddReward(-2);
                  //  Debug.Log("blue hits the ball into it's own side");
                }
                if (lastHitter == Team.Purple) //purple win
                {
                    blueAgent.AddReward(-1);
                   // Debug.Log("blue didn't catch the ball from purple");

                }
                StartCoroutine(GoalScoredSwapGroundMaterial(volleyballSettings.purpleGoalMaterial, RenderersList, .5f));
                blueAgent.EndEpisode();
                purpleAgent.EndEpisode();
                ResetScene();
                break;



            case Event.HitIntoBlueArea:
                if (lastHitter == Team.Purple)
                {
                    //purpleAgent.AddReward(3);
                    purpleAgent.AddReward(2);
                   // Debug.Log("purple hits ball into bluearea");
                }
                break;

            case Event.HitIntoPurpleArea:
                if (lastHitter == Team.Blue)
                {
                    // blueAgent.AddReward(3);
                    blueAgent.AddReward(2);
                   // Debug.Log("Blue hits ball into bluearea");
                }
                break;
        }
    }

    public void ResolveCollisionEnter()
    {


        if (lastHitter == Team.Blue && penultHitter == Team.Purple)
        {
            blueAgent.AddReward(4);
            purpleAgent.AddReward(1);
           // Debug.Log("blue catches the ball from purple");
        }
        else if (lastHitter == Team.Blue)
        {
            blueAgent.AddReward(2);
           // Debug.Log("blue bat touches the ball");
        }
        else if (lastHitter == Team.Purple && penultHitter == Team.Blue)
        {

            purpleAgent.AddReward(4);
            blueAgent.AddReward(1);
           // Debug.Log("purple catches the ball from Blue");
        }
        else if (lastHitter == Team.Purple)
        {
            purpleAgent.AddReward(2);
           // Debug.Log("purple bat touches the ball");
        }

    }




    /// <summary>
    /// Changes the color of the ground for a moment.
    /// </summary>
    /// <returns>The Enumerator to be used in a Coroutine.</returns>
    /// <param name="mat">The material to be swapped.</param>
    /// <param name="time">The time the material will remain.</param>
    IEnumerator GoalScoredSwapGroundMaterial(Material mat, List<Renderer> rendererList, float time)
    {
        foreach (var renderer in rendererList)
        {
            renderer.material = mat;
        }

        yield return new WaitForSeconds(time); // wait for 2 sec

        foreach (var renderer in rendererList)
        {
            renderer.material = volleyballSettings.defaultMaterial;
        }

    }

    /// <summary>
    /// Called every step. Control max env steps.
    /// </summary>
    void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {
            blueAgent.EpisodeInterrupted();
            purpleAgent.EpisodeInterrupted();
            ResetScene();
        }
    }

    /// <summary>
    /// Reset agent and ball spawn conditions.
    /// </summary>
    public void ResetScene()
    {
        resetTimer = 0;

        lastHitter = Team.Default; // reset last hitter

        // reset ball to starting conditions
        ResetBall();
    }

    /// <summary>
    /// Reset ball spawn conditions
    /// </summary>
    void ResetBall()
    {
        var randomPosX = Random.Range(-2f, 2f);
        var randomPosZ = Random.Range(6f, 10f);
        var randomPosY = Random.Range(6f, 8f);

        // alternate ball spawn side
        // -1 = spawn blue side, 1 = spawn purple side
        ballSpawnSide = -1 * ballSpawnSide;

        if (ballSpawnSide == -1)
        {
            ball.transform.localPosition = new Vector3(randomPosX, randomPosY, randomPosZ);
        }
        else if (ballSpawnSide == 1)
        {
            ball.transform.localPosition = new Vector3(randomPosX, randomPosY, -1 * randomPosZ);
        }

        ballRb.angularVelocity = Vector3.zero;
        ballRb.velocity = Vector3.zero;
    }
}
