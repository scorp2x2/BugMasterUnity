using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.Events;

public class __BattleAnimController : MonoBehaviour
{
    public SkeletonGraphic SkeletonPlayer;
    public SkeletonGraphic SkeletonEnemy;

    public Animator animator;

    public UnityAction UnityAction;

    void Awake()
    {
        if (SkeletonEnemy.skeletonDataAsset != null)
        {
            SkeletonEnemy.SkeletonDataAsset.Clear();
        }

        SkeletonEnemy.skeletonDataAsset = PlayerMapState.EnemyData.SkeletonDataAsset;
        SkeletonEnemy.Initialize(true);
    }

    public void StartAnimAttackPlayer(UnityAction unityAction)
    {
        this.UnityAction = unityAction;
        animator.Play("AttackPlayer");
    }

    public void StartAnimAttackEnemy(UnityAction unityAction)
    {
        this.UnityAction = unityAction;
        animator.Play("AttackEnemy");
    }

    public void GetDamage()
    {
        var player = FindObjectOfType<Battle.PlayerController>();
        player.hpControls.UpdateDisplay((int)player.data.CurrentHp, (int)player.data.MaxHP);

        var enemy = FindObjectOfType<Battle.EnemyController>();
        enemy.hpControls.UpdateDisplay((int)enemy.data.CurrentHp, (int)enemy.data.MaxHP);
    }

    public void EndAnim()
    {
        if (UnityAction != null)
            UnityAction.Invoke();
    }

    public void AttackPlayer()
    {
        SkeletonPlayer.AnimationState.SetAnimation(0, "Attack", false);
        SkeletonPlayer.AnimationState.Complete += a =>
         {
             SkeletonPlayer.AnimationState.SetAnimation(0, "Idle", true);
         };
    }

    public void DamagePlayer()
    {
        SkeletonPlayer.AnimationState.SetAnimation(0, "Damage", false);
        SkeletonPlayer.AnimationState.Complete += a =>
        {
            SkeletonPlayer.AnimationState.SetAnimation(0, "Idle", true);
        };
    }

    public void AttackEnemy()
    {
        SkeletonEnemy.AnimationState.SetAnimation(0, "Attack", false);
        SkeletonEnemy.AnimationState.Complete += a =>
        {
            SkeletonEnemy.AnimationState.SetAnimation(0, "animation", true);
        };
    }

    public void DamageEnemy()
    {
        SkeletonEnemy.AnimationState.SetAnimation(0, "Damage", false);
        SkeletonEnemy.AnimationState.Complete += a =>
        {
            SkeletonEnemy.AnimationState.SetAnimation(0, "animation", true);
        };
    }
}
