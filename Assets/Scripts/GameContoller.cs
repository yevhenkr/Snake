using UnityEngine;
using UnityEngine.UI;

public class GameContoller : MonoBehaviour
{
  public GameObject mainMenuPanel;
  public FoodController foodSpawner;
  public InfoPanel infoPanel;
  public Text gameOverText;
  public Timer timer;
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
    infoPanel.RestartCount();
    timer.RestartTimer();
    //инстанс змея
    //инстанс шары
  }

  public void EndGame()
  {
    mainMenuPanel.SetActive(true);
    timer.StopTimer();
    gameOverText.enabled = true;
    //удалить голову змеи
    //удалить хвост
    //удалить еду
  }

}
