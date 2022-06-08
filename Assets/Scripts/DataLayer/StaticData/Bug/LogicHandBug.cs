using UnityEngine;

namespace DataLayer.StaticData.Bug
{
    [CreateAssetMenu(menuName = "Battle/LogicHandBug", fileName ="LogicHandBug")]
    public class LogicHandBug : BugTask
    {
        [TextArea(5, 20)]
        public string Ansver;

        private void OnValidate()
        {
            Type = BugType.LogicHand;
        }
    }
}