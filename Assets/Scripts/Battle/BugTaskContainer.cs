using System;
using DataLayer;
using DataLayer.StaticData.Bug;
using Ookii.FormatC;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Battle
{
    public class BugTaskContainer : MonoBehaviour
    {
        public TMPro.TMP_InputField TMP_InputField;
        public TMPro.TextMeshProUGUI TextMeshProUGUI;
        
        private GlobalDataContainer _globalGlobalContainer;
        private uint _level;
        private BugType _type;

        [ReadOnlyField]
        public BugTask currentTask;

        private void Awake()
        {
            _level= PlayerMapState.EnemyData.Level;
            _globalGlobalContainer = FindObjectOfType<GlobalDataContainer>();
            _type = PlayerMapState.EnemyData.Type;
            currentTask = _globalGlobalContainer.GetRandomBug(_type); // todo: need level
            UpdateText();
        }

        public void UpdateText()
        {
            
            string code = currentTask.Code;
            TMP_InputField.text = code
                .Replace("\r", String.Empty)
                .Replace("\t", "    ");
            SyntaxHighlighting();
        }

        public void OnInput(string input)
        {
            int c_pos = TMP_InputField.caretPosition;
            TMP_InputField.text = input
                .Replace("\r", String.Empty)
                .Replace("\t", "    ");
            TMP_InputField.caretPosition = c_pos + 3;
        }

        public void LoadNewTask() // on user win
        {
            currentTask = _globalGlobalContainer.GetRandomBug(_type); // todo: need level
            UpdateText();
            FindObjectOfType<__TimeControls>().StartTime(new UnityEngine.Events.UnityAction(FindObjectOfType<PlayerMoveController>().OnButtonMoveClick));

        }

        public void SyntaxHighlighting()
        {
            var formatter = new CodeFormatter
                    {
                        FormattingInfo = new CSharpFormattingInfo
                        {
                            Types = string.Empty.Split(' '),
                        },
                    };
            TextMeshProUGUI.text= formatter.FormatCode(TMP_InputField.text);
        }
    }
}