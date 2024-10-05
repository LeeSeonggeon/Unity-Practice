using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private float moveSpeed = 10f;
    
    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if(transform.position.y < minY){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { //isTrigger가 체크되어있으면 -> 충돌 감지만
        if(other.gameObject.tag == "Weapon"){
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if(hp <= 0){
                if(gameObject.tag == "Boss"){
                    GameManager.instance.SetGameOver();
                }
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
            
            }
            Destroy(other.gameObject);
        }
    }

    // private void  OnCollisionEnter2D(Collision2D other) {// isTrigger가 해제되어 있으면 -> 물리적인 충돌
    
    // }
}
