using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  public GameContoller gameContoller;
  public GameObject mainMenuPanel;
  void Start()
  {
    OpenMainmenu();
  }
  public void OpenMainmenu()
  {
    mainMenuPanel.SetActive(true);
  }
  public void ClouseMainmenu()
  {
    mainMenuPanel.SetActive(false);
  }

  public void StartGame()
  {
    gameContoller.StartGame();
    ClouseMainmenu();
  }


}
