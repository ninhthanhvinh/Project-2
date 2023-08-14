using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinUIHandle : MonoBehaviour
{
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.Instance;
        Debug.Log(gameManager);
        gameManager.OnWin.AddListener(delegate { TweenIn(1f); });
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
}
