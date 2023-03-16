using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState m_State; //���ʹ� ���º���

    public float findDistance = 8f; //�÷��̾� �߰� ����
    Transform player; //�÷��̾� Ʈ������
    public float attackDistance = 2f;
    public float moveSpeed = 5f;
    CharacterController cc; //ĳ���� ��Ʈ�ѷ� ������Ʈ

    float currentTime = 0; //���� ���� �ð�
    float attackDelay = 2f; //���� ������ �ð�
    public int attackPower = 3;

    Vector3 originPos; //���ʹ��� �ʱ���ġ
    public float moveDistance = 20f; //�̵� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        m_State= EnemyState.Idle;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Move: Move(); break;
            case EnemyState.Attack: Attack(); break;
            case EnemyState.Return: Return(); break;
           // case EnemyState.Damaged: Damaged(); break;
           // case EnemyState.Die: Die(); break;
        }
    }

    void Idle() //��� ���� �Լ�, �÷��̾ 8���� ������ �������� �˻�
    {
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State= EnemyState.Move;
            print("���� ��ȯ: Idle->Move");
        }
    }
    void Move()
    {
         if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("���� ��ȯ:Move -> Return");
        }
        //�÷��̾ ���ݹ��� ���̶�� �÷��̾ ���� �̵�
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else //�׷��� �ʴٸ� ���� ���¸� �������� ��ȯ
        {
            m_State= EnemyState.Attack;
            print("���� ��ȯ:Move->Attack");
            currentTime= attackDelay; //�����ð��� �̸� 2�ʷ� �����Ͽ� ���� �� �ٷ� ����
        }
    }

    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                //�÷��̾��� ������ �Լ� ����
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����");
                currentTime = 0;
            }
        }
        else
        {
            m_State = EnemyState.Move;
            print("������ȯ: Attack -> Move");
            currentTime= 0;
        }
    }

    void Return()
    {
        //�ʱ���ġ���� �Ÿ��� 0.1f �̻��̶�� �ʱ� ��ġ ������ ����
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos- transform.position).normalized;
            cc.Move(dir*moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position= originPos;
            m_State= EnemyState.Idle;
            print("���� ��ȯ: Return->Idle");
        }
    }
}