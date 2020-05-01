
using System.Collections.Generic;
using UnityEngine;

public class SnakeHade : MonoBehaviour
{
  public GameObject snake;
  public GameContoller gameController;
  public FoodController foodController;
  public InfoPanel infoPanel;
  public Vector3 startPos;
  public string nameFoodClone;
  public string nameTileClone;
  public int smoothMove;
  public float angleTurn;
  public float maxXPos;
  public float maxYPos;

  private SnakeTail snakeTails;

  void Start()
  {
    gameController = gameController.GetComponent<GameContoller>();
    foodController = foodController.GetComponent<FoodController>();
    snakeTails = GetComponent<SnakeTail>();
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
      snake.transform.rotation *= Quaternion.Euler(0f, 0f, -angleTurn * Time.deltaTime);
      return;
    }

    if (Input.GetKey(KeyCode.A))
    {
      snake.transform.rotation *= Quaternion.Euler(0f, 0f, +angleTurn * Time.deltaTime);
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
      foodController.DestroitedFood(myCollision.gameObject);
    }
    if (myCollision.gameObject.tag == nameTileClone)
    {
      DeathSnake("Змьека сьела хвост");
    }
  }
  public void DeathSnake(string text)
  {
    Debug.Log(text);
    gameObject.SetActive(false);

    gameController.EndGame();
    snakeTails.RemoveAllTiles();
    foodController.DestroitedLastFood();
  }
  public void SnakeEatFood()
  {
    foodController.CreatFood();
    snakeTails.AddCircle();
    infoPanel.AddCount();
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