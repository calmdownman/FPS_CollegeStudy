using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //발사 위치
    public GameObject bombFactory; //투척 무기 오브젝트
    public float throwPower = 15f;

    public GameObject bulletEffect; //피격 이펙트 오브젝트
    ParticleSystem ps; //피격 이펙트 파티클 시스템
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
            //수류탄을 생성한 후 수류탄 생성 위치를 firePosition으로 한다
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;
            
            //수류탄 오브젝트의 리지드바디 정보를 얻어옴
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //AddForce를 이용해 수류탄 이동
            rb.AddForce(Camera.main.transform.forward*throwPower, ForceMode.Impulse);
        }

        if(Input.GetMouseButtonDown(0)) 
        { 
            //레이를 생성한 후 발사될 위치와 진행 방향을 설정
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit(); //레이와 부딪힌 상대방의 정보를 저장할 구조체

            if(Physics.Raycast(ray, out hitInfo))
            {
                //레이가 부딪히 오브젝트가 Enemy라면
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                } 
                else
                {
                    //피격 이펙트의 위치를 레이와 부딪힌 지점으로 이동
                    bulletEffect.transform.position = hitInfo.point;
                    //피격 이펙트의 forward 방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다
                    bulletEffect.transform.forward = hitInfo.normal;
                    ps.Play(); //피격 이펙트 플레이
                }
            }
        }
    }
}
