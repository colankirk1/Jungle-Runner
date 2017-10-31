using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour {

    public GameObject howToPlayObject;

    public void howToPlay(bool x)
    {
        howToPlayObject.SetActive(x);
    }

	public void loadScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void endGame()
    {
        Application.Quit();
    }
}
