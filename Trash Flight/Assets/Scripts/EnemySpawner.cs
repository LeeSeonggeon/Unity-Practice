using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject boss;

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f}; //몬스터 위치
    [SerializeField]
    private float spawnInterval = 1.5f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
       StartEnemyRoutine();
    }

    void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine"); //Coroutine을 통해 시작
    }

    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f); // 이 시간동안 다음 동작전에 대기
        
        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;                
        while(true){
            
            foreach(float PosX in arrPosX){
                SpawnEnemy(PosX, enemyIndex, moveSpeed);
            }
            spawnCount++;
            if (spawnCount % 10 ==0) {
                enemyIndex++;
                moveSpeed += 2;
            }

            if(enemyIndex >= enemies.Length){
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }
            yield return new WaitForSeconds(spawnInterval); //몬서터가 계속 나타나는 것을 막는다.
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        
        if(Random.Range(0,5) == 0){
            index += 1;
        }
        
        if(index >= enemies.Length) {
            index = enemies.Length - 1;
        }
        
        
        GameObject enemyobject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyobject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss(){
        Instantiate(boss, transform.position, Quaternion.identity);
    }

}
