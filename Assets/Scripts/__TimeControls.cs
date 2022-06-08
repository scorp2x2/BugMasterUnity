using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class __TimeControls : MonoBehaviour
{
    public UnityAction UnityAction;
    public Text Text;
    float timeStart;

    Coroutine Coroutine;

    public void StartTime(UnityAction unityAction, float time = 30)
    {
        Text.gameObject.SetActive(true);
        UnityAction = unityAction;
        timeStart = Time.time;

        if (Coroutine != null)
            StopCoroutine(Coroutine);

        Coroutine=StartCoroutine(TimeTick(time+timeStart));
    }

    IEnumerator TimeTick(float time)
    {
        while (true)
        {
            if (time - Time.time < 0)
                break;
            else if (time - Time.time < 10)
                Text.text = string.Format("00:0{0}", (int)(time - Time.time));
            else
                Text.text = string.Format("00:{0}", (int)(time - Time.time));

            yield return new WaitForSeconds(1f);
        }
        UnityAction.Invoke();
        Text.gameObject.SetActive(false);

    }
}
