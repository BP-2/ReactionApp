using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplayScript : MonoBehaviour
{
    private TextMeshPro tm;

    public GameObject GameController;

    private EasyMode em;

    // Start is called before the first frame update
    void Start()
    {
        em = GameController.GetComponent<EasyMode>();
        tm = gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        tm.text = "Timer: " + Mathf.Round(em.gameTimer * 10)/10f;
    }
}
