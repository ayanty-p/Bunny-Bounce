using UnityEngine;

public class ZigzagButterflySpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject zigzagButterflyPrefab;

    [Header("Spawn Area")]
    public float leftSpawnX = -10f;
    public float rightSpawnX = 10f;
    public float minY = -2f;
    public float maxY = 3f;

    [Header("Spawn Settings")]
    public float spawnInterval = 4f;

    [Header("Zigzag Movement")]
    public float zigzagAmplitude = 1.5f;
    public float zigzagFrequency = 2f;

    private float timer;

    private void Start()
    {
        timer = 0f;

        if (zigzagButterflyPrefab == null)
        {
            Debug.LogError("Zigzag Butterfly Prefab is not set");
        }
    }

    private void Update()
    {
        if (zigzagButterflyPrefab == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnZigzagButterfly();
            timer = 0f;
        }
    }

    private void SpawnZigzagButterfly()
    {
        float randomY = Random.Range(minY, maxY);
        bool spawnFromRight = Random.value > 0.5f;
        float spawnX = spawnFromRight ? rightSpawnX : leftSpawnX;
        bool shouldMoveRight = !spawnFromRight;

        Vector3 spawnPosition = new Vector3(
            spawnX,
            randomY,
            0f
        );

        GameObject butterflyObject = Instantiate(
            zigzagButterflyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        ZigzagButterfly butterfly = butterflyObject.GetComponent<ZigzagButterfly>();

        if (butterfly == null)
        {
            Debug.LogError("Zigzag Butterfly Prefab is missing ZigzagButterfly.cs");
            Destroy(butterflyObject);
            return;
        }

        butterfly.destroyX = Mathf.Max(
            butterfly.destroyX,
            MaxSpawnDistance()
        );

        butterfly.InitializeZigzag(
            shouldMoveRight,
            zigzagAmplitude,
            zigzagFrequency
        );
    }

    private float MaxSpawnDistance()
    {
        return Mathf.Max(Mathf.Abs(leftSpawnX), Mathf.Abs(rightSpawnX)) + 1f;
    }
}
