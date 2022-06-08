using UnityEngine;
using System.Collections.Generic;

namespace DataLayer.StaticData.Player
{
    [CreateAssetMenu(menuName = "Battle/PlayerStaticData", fileName ="PlayerStaticData")]
    public class PlayerStaticData : ScriptableObject
    {
        public uint MaxHP;
        public uint MaxMana;
        public List<int> levelRequierenments;
        public uint MaxLevel;
    }
}