using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -9.81f; // 중력의 강도 설정

    public void Attract(Transform body)
    {
        // 중력 방향을 계산 (대상 오브젝트에서 중력 중심으로)
        Vector3 gravityDirection = (transform.position - body.position).normalized;
        
        // 중력의 방향으로 힘을 가합니다.
        Rigidbody rb = body.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(gravityDirection * gravity * rb.mass); // 질량에 따른 중력 가속도 적용
        }
    }
}