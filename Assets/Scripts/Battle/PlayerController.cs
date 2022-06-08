using System;
using DataLayer.DynamicData;
using DataLayer.StaticData.Bug;
using DataLayer.StaticData.Skill;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerData data;
        public event EventHandler PlayerDied = (sender, args) => { };

        public __HpControls hpControls;
        public __ManaControls manaControls;

        public void AddXP(uint value)
        {
            data.AddXP(value);
        }

        private void Awake()
        {
            data = PlayerMapState.PlayerData;
            hpControls.UpdateDisplay((int)data.CurrentHp, (int)data.MaxHP);
            manaControls.UpdateDisplay((int)data.CurrentMp, (int)data.MaxMana);
            FindObjectOfType<__GameControls>().WinBattle += (o, e) => AddXP(12);
        }

        public bool UseHill(__MessageBox messageBox)
        {
            var hill = ((HillSkill)data.Skills
                .Find(skill => skill.GetType() == typeof(HillSkill)));
            uint cost = hill.Cost;

            Debug.LogFormat("mana: {0}, cost: {1}", data.CurrentMp, cost);


            if (((int)data.CurrentMp - cost) < 0)
            {
                messageBox.Show("У вас не достаточно поинтов для использования", null, __MessageBox.TypeButton.Ok);
                return false;
            }
            messageBox.Show(hill.Info+  "\nХотите его использовать?",
                () => {
                    DoHill(hill.value);
                    data.CurrentMp -= cost;
                    hpControls.UpdateDisplay((int)data.CurrentHp, (int)data.MaxHP);
                    manaControls.UpdateDisplay((int)data.CurrentMp, (int)data.MaxMana);
                },
                __MessageBox.TypeButton.YesNo);

            return true;
        }

        public void AddPoints(uint value)
        {
            data.GamePoints += value;
        }

        public void AddMana(uint value)
        {
            if(data.CurrentMp + value >= data.MaxMana)
            {
                data.CurrentMp = data.MaxMana;
                manaControls.UpdateDisplay((int)data.CurrentMp, (int)data.MaxMana);
                return;
            }
            data.CurrentMp += value;
            manaControls.UpdateDisplay((int)data.CurrentMp, (int)data.MaxMana);
        }

        public bool UseTip(uint number, __MessageBox messageBox, BugTask bugTask)
        {
            var skill = (TipSkill) (ScriptableObject) data.Skills[(int) number];
            Debug.LogFormat("MP: {0}, цена: {1}", data.CurrentMp, skill.Cost);
            if ((int)data.CurrentMp - skill.Cost < 0)
            {
                messageBox.Show("У вас не достаточно поинтов для использования", null, __MessageBox.TypeButton.Ok);
                return false;
            }

            messageBox.Show(skill.Info + "\nХотите его использовать?",
               () => {
                   data.CurrentMp -= skill.Cost;
                   manaControls.UpdateDisplay((int)data.CurrentMp, (int)data.MaxMana);

                   messageBox.Show(bugTask.helpNotes[skill.level - 1], null,
                       __MessageBox.TypeButton.Ok);
               },
               __MessageBox.TypeButton.YesNo);

            return true;
        }

        public void DoDamage(uint value)
        {
            if (((int)data.CurrentHp - value) <= 0)
            {
                data.CurrentHp = 0;
                //hpControls.UpdateDisplay((int)data.CurrentHp, (int)data.MaxHP);

                FindObjectOfType<__BattleAnimController>().StartAnimAttackEnemy(Death);

                return;
            }
            data.CurrentHp -= value;

            //hpControls.UpdateDisplay((int)data.CurrentHp,(int)data.MaxHP);
            FindObjectOfType<__TimeControls>().StartTime(new UnityEngine.Events.UnityAction(FindObjectOfType<PlayerMoveController>().OnButtonMoveClick));
            FindObjectOfType<__BattleAnimController>().StartAnimAttackEnemy(null);

        }

        public void DoHill(uint value)
        {
            if (data.CurrentHp + value >= data.MaxHP)
                data.CurrentHp = data.MaxHP;
            else
                data.CurrentHp += value;
            hpControls.UpdateDisplay((int)data.CurrentHp,(int)data.MaxHP);
        }

        void Death()
        {
            PlayerDied(data, EventArgs.Empty);

        }

        internal string GetSkillInfo(int skillNumber)
        {
            return data.Skills[skillNumber].Info;
        }
        // todo: handle skills
    }
}