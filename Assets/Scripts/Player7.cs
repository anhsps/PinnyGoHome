using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player7 : MonoBehaviour
{
    Animator animator => GetComponent<Animator>();
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    Collider2D col => GetComponent<Collider2D>();

    [SerializeField] private GameObject bigBubble, bubblePrefab;
    [SerializeField] private int bubbleCount = 10;
    [SerializeField] private float spawnRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            BubbleBurst();
            Vector3 to = collision.transform.position + Vector3.up * 0.5f;
            StartCoroutine(MoveTarget(gameObject.transform, to));
            rb.simulated = false;
            Invoke(nameof(DelayGameWin), 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager7.instance.isPaused)
        {
            Transform parentObj = collision.transform.parent;
            if (parentObj != null && parentObj.CompareTag("Key"))
            {
                SoundManager7.instance.PlaySound(5);
                parentObj.gameObject.SetActive(false);
                if (GameObject.FindGameObjectsWithTag("Key").Length == 0)
                    Destroy(GameObject.FindGameObjectWithTag("Block"));
            }

            if (collision.gameObject.CompareTag("Trap"))
            {
                Die();
                Invoke(nameof(DelayGameLose), 2f);
            }
        }

    }

    private void Die()
    {
        animator.SetTrigger("Die");
        BubbleBurst();

        rb.gravityScale = 1f;
        col.isTrigger = true;
    }

    // off big bubble, Instantiate small bubbles
    private void BubbleBurst()
    {
        SoundManager7.instance.PlaySound(4);
        GameManager7.instance.isPaused = true;
        GameManager7.instance.pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        bigBubble.SetActive(false);

        for (int i = 0; i < bubbleCount; i++)
        {
            GameObject smallBubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            StartCoroutine(MoveTarget(smallBubble.transform, randomPos));

            var rbBubble = smallBubble.GetComponent<Rigidbody2D>();
            rbBubble.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(4f, 6f));
            Destroy(smallBubble, 3f);
        }
    }

    private IEnumerator MoveTarget(Transform obj, Vector3 to)
    {
        Vector3 from = obj.position;
        float elapsed = 0f, duration = 0.5f;
        while (elapsed < duration)
        {
            obj.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void DelayGameWin() => GameManager7.instance.GameWin();
    private void DelayGameLose() => GameManager7.instance.GameLose();
}
