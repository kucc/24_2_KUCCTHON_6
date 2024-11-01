using UnityEngine;

public class input_control : MonoBehaviour
{
    // Speed variable to control how fast the object moves
    public float speed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create a new Vector3 to store movement
        Vector3 movement = Vector3.zero;

        // Check for arrow key input and adjust movement accordingly
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.up; // Move up
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.down; // Move down
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left; // Move left
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right; // Move right
        }

        // Move the object using the transform component
        transform.position += movement * speed * Time.deltaTime;
    }
}
