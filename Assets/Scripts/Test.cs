using Ookii.FormatC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Test : MonoBehaviour
{
    public InputField inputField;
    public Text text;
    
    // Start is called before the first frame update
    public void UpdateText()
    {
        var formatter = new CodeFormatter
        {
            FormattingInfo = new CSharpFormattingInfo
            {
                Types = string.Empty.Split(' '),
            },
        };

        text.text = formatter.FormatCode(inputField.text);
    }

}
