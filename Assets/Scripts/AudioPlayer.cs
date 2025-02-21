using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [Header("BG Music")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private float BGMusicDuration;

    [Header("Basic UI click SFX")]
    [SerializeField] private AudioClip clickSFX;

    [Header("Press Button SFX")]
    [SerializeField] private List<AudioClip> buttonSFXs;

    [Header("Challenge started SFX")]
    [SerializeField] private AudioClip challegeSFX;

    [Header("Game Over SFX")]
    [SerializeField] private AudioClip gameOverSFX;

    [Header("Round Completed SFX")]
    [SerializeField] private AudioClip roundCompletedSFX;

    [Header("Countdown SFX")]
    [SerializeField] private AudioClip countdownSFX;

    [Header("Countdown Completed SFX")]
    [SerializeField] private AudioClip countdownCompletedSFX;

    [Header("Showcase Click SFX")]
    [SerializeField] private AudioClip showcaseClickSFX;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    private bool donePlaying = true;
    private bool mainMenu;

    private void Awake() 
    {
        ManageSingelton();
    }

    private void Start() 
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") {mainMenu = true;} else {mainMenu = false;}
        if (PlayerPrefs.GetFloat("SFX") == 0 && PlayerPrefs.GetFloat("SFX") == 0)
        {
            PlayerPrefs.SetFloat("SFX", 0.3f);
            PlayerPrefs.SetFloat("Music", 0.3f);
        }
        SFXSource.volume = PlayerPrefs.GetFloat("SFX");
        musicSource.volume = PlayerPrefs.GetFloat("Music");
    }

    private void ManageSingelton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() 
    {
        if (donePlaying)
        {
           PlayMusic(); 
           StartCoroutine(ReplayMusicRoutine());
        }
    }

    public void ChangeSFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public float GetSFXVolume()
    {
        return SFXSource.volume;
    }

    public void PlayMusic()
    {
        musicSource.PlayOneShot(backgroundMusic);
    }

    public void PlayClickSFX()
    {
        PlayClip(clickSFX);
    }

    public void PlayButtonSFX()
    {
        PlayClip(buttonSFXs[Random.Range(0, buttonSFXs.Count)]);
    }

    public void PlayGameOverSFX()
    {
        PlayClip(gameOverSFX);
    }

    public void PlayChallengeSFX()
    {
        PlayClip(challegeSFX);
    }

    public void PlayRoundCompletedSFX()
    {
        PlayClip(roundCompletedSFX);
    }

    public void PlayCountdownSFX()
    {
        PlayClip(countdownSFX);
    }

    public void PlayShowcaseClickSFX()
    {
        PlayClip(showcaseClickSFX);
    }

    public void PlayCountdownCompletedSFX()
    {
        PlayClip(countdownCompletedSFX);
    }

    private void PlayClip(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private IEnumerator ReplayMusicRoutine()
    {
        donePlaying = false;
        yield return new WaitForSeconds(BGMusicDuration);
        donePlaying = true;
    }
}