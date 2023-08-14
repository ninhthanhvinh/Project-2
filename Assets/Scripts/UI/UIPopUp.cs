using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIPopUp : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        switch (gameObject.tag)
        {
            case "PauseUI":
                gameManager.popUpPauseUI.AddListener(delegate { TweenIn(1f); });
                break;
            case "DeadUI":
                gameManager.OnDead.AddListener(delegate { TweenIn(1f); UpdateScore(1f); });
                break;
            case "WinUI":
                gameManager.OnWin.AddListener(delegate { TweenIn(1f); UpdateScore(1f); });
                break;
        }

        Debug.Log("Start" + gameObject.transform.localScale);
    }
    private void DelayTime()
    {
        Time.timeScale = 0f;
    }

    public void TweenIn(float time)
    {
        Cursor.lockState = CursorLockMode.None;
        LeanTween.scale(gameObject, new Vector3(2f, 2f, 1f), time).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        Invoke(nameof(DelayTime), time);
    }

    public void TweenOut(float time)
    {
        Time.timeScale = 1f;
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), time).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        Debug.Log("TweenOut" + gameObject.transform.localScale);
        Time.timeScale = 1f;
        Debug.Log(Time.timeScale);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateScore(float x)
    {
        var text = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = "Score: " + gameManager.score;
    }
}
