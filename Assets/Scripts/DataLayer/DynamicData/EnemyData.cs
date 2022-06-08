using System;
using DataLayer.StaticData.Bug;
using DataLayer.StaticData.Enemy;
using UnityEngine;
using Spine.Unity;

namespace DataLayer.DynamicData
{

    [Serializable]
    public class EnemyData : GameData
    {
        [SerializeField]
        private EnemyStaticData _staticData;
        [SerializeField]
        private uint _level;
        [SerializeField]
        private uint currentHP;

        public EnemyData(EnemyStaticData staticData)
        {
        //    if (level > MaxLevel || level < MinLevel)
        //        throw new ArgumentOutOfRangeException("Level should be less then max level");

            _staticData = staticData;
            _level = (uint)UnityEngine.Random.Range(staticData.MinLevel, staticData.MaxLevel + 1);
            currentHP = _staticData.MaxHP;
        }

        public uint Level
        {
            get { return _level; }
        }

        public BugType Type
        {
            get { return _staticData.Type; }
        }
        public uint MaxHP
        {
            get { return _staticData.MaxHP; }
        }

        public uint CurrentHp
        {
            get { return currentHP; }
            set { currentHP = value; }
        }

        public SkeletonDataAsset SkeletonDataAsset
        {
            get { return _staticData.SkeletonDataAsset; }
        }

        public uint MaxLevel { get { return _staticData.MaxLevel; } }
        public uint MinLevel { get { return _staticData.MinLevel; } }
    }
}