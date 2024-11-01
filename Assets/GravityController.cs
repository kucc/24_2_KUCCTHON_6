using UnityEngine;
using System.Collections.Generic;

public class GravityController : MonoBehaviour
{
    public List<GravityZone> gravityZones; // 중력 영역 리스트
    public string gravityAffectedTag = "GravityAffected"; // 중력의 영향을 받을 오브젝트의 태그
    private List<Rigidbody2D> affectedBodies = new List<Rigidbody2D>(); // 중력의 영향을 받을 모든 Rigidbody2D

    void Update()
    {
        // 태그를 가진 오브젝트들을 계속해서 갱신
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(gravityAffectedTag);
        affectedBodies.Clear(); // 리스트를 비워서 매 프레임 새로 채웁니다

        foreach (var obj in taggedObjects)
        {
            Rigidbody2D body = obj.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                affectedBodies.Add(body);
            }
        }
    }

    void FixedUpdate()
    {
        foreach (var body in affectedBodies)
        {
            GravityZone nearestZone = null;
            float nearestDistance = float.MaxValue;

            // 모든 중력 영역 확인
            foreach (var zone in gravityZones)
            {
                if (zone.IsWithinGravityZone(body.position))
                {
                    float distance = Vector2.Distance(zone.gravityCenter.position, body.position);
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
                Vector2 gravityForce = nearestZone.GetGravityForce(body.transform);
                body.AddForce(gravityForce, ForceMode2D.Force);
            }
        }
    }
}
