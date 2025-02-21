using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite onSprite, offSprite;

    private bool isOn;

    private void Start() 
    {
        if (PlayerPrefs.GetInt("MusicOn") == 0)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
        SetTheMusic();
    }

    public void ToggleTheMusic()
    {
        GetComponent<Button>().interactable = false;
        GetComponent<Button>().interactable = true;

        isOn = !isOn;
        if (isOn)
        {
            PlayerPrefs.SetInt("MusicOn", 0);
            text.text = "ON";
            icon.sprite = onSprite;
            FindObjectOfType<AudioPlayer>().UnMuteMusic();
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 1);
            text.text = "OFF";
            icon.sprite = offSprite;
            FindObjectOfType<AudioPlayer>().MuteMusic();
        }
    }

    public void SetTheMusic()
    {
        GetComponent<Button>().interactable = false;
        GetComponent<Button>().interactable = true;

        if (isOn)
        {
            PlayerPrefs.SetInt("MusicOn", 0);
            text.text = "ON";
            icon.sprite = onSprite;
            FindObjectOfType<AudioPlayer>().UnMuteMusic();
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 1);
            text.text = "OFF";
            icon.sprite = offSprite;
            FindObjectOfType<AudioPlayer>().MuteMusic();
        }
    }

    private IEnumerator RenableButton()
    {
        GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.01f);
        GetComponent<Button>().interactable = true;
    }
}
