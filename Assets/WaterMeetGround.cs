using UnityEngine;

public class WaterOnGround : MonoBehaviour
{
    public string planetTag = "Planet"; // Planet 오브젝트의 태그

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Planet 태그를 가진 오브젝트와 충돌했는지 확인
        if (collision.gameObject.CompareTag(planetTag))
        {
            // 1초 후에 Water 오브젝트 비활성화
            Invoke("Disappear", 1f);
        }
    }

    // Water 오브젝트를 비활성화하는 메서드
    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
