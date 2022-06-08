using DataLayer.DynamicData;
using DataLayer.StaticData.Bug;
using UnityEngine;
using Spine.Unity;
using DataLayer.StaticData.Enemy;

namespace DataLayer
{
    public class EnemyDataContainer : MonoBehaviour
    {
        public EnemyData enemyData;
        //public BugTask currentTask;

        private void Start()
        {
            var dataBase = FindObjectOfType<GlobalDataContainer>();

            int rndEnemyTypeIdent = Random.Range(0, 2); // todo: change to 3 (4)
            BugType bugType = (BugType) rndEnemyTypeIdent;
            Debug.LogFormat("rnd: {0}, enum: {1}", rndEnemyTypeIdent, bugType);
            EnemyStaticData enemyStaticData = (dataBase.GetRandomEnemyStaticData(bugType));

            enemyData = new EnemyData(enemyStaticData);

            SkeletonGraphic skeletonGraphic = transform.GetChild(0).GetComponent<SkeletonGraphic>();

            if (skeletonGraphic.skeletonDataAsset != null)
            {
                skeletonGraphic.SkeletonDataAsset.Clear();
            }

            skeletonGraphic.skeletonDataAsset = enemyStaticData.SkeletonDataAsset;
            skeletonGraphic.Initialize(true);
        }
    }
}