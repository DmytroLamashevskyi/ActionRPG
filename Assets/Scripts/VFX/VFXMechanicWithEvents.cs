using UnityEngine;
using UnityEngine.Events;

public class VFXMechanicWithEvents : MonoBehaviour
{
    public UnityEvent onActivateVFX; // Событие для активации VFX
    public UnityEvent onDeactivateVFX; // Событие для деактивации VFX

    public void ActivateVFX()
    {
        onActivateVFX.Invoke(); // Вызов события
    }

    public void DeactivateVFX()
    {
        onDeactivateVFX.Invoke(); // Вызов события
    }
}
