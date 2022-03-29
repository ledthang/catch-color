using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 5;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
        rb.velocity *= Mathf.Pow(1.2f, GameController.GetInstance().score / 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!transform.CompareTag(other.transform.tag))
        {
            GameController.GetInstance().GameOver();
        }
        if (transform.CompareTag(other.transform.tag))
        {
            GameController.GetInstance().Score();
        }
        gameObject.SetActive(false);
        Destroy(gameObject, 3.0f);
    }
}
