using System;
using System.Collections.Generic;
using DataLayer.StaticData.Player;
using DataLayer.StaticData.Skill;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataLayer.DynamicData
{
    [Serializable]
    public class PlayerData : GameData
    {
        [SerializeField]
        private PlayerStaticData _staticData;
        [SerializeField]
        private uint currentHP;
        [SerializeField] 
        private uint currentMP;
        [SerializeField] 
        private uint level;
        [SerializeField] 
        private uint currentXP;
        [SerializeField] 
        private uint gamePoints;
        [SerializeField]
        private List<Skill> _skills;

        public PlayerData(PlayerStaticData staticData, List<Skill> skills)
        {
            _staticData = staticData;
            level = 1;
            currentHP = _staticData.MaxHP;
            currentMP = _staticData.MaxMana; // todo: set to 0 at release
            currentXP = 0;
            gamePoints = 0;
            _skills = skills;
        }

        public uint MaxHP
        {
            get {return _staticData.MaxHP;}
        }
        
        public uint MaxMana
        {
            get { return _staticData.MaxMana; }
        }

        public uint CurrentHp
        {
            get { return currentHP; }
            set { currentHP = value; }
        }
        
        public uint GamePoints
        {
            get { return gamePoints; }
            set { gamePoints = value; }
        }

        public uint CurrentMp
        {
            get { return currentMP; }
            set { currentMP = value; }
        }

        public uint Level
        {
            get { return level; }
        }

        public uint CurrentXp
        {
            get { return currentXP; }
        }

        public int LevelXp
        {
            get { return _staticData.levelRequierenments[(int)level - 1]; }
        }

        public void AddXP(uint value)
        {
            if (level == _staticData.MaxLevel)
                return;

            if (currentXP + value >= _staticData.levelRequierenments[(int)level-1])
            {
                currentXP += value;
                currentXP -= (uint)_staticData.levelRequierenments[(int)level-1];
                level += 1;

            }
            else
                currentXP += value;
        }

        public uint MaxLevel {
            get { return _staticData.MaxLevel; }
        }
        
        public List<Skill> Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }
    }
}