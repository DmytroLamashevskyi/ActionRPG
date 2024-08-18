using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVfxController : MonoBehaviour
{
    public VisualEffect footSteps;

    public void UpdateFootSteps(bool state)
    {
        if(state)
        {
            footSteps.Play();
        }
        else
        {
            footSteps.Stop();
        }
    }
}
