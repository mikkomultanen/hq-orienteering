using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public Vector2 size;
    public float interval = 5;
    public GameObject foodPrefab;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true) {
            var halfX = size.x / 2;
            var halfY = size.y / 2;
            var position = new Vector2(Random.Range(-halfX, halfX), Random.Range(-halfY, halfY));
            Debug.Log("Spawn at timestamp : " + Time.time + " " + position);
            Instantiate(foodPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
}
