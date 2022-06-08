using System;
using DataLayer.StaticData.Bug;
using UnityEngine;
using Spine.Unity;

namespace DataLayer.StaticData.Enemy
{
    [CreateAssetMenu(menuName = "Battle/EnemyData", fileName ="EnemyData")]
    public class EnemyStaticData : ScriptableObject
    {
        public BugType Type;
        public uint MaxHP;
        public SkeletonDataAsset SkeletonDataAsset;
        public uint MaxLevel;
        public uint MinLevel;
    }
}