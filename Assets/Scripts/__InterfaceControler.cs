using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class __InterfaceControler : MonoBehaviour
{
    public Text textScore;
    public Text textLevel;
    public Image xp;


    void Awake()
    {
        textLevel.text = PlayerMapState.PlayerData.Level.ToString();
        textScore.text = PlayerMapState.PlayerData.GamePoints.ToString();
        xp.fillAmount = (float)PlayerMapState.PlayerData.CurrentXp / (float)PlayerMapState.PlayerData.LevelXp;
    }

}
