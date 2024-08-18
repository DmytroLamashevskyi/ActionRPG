using UnityEngine;
 
public class AnimatorController 
{
    private const string _speedLabel = "speed";
    private const string _airbornLabel = "airborn";


    private Animator _animator;
    public AnimatorController(Animator animator)
    {
        _animator = animator; 
    }

    public void SetFall(bool isFalling)
    {
        _animator.SetBool(_airbornLabel, isFalling);
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(_speedLabel, speed);
    }

}
