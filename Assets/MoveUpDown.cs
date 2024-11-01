using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float speed = 2f; // 이동 속도
    public float range = 5f; // 왕복 운동 범위 (중심에서의 최대 거리)

    private Vector3 startPosition;

    void Start()
    {
        // 초기 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // 현재 시간에 따라 왕복 운동 계산
        float newY = startPosition.y + Mathf.PingPong(Time.time * speed, range) - (range / 2);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
