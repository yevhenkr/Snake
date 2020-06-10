using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  [SerializeField] private Text TimerText;
  private float gameTime;
  private float minuts;
  private bool timerIsOn;
  private TimeSpan timeFormat;

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
    TimerText.text = timeFormat.Minutes + ":" + timeFormat.Seconds.ToString("00");
  }

  public void StopTimer()
  {
    timerIsOn = false;
  }
}
