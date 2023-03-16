using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f; //�̵� �ӵ� ����
    CharacterController cc; // ĳ������Ʈ�ѷ� ����
    float gravity = -20f; //�߷� ����
    float yVelocity = 0; //���� �ӷ� ����
    public float jumpPower = 10f;
    public bool isJumping = false; //���� ���º���

    int hp = 100; //�÷��̾� ü�� ����
    // Start is called before the first frame update
    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
       cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //����� �Է��� �޴´�.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //�̵� ���� ����
        Vector3 dir = new Vector3(h,0,v);
        dir = dir.normalized;
        //���� ī�޶� �������� ������ ��ȯ�Ѵ�
        dir = Camera.main.transform.TransformDirection(dir);
        //�̵� �ӵ��� ���� �̵��Ѵ�
        //transform.position += dir * moveSpeed * Time.deltaTime;

        //���� �ٴڿ� ����ִٸ�
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping) //���� ���� ���̶��
            {
                isJumping = false; //������ ���·� �ʱ�ȭ
            }
            yVelocity = 0;
        }

        //���� Ű���� �����̽��ٸ� ������, �������� ���� ���¶��
        if (Input.GetButtonDown("Jump") && !isJumping) //���� Ű���� SpaceBar�� �����ٸ�
        {
            yVelocity = jumpPower; //���� �ӵ��� �������� ����
            isJumping = true;
        }

        yVelocity += gravity * Time.deltaTime; //ĳ���� �����ӵ��� �߷� ���� ����
        //Debug.Log(yVelocity.ToString());
        dir.y = yVelocity; //�߷°��� ������ �����ӵ� �Ҵ�
        //�̵� �ӵ��� ���� �̵�
        cc.Move(dir*moveSpeed*Time.deltaTime);
    }
    public void DamageAction(int damage)
    {
        hp -= damage; //���ʹ��� ���ݷ� ��ŭ �÷��̾� ü���� ����
    }
}
