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
        // Get input from the horizontal and vertical axes
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D keys or Left/Right arrow keys
        float moveVertical = Input.GetAxis("Vertical");     // W/S keys or Up/Down arrow keys

        // Create a new Vector3 movement based on the input
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        // Move the object using the transform component
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
