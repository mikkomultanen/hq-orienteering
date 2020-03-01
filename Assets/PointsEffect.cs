using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsEffect : MonoBehaviour
{
    public float duration = 1;
    public string text;

    private float fraction = 0;
    private TextMeshProUGUI tm;

    void Start()
    {
        tm = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        fraction = Mathf.Clamp01(fraction + Time.deltaTime / duration);
        tm.text = text;
        tm.alpha = Mathf.Clamp01(1f - fraction);
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(duration);
    }
}
