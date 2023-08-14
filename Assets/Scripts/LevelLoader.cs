using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    //public static LevelLoader Instance;
    [SerializeField] private GameObject _loaderUI;
    private Slider _progressBar;
    private Text _progressText;
    // Start is called before the first frame update
    void Start()
    {
        _progressBar = _loaderUI.GetComponentInChildren<Slider>();
        _progressText = _loaderUI.GetComponentInChildren<Text>();
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        Instantiate(_loaderUI);

        do
        {
            await Task.Delay(100);
            _progressBar.value = scene.progress;
            _progressText.text = $"{scene.progress * 100}%";    
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
    }

    public async void LoadScene(int index)
    {
        var scene = SceneManager.LoadSceneAsync(index);
        scene.allowSceneActivation = false;

        Instantiate(_loaderUI);

        do
        {
            await Task.Delay(1000);
            _progressBar.value = scene.progress;
            _progressText.text = $"{scene.progress * 100}%";
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
