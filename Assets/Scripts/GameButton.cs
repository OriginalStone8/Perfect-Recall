using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer buttonPart;

    private Vector3 originalPos;

    private void Start() 
    {
        buttonPart.color = ButtonColorAssigner.Instance.GetButtonColor();
        ButtonColorAssigner.Instance.SetNormalMaterial(buttonPart);

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

    public void HoldClick()
    {
        ButtonColorAssigner.Instance.SetPressedSprite(buttonPart);
        ButtonColorAssigner.Instance.SetGlowMaterial(buttonPart);
    }

    public void ReleaseClick()
    {
        ButtonColorAssigner.Instance.SetNormalSprite(buttonPart);
        ButtonColorAssigner.Instance.SetNormalMaterial(buttonPart);
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
}
