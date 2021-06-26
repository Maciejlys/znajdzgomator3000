using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform playerTransform;

   private float speed = 0.05f;
   private Vector3 offset = new Vector3(15, 25, 0);

   private void FixedUpdate()
   {
      Vector3 desiredPosition = playerTransform.position + offset;
      Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPosition, speed);
      transform.position = smoothPos;
      
      transform.LookAt(playerTransform);
   }
}
