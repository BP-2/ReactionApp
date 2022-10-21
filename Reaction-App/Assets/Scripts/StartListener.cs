using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartListener : MonoBehaviour
{
    public int difficulty;

    public bool started = false;

    void OnTriggerEnter(Collider c)
    {
        if (
            c.gameObject.tag == "l_controller" ||
            c.gameObject.tag == "r_controller"
        ) started = true;
    }
}
