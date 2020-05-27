using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWalkBehaviour
{
  float SetMoveSpeed(float speed);
  // float SerRoveSpeed(float speed);
  void Move(Vector3 direction);
  void Rotate(Vector3 direction);
}


