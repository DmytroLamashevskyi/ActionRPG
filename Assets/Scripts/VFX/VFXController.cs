using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField]
    public List<VFXParticalMechanic> effects;
    
    public void ActivateVFX(string effectId)
    {
        effects.ForEach(e=>e.ActivateVFX(effectId)); 
    }
     
    public void DeactivateVFX(string effectId)
    {
        effects.ForEach(e => e.DeactivateVFX(effectId));
    }
}
