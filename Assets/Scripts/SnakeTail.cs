using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
  public Transform SnakeHead;//Cсылка на голову змейки
  public float CircleDiameter;
  private List<Transform> snakeCircles = new List<Transform>();//Спискок трансформов в котрых будут храниться наши кружочки
  private List<Vector3> positions = new List<Vector3>();//Позици кружочков но не втекущий момент времени

  private void Awake()
  {
    positions.Add(SnakeHead.position);
  }

  private void Update()
  {
    float distance = ((Vector3)SnakeHead.position - positions[0]).magnitude;//Расстояыние между текущем положением головы и последним котрое сохранено
    if (distance > CircleDiameter)//отошла ли голова на растояние своего диаметра
    {
      // Направление от старого положения головы, к новому
      Vector3 direction;
      direction = ((Vector3)SnakeHead.position - positions[0]).normalized;

      positions.Insert((0), (positions[0] + direction * CircleDiameter));
      positions.RemoveAt(positions.Count - 1);

      distance -= CircleDiameter;
    }

    for (int i = 0; i < snakeCircles.Count; i++)//пройтись по всем
    {
      snakeCircles[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / CircleDiameter);
    }
  }

  public void AddCircle()
  {
    Transform circle = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
    if (snakeCircles.Count == 0)//Костыль отключаю тригер у первогопоявившегося
    {
      circle.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    snakeCircles.Add(circle);
    positions.Add(circle.position);
  }

  public void RemoveAllTiles()
  {
    float j = snakeCircles.Count;
    if (snakeCircles.Count > 0)
    {
      for (int i = 0; i < j; i++)
      {
        Destroy(snakeCircles[0].gameObject);
        snakeCircles.RemoveAt(0);
      }
    }
    // positions.RemoveAt(1);
  }
}
