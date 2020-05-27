using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : MonoBehaviour
{
  // Start is called before the first frame update
  protected Sprite image;
  private IWalkBehaviour walkBehaviour;


  public void SetWalkBehaviour(IWalkBehaviour behaviour) => walkBehaviour = behaviour;//метод SetWalkBehaviour имеете возможность добавить новое поведение,
                                                                                      //а конкретно поведение, реализуемое на базе интерфейса "WalkBehaviour"
  public void PerformWalkSetSpeed(float speed) => walkBehaviour.SetMoveSpeed(speed);
  public void PerformWalkMove(Vector3 direction) => walkBehaviour.Move(direction);

  public void PerformRotateSetSpeed(Vector3 rotate) => walkBehaviour.Rotate(rotate);

}
