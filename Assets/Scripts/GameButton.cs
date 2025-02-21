using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer buttonPart;
    [SerializeField] private Image buttonPartUI;
    [SerializeField] private bool isUI;

    private Vector3 originalPos;

    private void Start() 
    {
        if (isUI)
        {
            buttonPartUI.color = ButtonColorAssigner.Instance.GetButtonColor();
            ButtonColorAssigner.Instance.SetNormalMaterial(buttonPartUI);
        }
        else
        {
            buttonPart.color = ButtonColorAssigner.Instance.GetButtonColor();
            ButtonColorAssigner.Instance.SetNormalMaterial(buttonPart);
        }

        originalPos = transform.position;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LightUp();
        }
    }

    public void LightUp()
    {
        StartCoroutine(LightUpRoutine());
    }

    public void Pressed()
    {
        StartCoroutine(PressedRoutine());
    }

    public void FadeOutDestroy(float time)
    {
        StartCoroutine(FadeOutDestroyRoutine(time));
    }

    public IEnumerator FadeOutDestroyRoutine(float time)
    {
        FadeOut(time);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void FadeInClick()
    {
        StartCoroutine(FadeInClickRoutine());
    }

    private IEnumerator FadeInClickRoutine()
    {
        FadeIn(0.1f);
        yield return new WaitForSeconds(0.1f);
        Pressed();
        LightUp();
        yield return new WaitForSeconds(0.1f);
        FadeOut(0.1f);
    }

    private IEnumerator PressedRoutine()
    {
        ButtonColorAssigner.Instance.SetPressedSprite(buttonPart);
        yield return new WaitForSeconds(ButtonColorAssigner.LightUpDuration);
        ButtonColorAssigner.Instance.SetNormalSprite(buttonPart);
    }

    private IEnumerator LightUpRoutine()
    {
        ButtonColorAssigner.Instance.SetGlowMaterial(buttonPart);
        yield return new WaitForSeconds(ButtonColorAssigner.LightUpDuration);
        ButtonColorAssigner.Instance.SetNormalMaterial(buttonPart);
    }

    public void ResetToOriginalPos()
    {
        transform.position = originalPos;
    }

    public void SetToInvisible()
    {
        Color current = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0);
        current = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0);
    }

    public void FadeIn(float time)
    {
        StartCoroutine(FadeRoutine(time, false));
    }

    public void FadeOut(float time)
    {
        StartCoroutine(FadeRoutine(time, true));
    }

    private IEnumerator FadeRoutine(float time, bool off)
    {
        if (off)
        {
            for (int i = 0; i < time/4; i++)
            {
                Color currentColor = GetComponent<SpriteRenderer>().color;
                GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - 0.25f);
                
                currentColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - 0.25f);
                
                yield return new WaitForSeconds(time/4);
            }
            Color current = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0);
            current = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0);
        }
        else
        {
            for (int i = 0; i < time/4; i++)
            {
                Color currentColor = GetComponent<SpriteRenderer>().color;
                GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 0.25f);
                
                currentColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 0.25f);
                
                yield return new WaitForSeconds(time/4);
            }
            Color current = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1);
            current = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1);
        }
    }

    public void PressedUI()
    {
        StartCoroutine(PressedRoutineUI());
        StartCoroutine(LightUpRoutineUI());
    }

    public void HighlightUI()
    {
        ButtonColorAssigner.Instance.SetPressedSprite(buttonPartUI);
    }

    public void ReleaseHighlightUI()
    {
        ButtonColorAssigner.Instance.SetNormalSprite(buttonPartUI);
        ButtonColorAssigner.Instance.SetNormalMaterial(buttonPartUI);
    }

    private IEnumerator PressedRoutineUI()
    {
        ButtonColorAssigner.Instance.SetPressedSprite(buttonPartUI);
        yield return new WaitForSeconds(ButtonColorAssigner.LightUpDuration);
        ButtonColorAssigner.Instance.SetNormalSprite(buttonPartUI);
    }

    private IEnumerator LightUpRoutineUI()
    {
        ButtonColorAssigner.Instance.SetGlowMaterial(buttonPartUI);
        yield return new WaitForSeconds(ButtonColorAssigner.LightUpDuration);
        ButtonColorAssigner.Instance.SetNormalMaterial(buttonPartUI);
    }

    public bool GetIsUI()
    {
        return isUI;
    }
}
