using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataLayer.StaticData.Player;
using DataLayer.DynamicData;
using DataLayer.StaticData.Skill;

public class __MainControls : MonoBehaviour
{
    public void LoadScene(int index)
    {
        PlayerStaticData playerStatic  = Resources.LoadAll<PlayerStaticData>("")[0];
        var skills = Resources.LoadAll<Skill>("Skills").ToList();
        PlayerMapState.PlayerData = new PlayerData(playerStatic, skills);
        PlayerMapState.NumberLocationControls = 0;
        SceneManager.LoadScene(index);
    }
}
