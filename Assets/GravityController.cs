using UnityEngine;
using System.Collections.Generic;

public class GravityController : MonoBehaviour
{
    public List<GravityZone> gravityZones;  // 중력 영역 리스트
    public Rigidbody body;  // 중력의 영향을 받을 오브젝트

    void FixedUpdate()
    {
        GravityZone nearestZone = null;
        float nearestDistance = float.MaxValue;

        // 모든 중력 영역 확인
        foreach (var zone in gravityZones)
        {
            if (zone.IsWithinGravityZone(body.position))
            {
                float distance = Vector3.Distance(zone.gravityCenter.position, body.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestZone = zone;
                }
            }
        }

        // 가장 가까운 중력 중심으로 끌어당기는 힘을 적용
        if (nearestZone != null)
        {
            Vector3 gravityForce = nearestZone.GetGravityForce(body.transform);
            
            Debug.Log(gravityForce);
            body.AddForce(gravityForce, ForceMode.Acceleration);
        }
    }
}