using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;


public class UpdateLeaderBoard : MonoBehaviour
{
    private TextMeshPro tm;
    public GameObject GameController;
    private EasyMode em;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        em = GameController.GetComponent<EasyMode>();
        gm = GameController.GetComponent<GameManager>();
        tm = gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.playerNumber == 1) {
            tm.text = "You: " + em.score + "\nPlayer 2: " + gm.otherPlayer.playerScore.ToString();
        }
        else if(gm.playerNumber == 2) {
            tm.text = "Player 1: " + gm.otherPlayer.playerScore.ToString() + "\nYou: " + em.score;
        }
        else {
            tm.text = "";
        }
    }
}
