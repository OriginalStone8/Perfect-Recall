using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneVisualChange : MonoBehaviour
{
    public static SceneVisualChange Instance { get; private set;}

    [SerializeField] private Color lightColor;
    [SerializeField] private Color sceneDarkColor, sliderDarkColor, highScoreDarkColor;
    [SerializeField] private List<Image> imagesToChange;
    [SerializeField] private Image sliderBG, highScoreIconBG;

    private void Awake() 
    {
        Instance = this;
    }

    public void ChangeSceneColorTheme(string theme)
    {
        if (theme.ToLower().Equals("light"))
        {
            //change to light
            Camera.main.backgroundColor = lightColor;
            foreach (Image image in imagesToChange)
            {
                image.color = lightColor;
            }
            sliderBG.color = sliderDarkColor;
            highScoreIconBG.color = highScoreDarkColor;
        }
        else
        {
            //change to dark
            Camera.main.backgroundColor = sceneDarkColor;
            foreach (Image image in imagesToChange)
            {
                image.color = sceneDarkColor;
            }
            sliderBG.color = lightColor;
            highScoreIconBG.color = lightColor;
        }
    } 
}
