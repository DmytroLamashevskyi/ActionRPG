using System;
using UnityEngine;

[Serializable]
public class VFXParticalMechanic
{
    public string id;
    public ParticleSystem VFX; // Префаб для эффекта 
    public Transform position; // Точка спавна VFX на мече (конец лезвия меча)

    // Универсальный метод для активации любого VFX
    public void ActivateVFX(string id)
    {
        if(id == this.id)
            VFX.Play();
    }

    // Метод для отключения активного VFX
    public void DeactivateVFX(string id)
    {
        if(id == this.id)
            VFX.Stop();
    }
}
