using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class __PlayerControls : MonoBehaviour
{
    public SkeletonGraphic SkeletonGraphic;

    public void Start()
    {
        SetIdle();
    }

    public void SetIdle()
    {
        string nameAnim = "Idle";

        SkeletonGraphic.AnimationState.SetAnimation(0, nameAnim, true);

    }

    public void SetWalk()
    {
        string nameAnim = "Wolk";
        SkeletonGraphic.AnimationState.SetAnimation(0, nameAnim, true);
    }

    public void SetRight(bool isRight)
    {
        transform.rotation = new Quaternion(0, isRight ? 0 : 1, 0, 0);
    }
}
