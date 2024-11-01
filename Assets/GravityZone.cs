using UnityEngine;

public class GravityZone : MonoBehaviour
{
    public Transform gravityCenter;  // 중력 중심을 나타내는 오브젝트
    public float gravityStrength = 9.81f;  // 중력의 강도
    public float gravityRadius = 5f;  // 중력 범위의 반경
    public Transform gravitySphere;  // 중력 범위를 나타낼 스피어 오브젝트

    private void Start()
    {
        // 중력 반경에 맞춰 스피어 오브젝트 크기를 설정
        if (gravitySphere != null)
        {
            float scale = gravityRadius * 2;  // 반지름 -> 스케일로 변환
            gravitySphere.localScale = new Vector3(scale, scale, scale);
        }
    }

    public bool IsWithinGravityZone(Vector3 position)
    {
        // 대상 위치가 중력 범위 내에 있는지 확인
        float distance = Vector3.Distance(gravityCenter.position, position);
        return distance <= gravityRadius;
    }

    public Vector3 GetGravityForce(Transform body)
    {
        // 중력 중심 방향으로의 힘을 계산
        Vector3 direction = (gravityCenter.position - body.position).normalized;

        // 거리 제곱에 반비례한 중력 강도 적용
        return direction * gravityStrength;
    }
}