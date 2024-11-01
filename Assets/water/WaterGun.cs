using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject waterPrefab;       // 물 발사체 프리팹
    public Transform firePoint;          // 발사 지점
    public float waterSpeed = 50f;       // 발사 속도
    public float fireInterval = 0.1f;    // 발사 간격 (초)
    public float angleStep = 5f;         // 각도 조절 단위
    public float lineLength = 2f;        // 보조선 길이

    public SpriteRenderer angleLineSprite; // 보조선 스프라이트
    private float fireAngle = 0f;        // 발사 각도
    private float fireTimer;             // 발사 간격 타이머
    private bool isFiring = false;       // 발사 중 여부

    public HingeJoint2D ladderHinge;     // 사다리에 붙일 Hinge Joint

    void Start()
    {
        // 스프라이트 크기 설정 (lineLength에 맞게)
        angleLineSprite.size = new Vector2(lineLength, angleLineSprite.size.y);
        UpdateFireAngleLine(); // 초기 위치 설정

        // Hinge Joint 초기화
        if (ladderHinge != null)
        {
            ladderHinge.useLimits = true; // 회전 제한 사용
            JointAngleLimits2D limits = ladderHinge.limits;
            limits.min = -90f; // 회전 최소 각도
            limits.max = 90f;  // 회전 최대 각도
            ladderHinge.limits = limits;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            fireAngle += angleStep;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            fireAngle -= angleStep;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFiring = true;
            fireTimer = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFiring = false;
        }

        if (isFiring)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireInterval)
            {
                FireWater();
                fireTimer = 0f;
            }
        }

        UpdateFireAngleLine();
        UpdateLadderAngle();
    }

    void FireWater()
    {
        float radians = (fireAngle+12) * Mathf.Deg2Rad;
        Vector2 fireDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

        GameObject water = Instantiate(waterPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D waterRb = water.GetComponent<Rigidbody2D>();

        // 힘을 가해 물 발사체를 발사
        waterRb.AddForce(fireDirection * waterSpeed, ForceMode2D.Impulse);
    }

    void UpdateFireAngleLine()
    {
        // fireAngle에 맞춰 보조선 위치와 회전 조정
        angleLineSprite.transform.position = firePoint.position; // firePoint 위치에 맞춤
        angleLineSprite.transform.rotation = Quaternion.Euler(0, 0, fireAngle+12); // Z축 기준 회전
    }


    void UpdateLadderAngle()
    {
        // Hinge Joint를 통해 사다리의 각도를 fireAngle에 맞춰 설정
        if (ladderHinge != null)
        {
            ladderHinge.transform.rotation = Quaternion.Euler(0, 0, fireAngle);
        }
    }
}
