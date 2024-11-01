using UnityEngine;

public class CollisionDetecter : MonoBehaviour
{
    public GameObject flame; // Flame GameObject
    private ParticleSystem flameParticles;
    public float reductionAmount = 1f; // Amount to reduce emission rate each time

    void Start()
    {
        if (flame != null)
        {
            flameParticles = flame.GetComponent<ParticleSystem>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            if (flameParticles != null)
            {
                // 현재 emission 값을 가져와서 1씩 감소
                var emission = flameParticles.emission;
                float currentEmissionRate = emission.rateOverTime.constant;

                // 감소시키고 Clamp로 최소값을 0으로 설정
                currentEmissionRate = Mathf.Max(0, currentEmissionRate - reductionAmount);
                emission.rateOverTime = currentEmissionRate;

                // emission rate가 0에 도달하면 불꽃 비활성화
                if (currentEmissionRate <= 0)
                {
                    flame.SetActive(false);
                    Debug.Log("Flame has been completely extinguished!");
                }
            }
        }
    }
}