using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
  public int countFood;
  public GameObject fff;
  public Text text;
  void Start()
  {

  }

  public void AddCount()
  {
    int i = int.Parse(text.text);
    i++;
    text.text = i.ToString();
  }
  public void RestartCount()
  {
    text.text = "0";
  }
}
