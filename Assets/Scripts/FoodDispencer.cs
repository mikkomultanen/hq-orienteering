using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispencer : MonoBehaviour
{
    public float interval = 5;
    public GameObject foodPrefab;
    public float force = 100;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            var food = Instantiate(foodPrefab, transform.position, Quaternion.identity);
            var rb = food.GetComponent<Rigidbody2D>();
            rb.AddForce(Random.insideUnitCircle * force);
            yield return new WaitForSeconds(interval);
        }
    }
}
