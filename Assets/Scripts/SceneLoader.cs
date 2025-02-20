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
        StartCoroutine(LoadSceneRoutine(SceneManager.GetActiveScene().name, 0.3f));
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadSceneRoutine("GameOverScene", 0.3f));
    }

    public void LoadGame()
    {
        ScoreManager.Instance.ResetScore();
        StartCoroutine(LoadSceneRoutine("GameScene", 0.3f));
    }

    public void LoadMenu()
    {
        ScoreManager.Instance.ResetScore();
        StartCoroutine(LoadSceneRoutine("MainMenu", 0.3f));
    }

    private IEnumerator LoadSceneRoutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
