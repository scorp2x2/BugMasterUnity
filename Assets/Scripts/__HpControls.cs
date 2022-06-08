using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __HpControls : MonoBehaviour
{
    public GameObject[] hpArray=new GameObject[4];

    int index;

    public void UpdateDisplay(int count, int maxCount)
    {
        for (int i = 0; i < hpArray.Length; i++)
        {
            if (i < count)
            {
                hpArray[i].SetActive(true);
                hpArray[i].transform.GetChild(0).gameObject.SetActive(true);

            }
            else if (i < maxCount)
            {
                hpArray[i].SetActive(true);
                hpArray[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else
                hpArray[i].SetActive(false);
        }
        index = count - 1;
    }
}
