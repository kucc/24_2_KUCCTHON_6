using UnityEngine;

public class CollisionDetecter : MonoBehaviour
{
    // Reference to the Flame GameObject and its ParticleSystem
    public GameObject flame;
    private ParticleSystem flameParticles;
    public float reductionAmount = 0.1f; // Amount to reduce the emission each time
    private float currentEmission = 1.0f; // Initial emission rate (normalized)

    void Start()
    {
        // Get the ParticleSystem component from the Flame object
        if (flame != null)
        {
            flameParticles = flame.GetComponent<ParticleSystem>();
        }
    }

    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object we collided with has the tag "Water"
        if (collision.gameObject.CompareTag("Water"))
        {
            // Reduce the emission of the flame
            if (flameParticles != null && currentEmission > 0)
            {
                currentEmission -= reductionAmount;
                currentEmission = Mathf.Clamp(currentEmission, 0, 1); // Clamp to ensure it doesn't go below 0

                // Update the emission rate
                var emission = flameParticles.emission;
                emission.rateOverTime = emission.rateOverTime.constant * currentEmission;

                // If the emission reaches 0, disable the flame
                if (currentEmission <= 0)
                {
                    flame.SetActive(false);
                    Debug.Log("Flame has been completely extinguished!");
                }
            }
        }
    }
}