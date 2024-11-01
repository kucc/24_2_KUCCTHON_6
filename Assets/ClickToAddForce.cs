using UnityEngine;

public class ClickToAddForce : MonoBehaviour
{
    public float forceStrength = 5f;  // 힘의 크기
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 컴포넌트 가져오기
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭 감지
        {
            // 마우스 클릭 위치를 월드 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;  // Z축을 0으로 설정하여 2D 평면 유지

            // 클릭된 위치로 향하는 방향 벡터 계산
            Vector2 direction = ((Vector2)mousePosition - rb.position).normalized;

            // 방향으로 힘을 가함
            rb.AddForce(direction * forceStrength, ForceMode2D.Impulse);
        }
    }
}