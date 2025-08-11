using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private Enemy enemyScript;
    private Player playerScript;
    public Image gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI collectedAnchor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    public void GameOver()
    {
        if (enemyScript.isGameOver)
        {
            StartCoroutine(DelayGameOverScreen(3));
            scoreText.gameObject.SetActive(false);
        }
    }

    IEnumerator DelayGameOverScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameOver.gameObject.SetActive(true);
        DisplayScore();
    }

    public void DisplayScore()
    {
        int current = playerScript.GetScore();

        collectedAnchor.text = "Collected Anchor: " + current;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
