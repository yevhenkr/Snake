// float SerRoveSpeed(float speed);
using UnityEngine;

namespace GameFood
{
  public class Apple : Food
  {
    

    [SerializeField] private Vector3 direction;
    private PlayerInfo playerInfo;

    private void Start()
    {
      playerInfo = new PlayerInfo(transform.position);
      SetWalkBehaviour(new AppleWalkBehaviour(transform, .25f));
    }
    private void Update()
    {
      if (Input.GetKey(KeyCode.Space)) Movement();
    }
    [System.Serializable]
    public struct PlayerInfo
    {
      public Vector3 position;

      public PlayerInfo(Vector3 position)
      {
        this.position = position;
      }
    }
    private void Movement()
    {
      PerformWalkMove(direction);
    }
  }
}