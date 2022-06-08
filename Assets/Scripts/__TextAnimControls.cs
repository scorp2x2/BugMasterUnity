using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class __TextAnimControls : MonoBehaviour
{
    public Text text;
    public string startText;
    List<string> texts;
    int index = 0;

    void Start()
    {
        texts = new List<string>();
        texts=startText.Split(new char[] {'\n' }).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        FillText();
    }

    void FillText()
    {
        for (int i = index; i < texts.Count; i++)
        {
            text.text += texts[i];
        }

        for (int i = 0; i < index; i++)
        {
            text.text += texts[i];
        }
    }
}
