using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EasyMode : GameManager
{
    public bool on = false;

    int selected;

    public Material normal;

    public Material lit;

    private ButtonClickListener bcl;

    public int score = 0;
    public float difficulty;

    void start()
    {
        selected = (int) Mathf.Round(Random.Range(0, numShapes));
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            buttons[selected].GetComponent<MeshRenderer>().material = lit;
            buttons[selected].GetComponent<ButtonClickListener>().isOn = true;
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
                difficultyTimer -= Time.deltaTime;
                if (
                    difficultyTimer <= 0 ||
                    buttons[selected].GetComponent<ButtonClickListener>().touched
                )
                {
                    if(buttons[selected].GetComponent<ButtonClickListener>().touched){
                            score += 10;
                        }
                    difficultyTimer = difficulty;
                    buttons[selected].GetComponent<ButtonClickListener>().touched = false;
                    buttons[selected].GetComponent<ButtonClickListener>().isOn = false;
                    buttons[selected].GetComponent<MeshRenderer>().material = normal;
                    selected = (int) Mathf.Round(Random.Range(0, numShapes));
                    buttons[selected].GetComponent<MeshRenderer>().material =lit;
                }
            }
            else
            {
                on = false;
                buttons[selected].GetComponent<ButtonClickListener>().touched = false;
                buttons[selected].GetComponent<ButtonClickListener>().isOn = false;
                buttons[selected].GetComponent<MeshRenderer>().material =normal;
            }
        }
    }
}
