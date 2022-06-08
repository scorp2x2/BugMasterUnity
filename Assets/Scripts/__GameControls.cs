using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;

public class __GameControls : MonoBehaviour
{
    // todo : ловить события

    public Animator animator;
    public SceneControls SceneControls;

    public event EventHandler GameOver = (sender, args) => { };
    public event EventHandler WinBattle = (sender, args) => { };

    private void Awake()
    {
        var playerController = FindObjectOfType<PlayerController>();
        playerController.PlayerDied += OnHPFalseToZero;
        var enemyController = FindObjectOfType<EnemyController>();
        enemyController.EnemyDied += OnEnemyDied;
    }

    public void OnEnemyDied(object sender, EventArgs e)
    {
        animator.Play("EndBattle");
        WinBattle(this, EventArgs.Empty);
        SceneControls.LoadScene(1);
    }

    public void OnHPFalseToZero(object sender, EventArgs e)
    {
        animator.Play("EndBattle");
        GameOver(this, EventArgs.Empty);
        SceneControls.LoadScene(0);
    }
}
