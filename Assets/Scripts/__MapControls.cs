using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class __MapControls : MonoBehaviour
{
    private List<__LocationControls> LocationControls;
    public float speed = 1f;

    [SerializeField]
    private __LocationControls location;
    public bool isMove;
    public Transform panelLocation;

    public __PlayerControls player;

    public Animator animator;

    void Start()
    {
        LocationControls = new List<__LocationControls>();
        for (int i = 0; i < panelLocation.childCount; i++)
            LocationControls.Add(panelLocation.GetChild(i).GetComponent<__LocationControls>());

        location = LocationControls[PlayerMapState.NumberLocationControls];
        player.transform.position = location.transform.position;
        transform.position = new Vector3(-location.transform.position.x, -location.transform.position.y,0);

        foreach (var item in LocationControls)
        {
            if (item != location)
                item.GenerateEnemy();
        }
    }

    public void StartBattle()
    {
        for (int i = 0; i < panelLocation.childCount; i++)
        {
            if (panelLocation.GetChild(i) == location.transform)
            {
                PlayerMapState.NumberLocationControls = i;
                break;
            }
        }
        PlayerMapState.EnemyData = location.EnemyData.enemyData;
        animator.Play("EndMap");
    }

    public void SelectLocation(__LocationControls location)
    {
        var Neighbours = this.location.Neighbours.Where(a => a.location == location).ToList();
        if (Neighbours.Count == 0)
        {
            Debug.Log("Эти локации не соединены!");
            return;
        }
        if (isMove)
        {
            Debug.Log("Игрок в движение");
            return;
        }
        isMove = true;
        this.location.StartCoroutine(this.location.Move(player, Neighbours[0], speed));
        this.location = location;
        // player.position = this.location.transform.position;

    }

    public void EndMove()
    {
        isMove = false;
        player.SetIdle();
        if (location.isEnemy)
            StartBattle();
    }
}
