using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class __ManaControls : MonoBehaviour
{
    public Text textCurrentMana;
    public Text textMaxMana;

    public void UpdateDisplay(int curent, int max)
    {
        textCurrentMana.text = curent.ToString();
        textMaxMana.text = max.ToString();
    }
}
