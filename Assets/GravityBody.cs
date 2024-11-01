using UnityEngine;

public class GravityBody : MonoBehaviour
{
    private GravityAttractor attractor;  // 중력 원천 객체
    private Rigidbody rb;

    void Start()
    {
        attractor = FindObjectOfType<GravityAttractor>(); // GravityAttractor 객체 찾기
        rb = GetComponent<Rigidbody>();

        // Unity 기본 중력 비활성화
        rb.useGravity = false;
        //rb.constraints = RigidbodyConstraints.FreezeRotation; // 회전을 막아 오브젝트가 중심으로 향하게 고정
    }

    void FixedUpdate()
    {
        // attractor가 존재하면 Attract 메서드를 호출하여 중력 효과를 적용합니다.
        if (attractor != null)
        {
            attractor.Attract(transform); // 매 프레임 중력 적용
        }
    }
}