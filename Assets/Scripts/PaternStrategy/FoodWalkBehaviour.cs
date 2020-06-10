using UnityEngine;

public class AppleWalkBehaviour : IWalkBehaviour
{
  private Transform foodTransform;
  private float speed;
  public AppleWalkBehaviour(Transform pearTransform, float speed)
  {
    this.foodTransform = pearTransform;
    this.speed = speed;
  }
  public float SetMoveSpeed(float speed) => this.speed = speed;

  public void Move(Vector3 direction)
  {
    foodTransform.position += direction * speed;
    Debug.LogFormat("Transform: {0}, Direction: {1}, Speed: {2}", foodTransform, direction, speed);
  }

  public void Rotate(Vector3 direction)
  {
    foodTransform.Rotate(new Vector3(direction.x, direction.y, direction.z) * Time.deltaTime);
    Debug.LogFormat("Transform: {0}, Direction: {1}", foodTransform, direction);

  }
}
