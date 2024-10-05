using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//싱글턴 적용 하나만 있어야 하니까
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;
    public static GameManager instance = null;

    [HideInInspector] //유니티에서는 안보이게
    public bool isGameOver = false;

    private void Awake() { //생성자인가? 일단 start보다 먼저 불린다.
        if(instance == null) {
            instance = this;
        }
    }
    
    public void IncreaseCoin() {
        coin += 1;
        text.SetText(coin.ToString());

        if(coin % 30 == 0){
            Player player = FindObjectOfType<Player>();
            if(player != null){
                player.Upgrade();
            }
        } 
    }

    public void SetGameOver() {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();
            isGameOver = true;
        }
        Invoke("ShowGameOverPanel", 1f);// 두번째 변수 시간 이후에 함수 사용
    }

    void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene");
    }
}
