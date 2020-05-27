using UnityEngine;

namespace GameFood
{
  public class Apple : Food
  {
    [System.Serializable]
    public struct PlayerInfo
    {
      public Vector3 position;

      public PlayerInfo(Vector3 position)
      {
        this.position = position;
      }
    }

    [SerializeField]
    private Vector3 direction;
    private PlayerInfo playerInfo;

    private void Start()
    {
      playerInfo = new PlayerInfo(transform.position);
      SetWalkBehaviour(new FoodWalkBehaviour(transform, .25f));
    }
    private void Update()
    {
      if (Input.GetKey(KeyCode.Space)) Movement();
    }

    private void Movement()
    {
      PerformWalkMove(direction);
    }
  }
}