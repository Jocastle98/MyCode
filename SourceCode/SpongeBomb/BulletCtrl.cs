using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    // �Ѿ��� �ı���
    public float damage = 20.0f;
    // �Ѿ� �߻� ��
    public float force = 7000.0f;

    private Rigidbody rb;

    void Start()
    {
        // Rigidbody ������Ʈ�� ����
        rb = GetComponent<Rigidbody>();

        // �Ѿ��� ���� �������� ��(Force)�� ���Ѵ�.
        rb.AddForce(transform.forward * force);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
