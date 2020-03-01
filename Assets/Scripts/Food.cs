using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int points = 100;
    public AudioClip eatSfx;
    public float extraSeconds = 0;
    public GameObject onEatenEffect = null;
    public PointsEffect pointsEffect = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null) {
            if (pointsEffect != null) {
                var pe = Instantiate(pointsEffect, transform.position, Quaternion.identity);
                pe.text = "" + points;
            }
            if (onEatenEffect != null)
            {
                Instantiate(onEatenEffect, transform.position, Quaternion.identity);
            }
            ScoreSingleton.Instance.AddTime(extraSeconds);
            ScoreSingleton.Instance.Add(points);
            Destroy(gameObject);
        }
    }
}
