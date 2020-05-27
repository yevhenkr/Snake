using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaternStrategy
{
  public class ButtonControlle : MonoBehaviour
  {
    public void GetAppleJuice()
    {
      Fruits fruit = new Fruits(4, new AppleJuice());
      fruit.GetJuice();
    }

    public void GetPearJuice()
    {
      Fruits fruit = new Fruits(4, new PearJuice());
      fruit.GetJuice();
    }
  }
}
