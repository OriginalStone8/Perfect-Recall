using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ButtonColorAssigner : MonoBehaviour
{
    public static ButtonColorAssigner Instance { get; private set; }

    [SerializeField] private List<Color> colors;
    [SerializeField] private List<Material> glowMats;
    public List<Color> presetColors = new List<Color>();

    [SerializeField] private float lightUpDuration;
    public static float LightUpDuration;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Sprite normalButtonPart, pressedButtonPart;

    private static List<Color> usedColors = new List<Color>();

    private bool speedUp;

    private void Awake() 
    {
        Instance = this;
        LightUpDuration = lightUpDuration;
        SetPresetColors();
    }

    public Color GetButtonColor()
    {
        if (colors.Count == 0) 
        {
            usedColors.Clear();
            for (int i = 0; i < presetColors.Count; i++)
            {
                colors.Add(presetColors[i]);
            }
        }
        int index = Random.Range(0, colors.Count);
        Color color = colors[index];
        colors.RemoveAt(index);
        usedColors.Add(color);
        return color;
    }

    public void SetGlowMaterial(SpriteRenderer renderer)
    {
        if (!presetColors.Contains(renderer.color)) return;
        int index = presetColors.IndexOf(renderer.color);
        renderer.material = glowMats[index];
    }

    public void SetNormalMaterial(SpriteRenderer renderer)
    {
        renderer.material = normalMaterial;
    }

    public void SetPressedSprite(SpriteRenderer renderer)
    {
        renderer.sprite = pressedButtonPart;
    }

    public void SetNormalSprite(SpriteRenderer renderer)
    {
        renderer.sprite = normalButtonPart;
    }

    private void SetPresetColors()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            presetColors.Add(colors[i]);
        }
    }

    public void SetSpeedUp(bool enable)
    {
        if (enable) LightUpDuration /= 2.5f;
        else LightUpDuration = lightUpDuration;
    }

    public void SetGlowMaterial(Image renderer)
    {
        int index = presetColors.IndexOf(renderer.color);
        renderer.material = glowMats[index];
    }

    public void SetNormalMaterial(Image renderer)
    {
        renderer.material = normalMaterial;
    }

    public void SetPressedSprite(Image renderer)
    {
        renderer.sprite = pressedButtonPart;
    }

    public void SetNormalSprite(Image renderer)
    {
        renderer.sprite = normalButtonPart;
    }
}
