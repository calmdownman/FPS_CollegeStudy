using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //발사 위치
    public GameObject bombFactory; //투척 무기 오브젝트
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
            //수류탄을 생성한 후 수류탄 생성 위치를 firePosition으로 한다
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;
            
            //수류탄 오브젝트의 리지드바디 정보를 얻어옴
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //AddForce를 이용해 수류탄 이동
            rb.AddForce(Camera.main.transform.forward*throwPower, ForceMode.Impulse);
        }
    }
}
