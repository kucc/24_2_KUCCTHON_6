using UnityEngine;

[ExecuteInEditMode]
public class GravityZone : MonoBehaviour
{
    public Transform gravityCenter;  // 중력 중심을 나타내는 오브젝트
    public float gravityStrength = 9.81f;  // 중력의 강도
    public float radiusMultiplier = 1f;  // Mesh의 반경에 곱할 배율 (N값)
    public Transform gravitySphere;  // 중력 범위를 나타낼 스피어 오브젝트

    private void Start()
    {
        UpdateGravityRadius();
    }

    // Inspector에서 값이 변경될 때마다 호출되어 반지름 업데이트
    private void OnValidate()
    {
        UpdateGravityRadius();
    }

    private void UpdateGravityRadius()
    {
        // Mesh 크기를 기준으로 중력 반경 계산
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            // Mesh bounds의 extents를 기준으로 반경 계산
            float baseRadius = meshFilter.sharedMesh.bounds.extents.magnitude; // Mesh의 기본 반경
            float gravityRadius = baseRadius * radiusMultiplier;  // 반경에 배율 적용

            // gravitySphere의 스케일을 중력 반경에 맞게 설정하여 시각적 반영
            if (gravitySphere != null)
            {
                float scale = gravityRadius * 2;  // 반지름 -> 스케일로 변환
                gravitySphere.localScale = new Vector3(scale, scale, scale);
            }
        }
        else
        {
            Debug.LogWarning("MeshFilter가 없습니다. GravityZone에 MeshFilter를 추가해주세요.");
        }
    }

    public bool IsWithinGravityZone(Vector3 position)
    {
        // 중력 범위 내에 있는지 확인
        float distance = Vector3.Distance(gravityCenter.position, position);
        float baseRadius = GetComponent<MeshFilter>().sharedMesh.bounds.extents.magnitude;
        return distance <= baseRadius * radiusMultiplier;
    }

    public Vector3 GetGravityForce(Transform body)
    {
        // 중력 중심 방향으로의 힘을 계산
        Vector3 direction = (gravityCenter.position - body.position).normalized;
        return direction * gravityStrength;
    }
}