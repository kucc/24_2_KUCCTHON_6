using UnityEngine;

public class WaterOnGround : MonoBehaviour
{
    public string planetTag = "Planet"; // Planet ������Ʈ�� �±�

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Planet �±׸� ���� ������Ʈ�� �浹�ߴ��� Ȯ��
        if (collision.gameObject.CompareTag(planetTag))
        {
            // 1�� �Ŀ� Water ������Ʈ ��Ȱ��ȭ
            Invoke("Disappear", 1f);
        }
    }

    // Water ������Ʈ�� ��Ȱ��ȭ�ϴ� �޼���
    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
