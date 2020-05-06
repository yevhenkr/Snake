using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  public GameContoller GameContoller;
  public GameObject MainMenuPanel;
  void Start()
  {
    OpenMainmenu();
  }
  public void OpenMainmenu()
  {
    MainMenuPanel.SetActive(true);
  }
  public void ClouseMainmenu()
  {
    MainMenuPanel.SetActive(false);
  }

  public void StartGame()
  {
    GameContoller.StartGame();
    ClouseMainmenu();
  }


}
