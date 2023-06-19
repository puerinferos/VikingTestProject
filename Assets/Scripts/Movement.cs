using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(UnitAnimatorController))]
public abstract class Movement : MonoBehaviour
{
    [SerializeField]protected float speed;
    
    protected CharacterController controller;
    protected UnitAnimatorController animatorController;

    public Vector3 movementInput;
    protected Quaternion targetRotation;
    protected Vector3 targetDirection;

    public float CurrentSpeed { get; private set; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animatorController = GetComponent<UnitAnimatorController>();
    }

    public virtual void Move()
    {
        if(PlayerCore.playerIsDead)
            return;
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, movementInput.magnitude, Time.fixedDeltaTime * 5);

        targetDirection = (transform.forward) * CurrentSpeed;

        animatorController.ControlMovementAnimation(CurrentSpeed);
        if (!controller.isGrounded)
            targetDirection -= Vector3.up * (10);   
    }

    public virtual void Rotate()
    {
        if(PlayerCore.playerIsDead)
            return;
        if (movementInput == Vector3.zero)
            return;
        targetRotation = Quaternion.LookRotation(movementInput);
    }
}