using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private AudioManager audioManager;
    public UnityEvent<float> popUpPauseUI;
    public UnityEvent<float> OnDead;
    public UnityEvent<float> OnWin;

    public int score = 0;
    public int kill = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        loader = GetComponent<LevelLoader>();  
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
    }
    // Start is called before the first frame update
    LevelLoader loader;
    void Start()
    {
        //loader = LevelLoader.Instance;
        audioManager = AudioManager.Instance;
        audioManager.PlaySound("Theme");
    }

    public void NextScene()
    {
        Debug.Log(loader);
        loader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next Scene");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        popUpPauseUI.Invoke(1f);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Dead()
    {
        OnDead.Invoke(1f);
    }

    public void Win()
    {
        OnWin.Invoke(1f);
    }

    public void DebugLog()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
