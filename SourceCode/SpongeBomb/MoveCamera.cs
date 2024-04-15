using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    //따라가야할 대상 연결 할 변수
    public Transform targetTr;
    //Main Camera 자신의 Transform 컴포넌트
    private Transform camTr;
    //따라갈 대상으로부터 떨어질 거리 

    [Range(2.0f, 20.0f)] public float distance = 10.5f;

    //Y축으로 이동할 높이
    [Range(0.0f, 10.0f)] public float height = 4.2f;

    //반응속도
    public float damping = 10.0f;

    //smoothdamp에서 사용할 변수
    private Vector3 velocity = Vector3.zero;

    private float targetOffset = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 pos = camTr.position = targetTr.position //플레이어 위치
                  + (-targetTr.forward * distance) //플레이어 뒤에 있어야하므로 -targetTr.forward이다.
                  + (Vector3.up * height);
        camTr.position = Vector3.SmoothDamp(
            camTr.position, //시작위치
            pos,            //목표위치
            ref velocity,   //현재속도
            damping);       //목표위치까지 도달시간

         camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));

    }
}

