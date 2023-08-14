using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleScoreboard : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public Text killText;

    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameManager.score.ToString();
        killText.text = gameManager.kill.ToString();
    }
}
