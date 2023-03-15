using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //�߻� ��ġ
    public GameObject bombFactory; //��ô ���� ������Ʈ
    public float throwPower = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            //����ź�� ������ �� ����ź ���� ��ġ�� firePosition���� �Ѵ�
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;
            
            //����ź ������Ʈ�� ������ٵ� ������ ����
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //AddForce�� �̿��� ����ź �̵�
            rb.AddForce(Camera.main.transform.forward*throwPower, ForceMode.Impulse);
        }
    }
}
