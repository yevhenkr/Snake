using UnityEngine;
namespace GameFood
{
  public class Pear : Food
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
    [SerializeField] private Vector3 direction;
    private PlayerInfo playerInfo;

    private void Start()
    {
      playerInfo = new PlayerInfo(transform.position);
      SetWalkBehaviour(new PearRotateBehaviour(transform, .25f));
    }

    private void FixedUpdate()
    {
      Rotet();
    }
    private void Rotet()
    {
      // PerformWalkMove(direction);
      PerformRotateSetSpeed(direction);
      //Здесь мы будем двигать наше яблоко для примера
    }
  }
}
