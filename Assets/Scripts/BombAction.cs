using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect; //폭발 이펙트 프리팹 변수
    private void OnCollisionEnter(Collision collision)
    {
        //폭발 프리팹을 생성
        GameObject eff = Instantiate(bombEffect);
        //폭발 프리팹을 수류탄 오브젝트의 위치와 동일하게 한다.
        eff.transform.position = transform.position;
        Destroy(gameObject);//자신을 제거
    }
}
