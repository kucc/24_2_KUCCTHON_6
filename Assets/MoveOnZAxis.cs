using UnityEngine;

public class MoveOnZAxis : MonoBehaviour
{
    public float forceStrength = 10f;  // 힘의 크기
    private Rigidbody rb;

    void Start()
    {
        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 스페이스 키 입력 감지
        if (Input.GetKey(KeyCode.Space))
        {
            // Z축 방향으로 힘을 가함
            rb.AddForce(Vector3.forward * forceStrength, ForceMode.Force);
        }
    }
}