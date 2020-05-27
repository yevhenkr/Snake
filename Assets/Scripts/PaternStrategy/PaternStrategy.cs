using UnityEngine;

namespace PaternStrategy
{
  interface IFruit
  {
    void SqueezeOut();
  }

  class AppleJuice : IFruit
  {
    public void SqueezeOut()
    {
      Debug.Log("Получен яблдочный сок");
    }
  }

  class PearJuice : IFruit
  {
    public void SqueezeOut()
    {
      Debug.Log("Получен грушевый сок");
    }
  }

  class Fruits
  {
    protected int weight; // кол-во пассажиров
    protected string sort; // модель автомобиля

    public Fruits(int weight, IFruit fruit)
    {
      this.weight = weight;
      Fruit = fruit;
    }
    public IFruit Fruit { private get; set; }
    public void GetJuice()
    {
      Fruit.SqueezeOut();
    }
  }

}

