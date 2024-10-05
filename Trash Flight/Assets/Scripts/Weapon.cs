using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    public float damage = 1f;
    void Start()    // 객체가 만들어지고 한번 실행되는 함수
    {
        Destroy(gameObject, 1);    //삭제 명령어 (삭제할 객체, 몇 초 후 삭제할 것인가)
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;



    }
}
