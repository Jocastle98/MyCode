using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{


    // 컴포넌트를 저장할 변수
    private Transform tr;
    private Rigidbody rb;

    // 총알 맞은 횟수를 누적시킬 변수
    private int hitcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        
    }

    // 충돌 시 발생하는 콜백 함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            // 총알 맞은 횟수를 증가시키고 3회이상이면 폭발처리
            if (++hitcount == 3)
            {
                Destroy(gameObject, 1.0f);
            }
        }
    }



 
    void Update()
    {

    }
}
