using UnityEngine;

namespace DataLayer.StaticData.Skill
{
    public class Skill : ScriptableObject
    {
        public string Name;
        public string Info;
        [Range(1, 6)]
        public uint Cost;
    }
}