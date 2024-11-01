using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject waterPrefab;          // 물 발사체 프리팹
    public Transform firePoint;             // 물 발사 시작 지점
    public float waterSpeed = 270f;          // 발사 속도
    public float fireInterval = 0.05f;       // 발사 간격
    public float angleStep = 5f;            // 각도 조절 단위
    public float lineLength = 2f;           // 보조선 길이 (사다리 길이와 동일하게 설정)

    public SpriteRenderer ladderSprite;     // 사다리 스프라이트
    public Transform shootPos;              // 물 발사 위치 오브젝트
    private float fireAngle = 0f;           // 발사 각도
    private float fireTimer;                // 발사 간격 타이머
    private bool isFiring = false;          // 발사 여부

    void Start()
    {
        ladderSprite.size = new Vector2(lineLength, ladderSprite.size.y); // 사다리 크기 조정
        UpdateLadderAndShootPos(); // 초기 위치 설정
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

        UpdateLadderAndShootPos();
    }

    void FireWater()
    {
        float radians = fireAngle * Mathf.Deg2Rad;
        Vector2 fireDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

        GameObject water = Instantiate(waterPrefab, shootPos.position, Quaternion.identity);
        Rigidbody2D waterRb = water.GetComponent<Rigidbody2D>();

        waterRb.linearVelocity = fireDirection * waterSpeed;
    }

    void UpdateLadderAndShootPos()
    {
        // 사다리 각도에 따라 회전
        ladderSprite.transform.position = firePoint.position; // 사다리 시작 지점은 firePoint
        ladderSprite.transform.rotation = Quaternion.Euler(0, 0, fireAngle); // 사다리 각도 조정

        // ladderSprite의 실제 크기 가져오기
        float ladderLength = ladderSprite.bounds.size.x;

        // 사다리 오른쪽 끝에 shootPos 위치 업데이트
        float radians = fireAngle * Mathf.Deg2Rad;
        Vector2 ladderEndPos = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * ladderLength; // 사다리 끝점 계산
        shootPos.position = (Vector2)firePoint.position + ladderEndPos;
    }
}
