using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class UpdateWallText : MonoBehaviour
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
        tm.text ="Score: " + em.score;
    }
}
