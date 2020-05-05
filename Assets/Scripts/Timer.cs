using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  public Text timerTimer;
  private float gameTime;
  private float minuts;
  private bool timerIsOn;
  TimeSpan timeFormat;

  void Start()
  {
  }
  private void FixedUpdate()
  {
    if (timerIsOn)
    {
      StartTimer();
    }
    else
    {
      ;
    }
  }

  public void RestartTimer()
  {
    gameTime = 0;
    timerIsOn = true;
  }

  private void StartTimer()
  {
    gameTime += Time.deltaTime;
    timeFormat = TimeSpan.FromSeconds(gameTime);
    timerTimer.text = timeFormat.Minutes + ":" + timeFormat.Seconds.ToString("00");
  }

  public void StopTimer()
  {
    timerIsOn = false;
  }
}
