using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindFade : MonoBehaviour
{
    public SpriteRenderer windRender;
    float timer = 0, alpha;
    public float speed = 2;
    float color;
    private void Start()
    {
        transform.Translate(new Vector3(0, 0, -5), Space.Self);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            color = .3f;
        }
        else color = 1;
        windRender = GetComponent<SpriteRenderer>();
        windRender.enabled = true;
    }
    void Update()
    {
        transform.Translate(new Vector3(0, -.01f, 0), Space.Self);
        timer += Time.deltaTime * speed;
        alpha = -Mathf.Pow(timer, 3) + 1;
        windRender.color = new Color(color,1,color, alpha);
        
        if (windRender.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
