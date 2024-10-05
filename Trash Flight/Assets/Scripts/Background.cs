using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    private float moveSpeed = 3f;
    // Update is called once per frame
    void Update() //frame마다 계속 실행됨 -> 컴터 성능에 따라 실행 횟수가 달라짐 -> 컴퓨터에 따라 결과가 달라질 수 있음 -> 방지해줘야 함 -> Time.deltaTime을 곱해준다.
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime; //Vector3: 구조체 down말고도 left right 등도 존재

        if(transform.position.y < -10){
            transform.position += new Vector3(0, 20f, 0); //새로운 Vector3선언 더해줄 xyz값을 매개변수로 줌
        }

    }
}
