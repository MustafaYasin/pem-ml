using UnityEngine;

public class VolleyballController : MonoBehaviour
{
    [HideInInspector]
    public VolleyballEnvController envController;

    public GameObject purpleGoal;
    public GameObject blueGoal;
    Collider purpleGoalCollider;
    Collider blueGoalCollider;

    void Start()
    {
        envController = GetComponentInParent<VolleyballEnvController>();
        purpleGoalCollider = purpleGoal.GetComponent<Collider>();
        blueGoalCollider = blueGoal.GetComponent<Collider>();
    }

    /// <summary>
    /// Detects whether the ball lands in the blue, purple, or out of bounds area
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("boundary"))
        {
            // ball went out of bounds
            envController.ResolveEvent(Event.HitOutOfBounds);
           Debug.Log("Ball went out of bounds: Event.HitOutOfBounds is called");
        }
        else if (other.gameObject.CompareTag("blueBoundary"))
        {
            // ball hit into blue side
            envController.ResolveEvent(Event.HitIntoBlueArea);
           Debug.Log("Ball hit into blue side: Event.HitIntoBlueArea is called");
        }
        else if (other.gameObject.CompareTag("purpleBoundary"))
        {
            // ball hit into purple side
            envController.ResolveEvent(Event.HitIntoPurpleArea);
            Debug.Log("Ball  hit into purple side: Event.HitIntoPurpleArea is called");
        }
        else if (other.gameObject.CompareTag("purpleGoal"))
        {
            // ball hit purple goal (blue side court)
            envController.ResolveEvent(Event.HitPurpleGoal);
            Debug.Log("Ball hit purple goal (blue side court): Event.HitPurpleGoal is called");
        }
        else if (other.gameObject.CompareTag("blueGoal"))
        {
            // ball hit blue goal (purple side court)
            envController.ResolveEvent(Event.HitBlueGoal);
            Debug.Log("Ball hit blue goal (purple side court): Event.HitBlueGoal is called");
        }
    }
}