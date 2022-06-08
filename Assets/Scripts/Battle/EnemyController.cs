using System;
using DataLayer.DynamicData;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class EnemyController : MonoBehaviour
    {
        [Utils.ReadOnlyField]
        [SerializeField]
        public EnemyData data;

        public event EventHandler EnemyDied = (sender, args) => { };
        public __HpControls hpControls;

        private void Awake()
        {
            data = PlayerMapState.EnemyData;
            hpControls.UpdateDisplay((int)data.CurrentHp, (int)data.MaxHP);
        }

        public void GetDamage(uint value)
        {
            if (((int)data.CurrentHp - value) <= 0)
            {
                data.CurrentHp = 0;
                //hpControls.UpdateDisplay((int)data.CurrentHp, (int)data.MaxHP);

                FindObjectOfType<__BattleAnimController>().StartAnimAttackPlayer(new UnityEngine.Events.UnityAction(Died));

                return;
            }

            data.CurrentHp -= value;
            //hpControls.UpdateDisplay((int)data.CurrentHp,(int)data.MaxHP);

            var taskContainer = FindObjectOfType<BugTaskContainer>();
            FindObjectOfType<__BattleAnimController>().StartAnimAttackPlayer(new UnityEngine.Events.UnityAction(taskContainer.LoadNewTask));

        }

        void Died()
        {
            EnemyDied(data, EventArgs.Empty);

        }
    }
}