using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float speed = 2f; // �̵� �ӵ�
    public float range = 5f; // �պ� � ���� (�߽ɿ����� �ִ� �Ÿ�)

    private Vector3 startPosition;

    void Start()
    {
        // �ʱ� ��ġ ����
        startPosition = transform.position;
    }

    void Update()
    {
        // ���� �ð��� ���� �պ� � ���
        float newY = startPosition.y + Mathf.PingPong(Time.time * speed, range) - (range / 2);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
