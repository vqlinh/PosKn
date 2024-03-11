using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public Player player;
    public int currentHealth;
    public static Enemy Instance;
    public Transform playerTransform;
    public GameObject prefabCoins;
    int minCoins = 0;
    int maxCoins = 3;

    private void Awake()
    {
        Instance = this;
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
        player = GameObject.FindObjectOfType<Player>();
    }

    public void MoveToNewPosition(Vector2 newPosition)
    {
        transform.DOMove(newPosition, 0.3f).SetEase(Ease.Linear);
    }

    public virtual void Move()
    {
        StartCoroutine(MoveBack());
    }

    private IEnumerator MoveBack( )
    {
        float elapsedTime = 0f;
        float duration = 0.5f;
        Vector2 initialPosition = transform.position;
        Vector2 newPosition = (Vector2)transform.position + (Vector2)transform.right * 2f;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(initialPosition, newPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = newPosition;
    }

    public virtual void Die()
    {
        int numCoins=Random.Range(minCoins, maxCoins);
        for (int i=0;i<=numCoins;i++)
        {
            Instantiate(prefabCoins,transform.position,Quaternion.identity);
        }
        float duration = 0.8f;
        Vector2 targetPosition = new Vector2(transform.position.x + 20f, transform.position.y + 10f); 
        transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
        transform.DORotate(new Vector3(0, 0, 360), 0.1f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }
}
