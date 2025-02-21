using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set;}

    private void Awake() 
    {
        Instance = this;
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadSceneRoutine(SceneManager.GetActiveScene().name, 1f));
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadSceneRoutine("GameOverScene", 1f));
    }

    public void LoadGame()
    {
        FindObjectOfType<AudioPlayer>().PlayButtonSFX();
        ScoreManager.Instance.ResetScore();
        StartCoroutine(LoadSceneRoutine("GameScene", 1f));
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioPlayer>().PlayButtonSFX();
        ScoreManager.Instance.ResetScore();
        StartCoroutine(LoadSceneRoutine("MainMenu", 1f));
    }

    private IEnumerator LoadSceneRoutine(string sceneName, float delay)
    {
        TransitionLoader.Instance.StartTransition();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
