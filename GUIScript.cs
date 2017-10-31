using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour {

    public GameObject ready;
    public GameObject go;
    public Text scoreText;
    public Text coinsText;
    public CharacterControllerScript ccs;
    
    public Image displayedHearts;
    public Sprite[] hearts;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private float playerStart;
    private bool paused;

    void Start () {
        paused = false;
        StartCoroutine(readyIn(1));
        StartCoroutine(startMoving(3));
    }

    public void setPlayerStart(float x)
    {
        playerStart = x;
    }

    public void setCountText(int numCoins, float playerX)
    {
        coinsText.text = "Coins: " + numCoins.ToString();
        scoreText.text = "Distance: " + ((playerX - playerStart)).ToString("F1");
    }

    public void changeHeartCount(int heartNum)
    {
        displayedHearts.sprite = hearts[heartNum];
    }

    public void setPauseMenu(bool set)
    {
        pauseMenu.SetActive(set);
    }

    public void pause()
    {
        if (paused)
        {
            paused = false;
            setPauseMenu(paused);
            Time.timeScale = 1;
        }
        else
        {
            paused = true;
            setPauseMenu(paused);
            Time.timeScale = 0;
        }

    }

    public void endGame()
    {
        Application.Quit();
    }

    public void playerDeath()
    {
        gameOverMenu.SetActive(true);
        //Allign the scores to the middle
        scoreText.rectTransform.anchorMin = new Vector2(.5f, .5f);
        scoreText.rectTransform.anchorMax = new Vector2(.5f, .5f);
        scoreText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        scoreText.alignment = TextAnchor.MiddleCenter;
        scoreText.rectTransform.anchoredPosition = new Vector2(0, 50);
        coinsText.rectTransform.anchorMin = new Vector2(.5f, .5f);
        coinsText.rectTransform.anchorMax = new Vector2(.5f, .5f);
        coinsText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        coinsText.alignment = TextAnchor.MiddleCenter;
        coinsText.rectTransform.anchoredPosition = new Vector2(0, 20);
    }

    public void loadScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    IEnumerator readyIn(float x)
    {
        yield return new WaitForSeconds(x);
        ready.SetActive(true);
    }

    IEnumerator startMoving(float x)
    {
        yield return new WaitForSeconds(x);
        ccs.startPlayer();
        ready.SetActive(false);
        go.SetActive(true);
        StartCoroutine(goOut(0.5f));
    }

    IEnumerator goOut(float x)
    {
        yield return new WaitForSeconds(x);
        go.SetActive(false);
    }
}
