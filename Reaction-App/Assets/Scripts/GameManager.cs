using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameController;

    private EasyMode easyMode;

    public GameObject[] buttons;

    public GameObject startButton;

    StartListener sl;

    public GameObject startButton2;

    StartListener sl2;

    public GameObject startButton3;

    StartListener sl3;

    public int numShapes;

    public float gameTimer = 20;

    public float difficultyTimer = 0;

    public bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        numShapes = buttons.Length;
        easyMode = GameController.GetComponent<EasyMode>();
        sl = startButton.GetComponent<StartListener>();
        sl2 = startButton2.GetComponent<StartListener>();
        sl3 = startButton3.GetComponent<StartListener>();
    }

    void Update()
    {
        if (sl.started == true)
        {
            startGame(sl.difficulty);
            sl.started = false;
        }
        else if (sl2.started == true)
        {
            startGame(sl2.difficulty);
            sl2.started = false;
        }
        else if (sl3.started == true)
        {
            startGame(sl3.difficulty);
            sl3.started = false;
        }
    }

    public void startGame(int difficulty)
    {
        easyMode = GameController.GetComponent<EasyMode>();
        easyMode.gameTimer = 30;
        easyMode.on = true;
        easyMode.score = 0;
        switch (difficulty)
        {
            case (1):
                easyMode.difficulty = 4;
                break;
            case (2):
                easyMode.difficulty = 2.5f;
                break;
            case (3):
                easyMode.difficulty = 1f;
                break;
            default:
                easyMode.difficulty = 4;
                break;
        }
    }
}
