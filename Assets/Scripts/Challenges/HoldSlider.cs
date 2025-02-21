using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldSlider : MonoBehaviour
{
    [SerializeField] private Transform handle;
    [SerializeField] private Transform minPos, maxPos;
    [SerializeField] private int width;
    [SerializeField] private int HandleWidth;
    [SerializeField] private Transform safeArea;
    [SerializeField] private Transform minPosSafe, maxPosSafe;
    [SerializeField] private int safeAreaWidth;


    private float value;

    private float max = 1;
    private float min = 0;

    private bool move;
    private bool toMax;

    private void Update() 
    {
        if (move)
        {
            float step = HoldToConfirmButton.Instance.GetSpeed() * Time.deltaTime;

            if (toMax)
            {
                handle.position = Vector2.MoveTowards(handle.position, maxPos.position, step);
                if (Vector2.Distance(handle.position, maxPos.position) < Mathf.Epsilon)
                {
                    handle.position = maxPos.position;
                    toMax = false;
                }
            }
            else
            {
                handle.position = Vector2.MoveTowards(handle.position, minPos.position, step);
                if (Vector2.Distance(handle.position, minPos.position) < Mathf.Epsilon)
                {
                    handle.position = minPos.position;
                    toMax = true;
                }
            }
        }

        UpdateValue();
    }

    private void UpdateValue()
    {
        Vector2 handlePos = handle.GetComponent<RectTransform>().anchoredPosition;
        Vector2 maxPos = this.maxPos.GetComponent<RectTransform>().anchoredPosition;
        value = handlePos.x / maxPos.x;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }

    public float GetValue()
    {
        return this.value;
    }

    public void ToggleSliderMove(bool enable)
    {
        if (enable)
        {
            value = 0;
            handle.position = minPos.position;
            toMax = true;
        }
        move = enable;
    }

    public void PauseMove()
    {
        move = false;
        StartCoroutine(PauseRoutine());
    }

    public IEnumerator PauseRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        move = true;
    }

    public void GenerateSafeArea()
    {
        safeArea.position = new Vector2(Random.Range(minPosSafe.position.x, maxPosSafe.position.x), safeArea.position.y);
        Vector2 safePos = safeArea.GetComponent<RectTransform>().anchoredPosition;
        HoldToConfirmButton.Instance.SetSafeArea(safePos.x / width);
    }
}
