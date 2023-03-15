using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0; //회전값 변수(Y축만 회전)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse Y");
        //회전 값 변수에 마우스 입력 값만큼 미리 누적 시킨다.
        mx += mouse_X * rotSpeed * Time.deltaTime;
        //회전 방향으로 Y축 회전을 한다.
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
