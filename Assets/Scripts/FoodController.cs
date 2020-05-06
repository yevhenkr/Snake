using UnityEngine;

public class FoodController : MonoBehaviour
{
  public GameObject PrefabFood;
  public float maxPosX;
  public float minPosX;
  public float maxPosY;
  public float minPosY;
  public float posZ;

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
    // StartCoroutine(Destroy(prefabFood));
    Destroy(food);
  }
  public void DestroitedLastFood()
  {
    Destroy(go);
  }
}
