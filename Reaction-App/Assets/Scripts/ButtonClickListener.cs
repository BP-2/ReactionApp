using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickListener : MonoBehaviour
{
    public bool touched;

    public bool isOn = false;

    void OnTriggerEnter(Collider c)
    {
        if (isOn)
        {
            touched = true;
            isOn = false;
        }
    }
}
