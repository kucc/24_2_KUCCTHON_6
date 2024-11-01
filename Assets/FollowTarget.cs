using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // Reference to the Flame GameObject
    public Transform flame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure the flame reference is not null
        if (flame != null)
        {
            // Set the position of this object to the position of the Flame object
            transform.position = flame.position;
        }
    }
}
