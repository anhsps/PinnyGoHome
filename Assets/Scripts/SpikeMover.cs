using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float startY = -7f, endY = 7f;
    [SerializeField] private bool down;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = down ? Vector3.down : Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        if (transform.position.y > endY && !down)
            transform.position = new Vector3(transform.position.x, startY);
        else if (transform.position.y < startY && down)
            transform.position = new Vector3(transform.position.x, endY);
    }
}
