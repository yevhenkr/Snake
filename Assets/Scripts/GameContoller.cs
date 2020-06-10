using UnityEngine;
using UnityEngine.UI;

public class GameContoller : MonoBehaviour
{
  public GameObject MainMenuPanel;
  public FoodController FoodSpawner;
  public InfoPanel InfoPanel;
  public Text GameOverText;
  public Timer Timer;
  public SnakeHade Snake;

  private void Start()
  {
    MainMenuPanel.SetActive(true);
  }

  public void StartGame()
  {
    FoodSpawner.CreatFood();
    Snake.StartSnake();
    InfoPanel.RestartCount();
    Timer.RestartTimer();
  }

  public void EndGame()
  {
    MainMenuPanel.SetActive(true);
    Timer.StopTimer();
    GameOverText.enabled = true;
  }
}
