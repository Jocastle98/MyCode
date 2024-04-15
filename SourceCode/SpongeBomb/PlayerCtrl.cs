using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{


    private float h = 0.0f;
    private float v = 0.0f;

    private Transform tr;           //접근해야하는 컴포넌트는 반드시 변수에 할당 후 사용
    public float moveSpeed = 10.0f; // 이동속도 변수


    public float rotSpeed = 1.0f; //회전속도 벼수

    private Animation anim;

    void Start()
    {
        //스크립트 처음에 Transform컴포넌트 할당
        tr = GetComponent<Transform>();
        //애니메이션 실행
      //  anim = GetComponent<Animation>();
       // anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");    //A,D,Left,Right키를 누럴ㅆ을 때
        v = Input.GetAxis("Vertical");//-1~1          //W,D ,Up, Down키를 눌렀을 떄
                                      //(0,0,0.5) + ( -0.5,0,0) = > (-0.5,0,0.5)

        //전후좌우 이동 방향 벡터 계산
        //Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //Translate(이동방향 *속도*변위 값 *TimedeltaTime, 기준좌표
       // tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);

        //Vector3.up 축을 기준으로 rotSpeed만큼의 속도로 회전


        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        //주인공 캐릭터의 애니메이션 설정
      //  PlayerAnim(h, v);

    }

   /* void PlayerAnim(float h, float v)
    {

        //키보드 입력값 기준으로 동작 애니메이션 수행
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f); //전진애니메이션 , 0.25초 간격으로 합치는 블렌딩

        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f); //후진
        }

        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f); //오른
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f); //왼쪽
        }

        else
        {
            anim.CrossFade("Idle", 0.25f);//정지 시 Idle 애니 실행
        }

    }
*/
}
