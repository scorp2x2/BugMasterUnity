using DataLayer;
using DataLayer.DynamicData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __LocationControls : MonoBehaviour
{
    public List<Neighbour> Neighbours;
    public bool isEnemy;

    public EnemyDataContainer EnemyData;

    void OnMouseUpAsButton()
    {
        FindObjectOfType<__MapControls>().SelectLocation(this);
    }

    public IEnumerator Move(__PlayerControls player, Neighbour neighbour, float speed)
    {
        Transform transformEndPos;
        int i = 0;
        player.SetWalk();
        while (true)
        {
            if (i < neighbour.path.Count)
            {
                transformEndPos = neighbour.path[i].path;
                player.SetRight(neighbour.path[i].isRight);
            }
            else
            {
                transformEndPos = neighbour.location.transform;
                player.SetRight(neighbour.isRight);
            }
            player.transform.position = Vector3.MoveTowards(player.transform.position, transformEndPos.position, Time.deltaTime * speed);

            Vector3 vectorDelta = player.transform.position - transformEndPos.position;
            float round = 0.05f;
            if (Mathf.Abs(vectorDelta.x) < round && Mathf.Abs(vectorDelta.y) < round && Mathf.Abs(vectorDelta.z) < round)
            {
                i++;
                if (i > neighbour.path.Count)
                    break;
            }
            yield return new WaitForFixedUpdate();
        }

        FindObjectOfType<__MapControls>().EndMove();
        Debug.Log("Прибыл в место");
    }

    public void GenerateEnemy()
    {
        if (Random.Range(0, 100) > 30)
        {
            EnemyData.gameObject.SetActive(true);
            isEnemy = true;
        }
        else
        {
            EnemyData.gameObject.SetActive(false);
            isEnemy = false;
        }
    }
}

[System.Serializable]
public class Neighbour
{
    public __LocationControls location;
    public List<PartPath> path;
    public bool isRight;
}

[System.Serializable]
public class PartPath
{
    public bool isRight;
    public Transform path;
}
