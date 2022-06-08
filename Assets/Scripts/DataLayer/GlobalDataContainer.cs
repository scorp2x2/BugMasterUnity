using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.StaticData.Bug;
using DataLayer.StaticData.Enemy;
using DataLayer.StaticData.Skill;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace DataLayer
{
    public class GlobalDataContainer : MonoBehaviour
    {
        public List<SyntaxBag> syntaxBags;
        public List<LogicHandBug> logicHandBugs;
        public List<EnemyStaticData> enemyStaticDataList;

        private void Awake()
        {
            LoadBugsFromSource();
        }

        private void LoadBugsFromSource()
        {
            syntaxBags = Resources.LoadAll<SyntaxBag>("Bugs/Syntax").ToList();
            logicHandBugs = Resources.LoadAll<LogicHandBug>("Bugs/LogicHand").ToList();
            enemyStaticDataList = Resources.LoadAll<EnemyStaticData>("Enemy").ToList();
        }
        public IEnumerable<BugTask> GetBugsData(BugType type)
        {
            switch (type)
            {
                case BugType.Syntax:
                    return syntaxBags;
                case BugType.LogicHand:
                    return logicHandBugs;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
        public IEnumerable<BugTask> GetBugsData(BugType type, uint level)
        {
            return GetBugsData(type).Where(task => task.Level == level);
        }
        public IEnumerable<T> GetBugsData<T>() where T : BugTask
        {
            if (typeof(T) == typeof(SyntaxBag))
                return (IEnumerable<T>)syntaxBags;
            if (typeof(T) == typeof(LogicHandBug))
                return (IEnumerable<T>)logicHandBugs;
            throw new IndexOutOfRangeException("there is no such bug type!");
        }
        public IEnumerable<T> GetBugsData<T>(uint level) where T : BugTask
        {
            return GetBugsData<T>().Where(arg => arg.Level == level);
        }
        public BugTask GetRandomBug(BugType type)
        {
            switch (type)
            {
                case BugType.Syntax:
                    return syntaxBags.GetRandom();
                case BugType.LogicHand:
                    return logicHandBugs.GetRandom();
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
        public BugTask GetRandomBug(BugType type, uint level)
        {
            return GetBugsData(type, level).GetRandom();
        }
        public T GetRandomBug<T>() where T : BugTask
        {
            if (typeof(T) == typeof(SyntaxBag))
                return (T) (object)syntaxBags.GetRandom();
            if (typeof(T) == typeof(LogicHandBug))
                return (T) (object)logicHandBugs.GetRandom();
            throw new IndexOutOfRangeException("there is no such bug type!");
        }
        public T GetRandomBug<T>(uint level) where T : BugTask
        {
            return GetBugsData<T>(level).Where(arg => arg.Level == level).GetRandom();
        }
        public List<EnemyStaticData> GetEnemyStaticDataList()
        {
            return enemyStaticDataList;
        }
        public List<EnemyStaticData> GetEnemyStaticDataList(BugType type)
        {
            return enemyStaticDataList.Where(data => data.Type == type).ToList();
        }
        public EnemyStaticData GetRandomEnemyStaticData()
        {
            return enemyStaticDataList.GetRandom();
        }
        public EnemyStaticData GetRandomEnemyStaticData(BugType type)
        {
            return GetEnemyStaticDataList(type).GetRandom();
        }
    }
}