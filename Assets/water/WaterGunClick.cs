using UnityEngine;
using UnityEngine.UI;

public class WaterGunClick : MonoBehaviour
{
    public GameObject waterPrefab;         // 물 발사체 프리팹
    public Transform firePoint;            // 발사 지점
    public float waterSpeed = 50f;         // 발사 속도
    public float fireInterval = 0.1f;      // 발사 간격 (초)
    public float angleStep = 5f;           // 각도 조절 단위
    public SpriteRenderer angleLineSprite; // 보조선 스프라이트
    private float fireAngle = 0f;          // 발사 각도
    private float fireTimer;               // 발사 간격 타이머
    private bool isFiring = false;         // 발사 중 여부

    public Button upButton;               // UI 각도 증가 버튼
    public Button downButton;             // UI 각도 감소 버튼
    public Button shootButton;            // UI 발사 버튼

    void Start()
    {
        // 스프라이트 크기 설정
        angleLineSprite.size = new Vector2(angleLineSprite.size.x, angleLineSprite.size.y);
        UpdateFireAngleLine(); // 초기 위치 설정

        // UI 버튼 이벤트 등록
        upButton.onClick.AddListener(AngleUp);
        downButton.onClick.AddListener(AngleDown);
        shootButton.onClick.AddListener(FireWater); // Shoot 버튼에 발사 함수 연결
    }

    void Update()
    {
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
    }

    // 버튼 이벤트로 각도 증가 함수
    public void AngleUp()
    {
        fireAngle += angleStep;
    }

    // 버튼 이벤트로 각도 감소 함수
    public void AngleDown()
    {
        fireAngle -= angleStep;

        Debug.Log("fwefwe");
    }

    // 발사 함수
    void FireWater()
    {
        float radians = fireAngle * Mathf.Deg2Rad;
        Vector2 fireDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

        GameObject water = Instantiate(waterPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D waterRb = water.GetComponent<Rigidbody2D>();

        waterRb.linearVelocity = fireDirection * waterSpeed;
    }

    void UpdateFireAngleLine()
    {
        float radians = fireAngle * Mathf.Deg2Rad;
        float spriteWidth = angleLineSprite.bounds.size.x;
        Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (spriteWidth / 2);

        angleLineSprite.transform.position = (Vector2)firePoint.position + offset;
        angleLineSprite.transform.rotation = Quaternion.Euler(0, 0, fireAngle); // Z축 기준 회전
    }
}
