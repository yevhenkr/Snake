
using System.Collections.Generic;
using UnityEngine;

public class SnakeHade : MonoBehaviour
{
  public GameObject Snake;
  public GameContoller GameController;
  public FoodController FoodController;
  public InfoPanel InfoPanel;
  public Vector3 startPos;
  public string nameFoodClone;
  public string nameTileClone;
  public int smoothMove;
  public float angleTurn;
  public float maxXPos;
  public float maxYPos;

  private SnakeTail SnakeTails;

  void Start()
  {
    GameController = GameController.GetComponent<GameContoller>();
    FoodController = FoodController.GetComponent<FoodController>();
    SnakeTails = GetComponent<SnakeTail>();
  }

  private void FixedUpdate()
  {
    if (gameObject.activeInHierarchy)
    {
      Turn();
      Move();
      CheckGoingOutField();
    }
  }

  public void Turn()
  {
    if (Input.GetKey(KeyCode.D))
    {
      Snake.transform.rotation *= Quaternion.Euler(0f, 0f, -angleTurn * Time.deltaTime);
      return;
    }

    if (Input.GetKey(KeyCode.A))
    {
      Snake.transform.rotation *= Quaternion.Euler(0f, 0f, +angleTurn * Time.deltaTime);
    }
    if (Input.touchCount > 0)
    {
      Touch myTouch = Input.GetTouch(0);
      Vector3 mousePosition = Input.mousePosition;
      mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
      Debug.Log("mousePosition = " + myTouch.position.x);
      if (myTouch.position.x > 515)
      {
        Snake.transform.rotation *= Quaternion.Euler(0f, 0f, -angleTurn * Time.deltaTime);
        return;
      }
      else
      {
        Snake.transform.rotation *= Quaternion.Euler(0f, 0f, +angleTurn * Time.deltaTime);
      }
    }
  }

  public void Move()
  {
    transform.position += transform.up / smoothMove;
  }

  public void StartSnake()
  {
    gameObject.SetActive(true);
    gameObject.transform.position = startPos;
  }

  private void OnTriggerEnter2D(Collider2D myCollision)
  {
    if (myCollision.gameObject.name == nameFoodClone)
    {
      SnakeEatFood();
      FoodController.DestroitedFood(myCollision.gameObject);
    }
    if (myCollision.gameObject.name == nameTileClone)
    {
      DeathSnake("Змьека сьела хвост");
    }
  }
  public void DeathSnake(string text)
  {
    Debug.Log(text);
    gameObject.SetActive(false);

    GameController.EndGame();
    SnakeTails.RemoveAllTiles();
    FoodController.DestroitedLastFood();
  }
  public void SnakeEatFood()
  {
    FoodController.CreatFood();
    SnakeTails.AddCircle();
    InfoPanel.AddCount();
  }


  private void CheckGoingOutField()
  {
    if (maxXPos < transform.position.x || -maxXPos > transform.position.x)
    {
      DeathSnake("Выпокинули пределы поля");
    }
    if (maxYPos < transform.position.y || -maxYPos > transform.position.y)
    {
      DeathSnake("Выпокинули пределы поля");
    }
  }
}