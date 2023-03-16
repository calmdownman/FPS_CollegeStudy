using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f; //이동 속도 변수
    CharacterController cc; // 캐릭터컨트롤러 변수
    float gravity = -20f; //중력 변수
    float yVelocity = 0; //수직 속력 변수
    public float jumpPower = 10f;
    public bool isJumping = false; //점프 상태변수

    int hp = 100; //플레이어 체력 변수
    // Start is called before the first frame update
    void Start()
    {
        //캐릭터 컨트롤러 컴포넌트 받아오기
       cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //사용자 입력을 받는다.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //이동 방향 설정
        Vector3 dir = new Vector3(h,0,v);
        dir = dir.normalized;
        //메인 카메라를 기준으로 방향을 변환한다
        dir = Camera.main.transform.TransformDirection(dir);
        //이동 속도에 맞춰 이동한다
        //transform.position += dir * moveSpeed * Time.deltaTime;

        //만일 바닥에 닿아있다면
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping) //만일 점핑 중이라면
            {
                isJumping = false; //점프전 상태로 초기화
            }
            yVelocity = 0;
        }

        //만일 키보드 스페이스바를 눌렀고, 점프하지 않은 상태라면
        if (Input.GetButtonDown("Jump") && !isJumping) //만일 키보드 SpaceBar를 눌렀다면
        {
            yVelocity = jumpPower; //수직 속도에 점프력을 적용
            isJumping = true;
        }

        yVelocity += gravity * Time.deltaTime; //캐릭터 수직속도에 중력 값을 적용
        //Debug.Log(yVelocity.ToString());
        dir.y = yVelocity; //중력값을 적용한 수직속도 할당
        //이동 속도에 맞춰 이동
        cc.Move(dir*moveSpeed*Time.deltaTime);
    }
    public void DamageAction(int damage)
    {
        hp -= damage; //에너미의 공격력 만큼 플레이어 체력을 감소
    }
}
