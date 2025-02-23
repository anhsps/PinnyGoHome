using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootTime = 0.1f, timeLife = 1f;
    [SerializeField] private bool down, left, right;
    private Vector2 dir;
    private Transform gunTip;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        dir = down ? Vector2.down : (left ? Vector2.left : (right ? Vector2.right : Vector2.up));
        gunTip = GetComponentInChildren<Transform>();
        //gunTip.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        GunHandling();
    }

    private void GunHandling()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = shootTime;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 randomPos;
        if (left || right)
        {
            float randomY = gunTip.position.y + Random.Range(-0.3f, 0.3f);
            randomPos = new Vector3(gunTip.position.x, randomY);
        }
        else
        {
            float randomX = gunTip.position.x + Random.Range(-0.3f, 0.3f);
            randomPos = new Vector3(randomX, gunTip.position.y);
        }

        GameObject bullet = Instantiate(bulletPrefab, randomPos, transform.rotation);
        bullet.GetComponent<Bullet>().SetDirection(dir);
        Destroy(bullet, timeLife);
    }
}
