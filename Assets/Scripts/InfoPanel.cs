using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
  [Header("Count")]
  public Text CountText;

  public void AddCount()
  {
    int i = int.Parse(CountText.text);
    i++;
    CountText.text = i.ToString();
  }

  public void RestartCount()
  {
    CountText.text = "0";
  }
}
