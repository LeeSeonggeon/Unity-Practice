using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //private, public 상관없이 유니티에서 조작 가능하게 
        
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal"); // 키보드 방향키 왼쪽 오른쪽 값을 -1 또는 1로 받음
        // float verticalInput = Input.GetAxisRaw("Vertical"); // 키보드 방향키 상하 값 받기
        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f); //어디로 이동할지 정해주는 값
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // if (Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // }
        // else if(Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveTo;
        // }

        //Debug.Log(Input.mousePosition); // Debug.Log: consol에 log 남기기, Input.mousePosition: 마우스 좌표 찍기
                                        // 이걸로 찍히는 것은 Game view의 왼쪽 아래가 0,0으로 시작하지만 유니티상 좌표와 다름

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 좌표를 유니티상의 좌표와 맞춰줌
        //Debug.Log(mousePos);


        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f); //값의 최대 최소를 지정해 넘어가면 최대 최소값을 주도록 한다.
        transform.position = new Vector3(toX, transform.position.y, transform.position.z); 
                            //yz값을 기존 값을 유지하게 한다.
                            //계속 업데이트하기 때문에 충돌벽이 소용 없음
                            //플레이를 통해 최대값 최소값을 확인하고 코딩 필요
        if(GameManager.instance.isGameOver == false){
            Shoot();
        }

    }
    void Shoot() {

        if(Time.time - lastShotTime > shootInterval){ //Time.time: 게임이 시작된 이후로 현재까지 흐른 시간
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity); //게임 오브젝트 만들기(만들 오브젝트, 위치, 회전 값)
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            //Debug.Log("Game Over");
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag =="Coin"){
            Debug.Log("Coin +1");
            Destroy(other.gameObject);
            GameManager.instance.IncreaseCoin();
        }

    }

    public void Upgrade(){
        weaponIndex += 1;
        if(weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1;
        }
    }

}
