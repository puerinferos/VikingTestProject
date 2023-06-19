using UnityEngine;

public class UnitAnimatorController : MonoBehaviour
{
    [SerializeField] private ParticleSystem attackParticles;
    private Animator animator;
    public Animator Animator => animator;
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Damage = Animator.StringToHash("Damage");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DieAnimation()
    {
        animator.SetFloat(Speed,0);
        animator.SetBool(Death,true);
    }

    public void OnGameStart()
    {
        animator.SetBool(Death,false);
    }

    public void DamageAnimation()
    {
        if(PlayerCore.playerIsDead)
            return;
        animator.SetTrigger(Damage);
    }

    public void ControlMovementAnimation(float speed)
    {
        if(PlayerCore.playerIsDead)
            return;
        animator.SetFloat(Speed,speed);
    }
}