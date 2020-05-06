using UnityEngine;
using UnityEngine.UI;

  public class InfoPanel : MonoBehaviour
  {
    [Header("Count")]
    public Text text;

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
