using System;
using System.Collections.Generic;
using DataLayer.StaticData.Bug;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class PlayerMoveController : MonoBehaviour
    {
        public PlayerController playerController;
        public EnemyController enemyController;

        public __MessageBox MessageBox;

        void Start()
        {
            FindObjectOfType<__TimeControls>().StartTime(new UnityEngine.Events.UnityAction(OnButtonMoveClick));
        }

        private bool ValidateCode()
        {
            var bug = FindObjectOfType<BugTaskContainer>().currentTask;
            switch (bug.Type)
            {
                case BugType.Syntax:
                    return ValidateOnSyntax(true);
                    break;
                case BugType.LogicHand:
                    return ValidateOnSyntax(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            throw new ArgumentOutOfRangeException();
        }

        private bool ValidateOnSyntax(bool isSyntax)
        {
            var bug = FindObjectOfType<BugTaskContainer>().currentTask;
            
            string origin = string.Empty;
            if (isSyntax)
                origin = (bug as SyntaxBag).Ansver;
            else
                origin = (bug as LogicHandBug).Ansver;

            string validated = FindObjectOfType<TMPro.TMP_InputField>().text;

            var separator = new char[] { '\r', '\n','\t', ' ' };

            var replaced_origin = origin
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);
            

            var replaced_validated = validated
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);


                string[] tokens_origin = replaced_origin.Split(separator,StringSplitOptions.RemoveEmptyEntries);
                string[] tokens_validated = replaced_validated.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            
               // if (tokens_origin.Length != tokens_validated.Length)
                //    return false;
            
                for(int i = 0; i < tokens_origin.Length; i++)
                    if (tokens_origin[i] != tokens_validated[i])
                        return false;

            return true; // todo: Придумать нормальный алгоритм
        }

        public void OnButtonMoveClick()
        {
            if (ValidateCode())
            {
                enemyController.GetDamage(1);
                playerController.AddMana(1);
                playerController.AddPoints(10);
            }
            else
            {
                playerController.DoDamage(1);
            }
        }

        public void ButtonUseSkin(int skillNumber)
        {
            var bug = FindObjectOfType<BugTaskContainer>().currentTask;
            var writeField = FindObjectOfType<TMPro.TMP_InputField>();
            string tipString;
            if (skillNumber == 0)
            {
                playerController.UseHill(MessageBox);
                return;
            }
            playerController.UseTip((uint)skillNumber, MessageBox, bug);
        }


    }
}