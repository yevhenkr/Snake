using UnityEngine;

public class SnakeHade : MonoBehaviour
{

  public GameContoller GameController;
  public FoodController FoodController;
  public SnakeTail SnakeTails;
  public InfoPanel InfoPanel;
  public Vector3 startPos;
  public string nameFoodClone;
  public string nameTileClone;
  public int smoothMove;
  public float angleTurn;
  public float maxXPos;
  public float maxYPos;


  private void FixedUpdate()
  {
    if (gameObject.activeInHierarchy)
    {
      Turn();
      Move();
      CheckGoingOutField();
    }
  }

  public void StartSnake()
  {
    gameObject.SetActive(true);
    transform.position = startPos;
  }

  public void Turn()
  {
    if (Input.GetKey(KeyCode.D))
    {
      transform.rotation *= Quaternion.Euler(0f, 0f, -angleTurn * Time.deltaTime);
      return;
    }
    if (Input.GetKey(KeyCode.A))
    {
      transform.rotation *= Quaternion.Euler(0f, 0f, +angleTurn * Time.deltaTime);
      return;
    }

    TurnInAndroid();
  }
  public void TurnInAndroid()
  {
    if (Input.touchCount > 0)
    {
      Touch myTouch = Input.GetTouch(0);
      Vector3 mousePosition = Input.mousePosition;
      mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
      if (myTouch.position.x > 515)
      {
        transform.rotation *= Quaternion.Euler(0f, 0f, -angleTurn * Time.deltaTime);
        return;
      }
      else
      {
        transform.rotation *= Quaternion.Euler(0f, 0f, +angleTurn * Time.deltaTime);
      }
    }
  }

  public void Move()
  {
    transform.position += transform.up / smoothMove;
  }


  private void OnTriggerEnter2D(Collider2D myCollision)
  {
    if (myCollision.gameObject.name == nameFoodClone)
    {
      EatFoodSnake();
      FoodController.DestroitedFood(myCollision.gameObject);
    }
    if (myCollision.gameObject.name == nameTileClone)
    {
      DeathSnake("Змьека сьела хвост");
    }
  }
  public void EatFoodSnake()
  {
    FoodController.CreatFood();
    SnakeTails.AddCircle();
    InfoPanel.AddCount();
  }

  public void DeathSnake(string text)
  {
    gameObject.SetActive(false);
    GameController.EndGame();
    SnakeTails.RemoveAllTiles();
    FoodController.DestroitedLastFood();
  }

  private void CheckGoingOutField()
  {
    if (maxXPos < transform.position.x || -maxXPos > transform.position.x)
    {
      DeathSnake("Вы покинули пределы поля");
    }
    if (maxYPos < transform.position.y || -maxYPos > transform.position.y)
    {
      DeathSnake("Вы покинули пределы поля");
    }
  }
}