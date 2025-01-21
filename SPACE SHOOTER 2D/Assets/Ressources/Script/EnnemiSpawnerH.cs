using System.Collections;
using UnityEngine;

public class EnnemiSpawnerH : MonoBehaviour
{
    [SerializeField] GameObject ennemiPrefab;
    [SerializeField] GameObject bonusPrefab;

    [SerializeField]Collider2D spawnArea;

    void Start()
    {
        StartCoroutine(SpawnEnnemis());
        StartCoroutine(SpawnBonus());
    }

    IEnumerator SpawnEnnemis()
    {
        while (true)
        {
            float spawnInterval = Random.Range(0f, 5f);
            yield return new WaitForSeconds(spawnInterval);

            Vector2 spawnPosition = GetRandomPositionInCollider();
            Instantiate(ennemiPrefab, spawnPosition, Quaternion.Euler(0, 0, -180));
        }
    }

    IEnumerator SpawnBonus()
    {
        while (true)
        {
            float spawnInterval = Random.Range(10f, 20f);
            yield return new WaitForSeconds(spawnInterval);

            Vector2 spawnPosition = GetRandomPositionInCollider();
            Instantiate(bonusPrefab, spawnPosition, Quaternion.Euler(0, 0, -180));
        }
    }


    Vector2 GetRandomPositionInCollider()
    {
        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }
}
