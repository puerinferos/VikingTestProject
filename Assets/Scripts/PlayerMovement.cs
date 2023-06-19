using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
   [SerializeField]private float shiftSpeedMultiplier;
   [SerializeField]private Transform cameraFollowTarget;
   
   private Vector2 look;
   private Vector2 cameraRotation;

   private void Awake()
   {
      controller = GetComponent<CharacterController>();
      animatorController = GetComponent<UnitAnimatorController>();
   }

   void OnMove(InputValue input)
   {
      var moveInput = input.Get<Vector2>();
      movementInput = new Vector3(moveInput.x, 0, moveInput.y);
   }

   void OnLook(InputValue input)
   {
      look = input.Get<Vector2>();
   }
   public override void Move()
   {
      base.Move();
      controller.Move(targetDirection * (speed * Time.fixedDeltaTime));
   }

   public override void Rotate()
   {
      if (movementInput == Vector3.zero)
         return;
      base.Rotate(); 
      targetRotation *= Quaternion.Euler(cameraFollowTarget.eulerAngles.y * Vector3.up);
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10);
   }

   private void FixedUpdate()
   {
      if(PlayerCore.playerIsDead)
         return;
      Rotate();
      Move();
   }

   private void RotateCamera()
   {
      cameraRotation += new Vector2(-look.y, look.x);
      cameraRotation.x = Mathf.Clamp(cameraRotation.x, -30, 70);

      Quaternion rotation = Quaternion.Euler(cameraRotation);
      cameraFollowTarget.rotation = rotation;
   }

   private void LateUpdate()
   {
      if(PlayerCore.playerIsDead)
         return;
      RotateCamera();
   }
}
