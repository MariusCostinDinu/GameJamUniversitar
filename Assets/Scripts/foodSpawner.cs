using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class foodSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] foodPrefab;
    [SerializeField] float spawnSpeed = 3f;
    [SerializeField] float positionX;
    [SerializeField] float positionY;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        StartCoroutine(FoodSpawn());
    }

    IEnumerator FoodSpawn()
    {
        while (true)
        {
            var position = new Vector3(positionX, positionY);
            GameObject gameObject = Instantiate(foodPrefab[Random.Range(0, foodPrefab.Length)],
                position, Quaternion.identity);
            
            StartCoroutine(MoveLeft(gameObject));
            
            yield return new WaitForSeconds(spawnSpeed);

            Destroy(gameObject, 10f);
        }
    }

    private IEnumerator MoveLeft(GameObject food)
    {
        while (food != null)
        {
            food.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
