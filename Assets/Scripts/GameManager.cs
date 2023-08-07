using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameManager Instance;
    private AudioManager audioManager;

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
    }
    // Start is called before the first frame update
    LevelLoader loader;
    void Start()
    {
        //loader = LevelLoader.Instance;
        audioManager = AudioManager.Instance;
        audioManager.PlaySound("Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        Debug.Log(loader);
        loader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next Scene");
    }
}
