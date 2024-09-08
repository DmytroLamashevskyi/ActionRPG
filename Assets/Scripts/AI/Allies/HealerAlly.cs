using Game.AI;
using UnityEngine;

public class HealerAlly : AiCharacter
{
    private void Start()
    {
        SetBehavior(new PatrolBehavior());
    }

    public override void ActOnTarget()
    {
        if(IsTargetInActionRange())
        {
            Debug.Log("Лекарь лечит союзника");
            HealTarget();
        }
        else
        {
            navAgent.destination = target.position;
            Debug.Log("Лекарь движется к союзнику");
        }
    }

    private void HealTarget()
    {
        // Логика лечения союзника
        Debug.Log("Союзник получает лечение");
    }
}