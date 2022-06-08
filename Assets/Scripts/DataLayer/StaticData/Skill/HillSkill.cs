using UnityEngine;

namespace DataLayer.StaticData.Skill
{
    [CreateAssetMenu(menuName = "Battle/Skill/HillSkill", fileName ="HillSkill")]
    public class HillSkill : Skill
    {
        [Range(1, 4)]
        public uint value;
    }
}