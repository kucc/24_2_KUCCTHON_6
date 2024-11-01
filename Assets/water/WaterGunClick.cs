using UnityEngine;
using UnityEngine.UI;

public class WaterGunClick : MonoBehaviour
{
    public GameObject waterPrefab;           // 물 발사체 프리팹
    public Transform firePoint;              // 발사 지점
    public float waterSpeed = 50f;           // 발사 속도
    public float fireInterval = 0.1f;        // 발사 간격 (초)
    public float angleStep = 5f;             // 각도 조절 단위
    public float lineLength = 2f;            // 보조선 길이

    public SpriteRenderer angleLineSprite;   // 보조선 스프라이트
    public HingeJoint2D ladderHinge;         // 사다리에 붙일 Hinge Joint

    public Button upButton;                  // 각도 증가 버튼
    public Button downButton;                // 각도 감소 버튼
    public Button shootButton;               // 발사 버튼

    private float fireAngle = 0f;            // 발사 각도
    private float fireTimer;                 // 발사 간격 타이머
    private bool isFiring = false;           // 발사 중 여부

    void Start()
    {
        // 보조선 초기 설정
        angleLineSprite.size = new Vector2(lineLength, angleLineSprite.size.y);
        UpdateFireAngleLine();

        // Hinge Joint 초기화
        if (ladderHinge != null)
        {
            ladderHinge.useLimits = true;
            JointAngleLimits2D limits = ladderHinge.limits;
            limits.min = -90f;
            limits.max = 90f;
            ladderHinge.limits = limits;
        }

        // 버튼 이벤트 등록
        upButton.onClick.AddListener(AngleUp);
        downButton.onClick.AddListener(AngleDown);
        shootButton.onClick.AddListener(FireWater);
    }

    void Update()
    {
        // 키보드 입력으로 각도 조정
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AngleUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AngleDown();
        }

        // 스페이스바 입력으로 발사 제어
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

    public void AngleUp()
    {
        fireAngle += angleStep;
    }

    public void AngleDown()
    {
        fireAngle -= angleStep;
    }

    void FireWater()
    {
        float radians = (fireAngle + 12) * Mathf.Deg2Rad;
        Vector2 fireDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

        GameObject water = Instantiate(waterPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D waterRb = water.GetComponent<Rigidbody2D>();

        // 힘을 가해 물 발사체를 발사
        waterRb.AddForce(fireDirection * waterSpeed, ForceMode2D.Impulse);
    }

    void UpdateFireAngleLine()
    {
        float radians = fireAngle * Mathf.Deg2Rad;
        float spriteWidth = angleLineSprite.bounds.size.x;
        //Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (spriteWidth / 2);

        //angleLineSprite.transform.position = (Vector2)firePoint.position + offset;
        //angleLineSprite.transform.rotation = Quaternion.Euler(0, 0, fireAngle + 12);
    }

    void UpdateLadderAngle()
    {
        if (ladderHinge != null)
        {
            ladderHinge.transform.rotation = Quaternion.Euler(0, 0, fireAngle);
        }
    }
}
