using UnityEngine;

public class PearRotateBehaviour : IWalkBehaviour
{

  private Transform foodTransform;
  private float speed;
  private float rotate;
  public PearRotateBehaviour(Transform pearTransform, float speed)
  {
    this.foodTransform = pearTransform;
    this.speed = speed;
  }
  public void Move(Vector3 direction) => Debug.Log("This enemy can not walk! He can only kill!");

  public float SetMoveSpeed(float speed) => 0;

  public void Rotate(Vector3 direction) => this.rotate = rotate;
}
