using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXController : MonoBehaviour
{
    public VisualEffect footSteps;

    public void BustFootStep()
    {
        footSteps.SendEvent("OnPlay");
    }
}
