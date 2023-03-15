using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect; //���� ����Ʈ ������ ����
    private void OnCollisionEnter(Collision collision)
    {
        //���� �������� ����
        GameObject eff = Instantiate(bombEffect);
        //���� �������� ����ź ������Ʈ�� ��ġ�� �����ϰ� �Ѵ�.
        eff.transform.position = transform.position;
        Destroy(gameObject);//�ڽ��� ����
    }
}
