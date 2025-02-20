using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHell : MonoBehaviour
{
    public static ButtonHell Instance { get; private set; }

    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private List<Transform> newSpots;
    [SerializeField] private float cameraZoomOutSize, cameraZoomInSize;

    private List<GameButton> newButtons = new List<GameButton>();

    private void Awake() 
    {
        Instance = this;
    }

    public IEnumerator SetCameraSize(bool zoomOut)
    {
        if (zoomOut)
        {
            while (Camera.main.orthographicSize < cameraZoomOutSize)
            {
                Camera.main.orthographicSize += 0.02f;
                yield return new WaitForSeconds(0.008f);
            }
            Camera.main.orthographicSize = cameraZoomOutSize;
        }
        else
        {
            while (Camera.main.orthographicSize > cameraZoomInSize)
            {
                Camera.main.orthographicSize -= 0.02f;
                yield return new WaitForSeconds(0.008f);
            }
            Camera.main.orthographicSize = cameraZoomInSize;
        }
    }

    public void AddButtons()
    {
        StartCoroutine(SetCameraSize(true));
        for (int i = 0; i < newSpots.Count; i++)
        {
            GameObject btn = Instantiate(btnPrefab, newSpots[i]);
            GameButton gameButton = btn.GetComponent<GameButton>();
            gameButton.SetToInvisible();
            gameButton.FadeIn(0.3f);
            newButtons.Add(gameButton);
        }
    }

    public void RemoveButtons()
    {
        for (int i = 0; i < newButtons.Count; i++)
        {
            GameButton btn = newButtons[i];
            btn.FadeOutDestroy(0.3f);
        }

        newButtons.Clear();
        StartCoroutine(SetCameraSize(false));
    }

    public List<GameButton> GetNewButtons()
    {
        return newButtons;
    }
}
