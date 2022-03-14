using UnityEngine;

public class VolleyBallController : MonoBehaviour
{
    [HideInInspector]
    public VolleyBallEnvController envController;

    public GameObject redGoal;
    public GameObject blueGoal;
    Collider redGoalCollider;
    Collider blueGoalCollider;

    // Start is called before the first frame update
    void Start()
    {
        envController = GetComponentInParent<VolleyBallEnvController>();
        redGoalCollider = redGoal.GetComponent<Collider>();
        blueGoalCollider = blueGoal.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("boundary"))
        {
            // ball went out of bounds
        }
        else if (other.gameObject.CompareTag("blueBoundary"))
        {
            // ball hit into blue side

        }
        else if (other.gameObject.CompareTag("redBoundary"))
        {
            // ball hit into red side

        }
        else if (other.gameObject.CompareTag("redGoal"))
        {
            // ball hit purple goal (blue side court)

        }
        else if (other.gameObject.CompareTag("blueGoal"))
        {
            // ball hit blue goal (red side court)

        }
    }
}
