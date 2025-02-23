using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f, pushForce = 5f;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb) rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
