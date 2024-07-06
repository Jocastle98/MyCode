using UnityEngine;

public class SpinHandle : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Transform[] tilemapLayers; // �� Ÿ�ϸ� ���̾ �����մϴ�.
    public Vector3 rotationCenter = Vector3.zero; // ������ ȸ�� �߽���

    void Update()
    {
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                // ȭ���� ���� ���� ��ġ �� �ݽð� �������� ȸ��
                if (touchPosition.x < Screen.width / 2)
                {
                    RotateLeft();
                }
                // ȭ���� ������ ���� ��ġ �� �ð� �������� ȸ��
                else if (touchPosition.x >= Screen.width / 2)
                {
                    RotateRight();
                }
            }
        }
    }

    void RotateLeft()
    {
        foreach (var layer in tilemapLayers)
        {
            layer.RotateAround(rotationCenter, Vector3.forward, 60f); // ������ �߽��� �������� 60�� ȸ��
        }
    }

    void RotateRight()
    {
        foreach (var layer in tilemapLayers)
        {
            layer.RotateAround(rotationCenter, Vector3.back, 60f); // ������ �߽��� �������� 60�� ȸ��
        }
    }
}
