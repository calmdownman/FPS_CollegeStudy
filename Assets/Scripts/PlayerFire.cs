using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //�߻� ��ġ
    public GameObject bombFactory; //��ô ���� ������Ʈ
    public float throwPower = 15f;

    public GameObject bulletEffect; //�ǰ� ����Ʈ ������Ʈ
    ParticleSystem ps; //�ǰ� ����Ʈ ��ƼŬ �ý���
    public int weaponPower = 5;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1)) 
        {
            //����ź�� ������ �� ����ź ���� ��ġ�� firePosition���� �Ѵ�
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;
            
            //����ź ������Ʈ�� ������ٵ� ������ ����
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //AddForce�� �̿��� ����ź �̵�
            rb.AddForce(Camera.main.transform.forward*throwPower, ForceMode.Impulse);
        }

        if(Input.GetMouseButtonDown(0)) 
        { 
            //���̸� ������ �� �߻�� ��ġ�� ���� ������ ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit(); //���̿� �ε��� ������ ������ ������ ����ü

            if(Physics.Raycast(ray, out hitInfo))
            {
                //���̰� �ε��� ������Ʈ�� Enemy���
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                } 
                else
                {
                    //�ǰ� ����Ʈ�� ��ġ�� ���̿� �ε��� �������� �̵�
                    bulletEffect.transform.position = hitInfo.point;
                    //�ǰ� ����Ʈ�� forward ������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��
                    bulletEffect.transform.forward = hitInfo.normal;
                    ps.Play(); //�ǰ� ����Ʈ �÷���
                }
            }
        }
    }
}
