using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindFade : MonoBehaviour
{
    public SpriteRenderer wind;
    float timer = 0, alpha;
    public float speed = 2;

    private void Start()
    {
        transform.Translate(new Vector3(0, 0, -5), Space.Self);
    }
    void Update()
    {
        transform.Translate(new Vector3(0, -.01f, 0), Space.Self);
        timer += Time.deltaTime * speed;
        alpha = -Mathf.Pow(timer, 3) + 1;
        wind.color = new Color(1, 1, 1, alpha);
        
        if (wind.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
