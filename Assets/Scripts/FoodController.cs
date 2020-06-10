using UnityEngine;

public class FoodController : MonoBehaviour
{
  public GameObject PrefabFood;
  [SerializeField] private float maxPosX;
  [SerializeField] private float minPosX;
  [SerializeField] private float maxPosY;
  [SerializeField] private float minPosY;
  [SerializeField] private float posZ;

  private Vector3 position;
  private GameObject go;
  public void CreatFood()
  {
    ChoosePlaeseSpawn();
    Instantiate();
  }

  private void ChoosePlaeseSpawn()
  {
    position = new Vector3(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY), posZ);
  }

  private void Instantiate()
  {
    go = Instantiate(PrefabFood, position, Quaternion.identity);
  }

  public void DestroitedFood(GameObject food)
  {
    Destroy(food);
  }

  public void DestroitedLastFood()
  {
    Destroy(go);
  }

}
