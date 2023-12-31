using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDie : MonoBehaviour
{
    EnemyAI enemyAI;
    GameObject gameOverPanel;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PanelOff());
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            //플레이어 사망 메서드 호출
            enemyAI.PlayerDie();
            //플레이어 사망시 더 이상 움직이지 못 하게
            collision.collider.GetComponent<PlayerMove>().isPlayerDie = true;
            gameOverPanel.SetActive(true);
            var image = gameOverPanel.GetComponent<Image>();
            image.color = new Color(1, 1, 1, 1);

        }
    }

    public void PlayerDieEvent()
    {
        //플레이어 사망 메서드 호출
        enemyAI.PlayerDie();
        gameOverPanel.SetActive(true);
        var image = gameOverPanel.GetComponent<Image>();
        image.color = new Color(1, 1, 1, 1);
    }
    IEnumerator PanelOff()
    {
        enemyAI = GetComponent<EnemyAI>();
        gameOverPanel = GameObject.Find("GameOverImage");
        //yield return new WaitForSeconds(0.01f);
        yield return null;
        gameOverPanel.SetActive(false);
    }

}
