using UnityEngine;

public class GameContoller : MonoBehaviour
{
  public GameObject mainMenuPanel;
  public FoodController foodSpawner;
  public GameObject snake;

  void Start()
  {
    mainMenuPanel.SetActive(true);
  }
  public void ActivateSnake()
  {
    snake.SetActive(true);
  }
  public void StartGame()
  {
    //ActivateSnake();
    foodSpawner.CreatFood();
    snake.GetComponent<SnakeHade>().StartSnake();
    //старт таймер
    //СТАРТ КОУНТ
    //инстанс змея
    //инстанс шары
  }

  public void EndGame()
  {
    mainMenuPanel.SetActive(true);
    //удалить лиголову змеи
    //удалить хвост
    //удалить еду
  }

}
