using UnityEngine;

namespace DataLayer.StaticData.Skill
{
    [CreateAssetMenu(menuName = "Battle/Skill/TipSkill", fileName ="TipSkill")]
    public class TipSkill : Skill
    {
        [Range(0, 3)]
        public uint level;
    }
}