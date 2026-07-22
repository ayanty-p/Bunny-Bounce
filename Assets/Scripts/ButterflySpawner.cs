using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject butterflyPrefab;
    public GameObject circularButterflyPrefab;

    [Header("Spawn Area")]
    public float leftSpawnX = -8f;
    public float rightSpawnX = 8f;
    public float minY = -2f;
    public float maxY = 3f;

    [Header("Spawn Settings")]
    public float spawnInterval = 4f;

    [Header("Circular Movement")]
    public bool useCircularMovement = false;
    public float circleRadius = 1.6f;
    public float circleAngularSpeed = 2.2f;

    [Header("Movement Variety")]
    public bool alternateCircularMovement = false;

    private enum MovementPattern
    {
        Straight,
        Circular
    }

    private float timer;
    private int spawnCount;

    private void Start()
    {
        timer = 0f;

        if (butterflyPrefab == null)
        {
            Debug.LogError("Butterfly Prefab is not set");
        }
    }

    private void Update()
    {
        if (butterflyPrefab == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnButterfly();
            timer = 0f;
        }
    }

    private void SpawnButterfly()
    {
        float randomY = Random.Range(minY, maxY);
        bool spawnFromRight = Random.value > 0.5f;
        float spawnX = spawnFromRight ? rightSpawnX : leftSpawnX;
        bool shouldMoveRight = !spawnFromRight;

        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

        switch (DeterminePattern())
        {
            case MovementPattern.Circular:
                SpawnCircular(spawnPosition, shouldMoveRight);
                break;
            default:
                SpawnStraight(spawnPosition, shouldMoveRight);
                break;
        }
    }

    private MovementPattern DeterminePattern()
    {
        if (alternateCircularMovement)
        {
            int index = spawnCount % 2;
            spawnCount++;

            if (index == 0)
            {
                return MovementPattern.Circular;
            }

            return MovementPattern.Straight;
        }

        if (useCircularMovement)
        {
            return MovementPattern.Circular;
        }

        return MovementPattern.Straight;
    }

    private void SpawnStraight(Vector3 spawnPosition, bool shouldMoveRight)
    {
        GameObject butterflyObject = Instantiate(
            butterflyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Butterfly butterfly = butterflyObject.GetComponent<Butterfly>();

        if (butterfly == null)
        {
            Debug.LogError("Butterfly Prefab is missing Butterfly.cs");
            Destroy(butterflyObject);
            return;
        }

        butterfly.destroyX = Mathf.Max(butterfly.destroyX, MaxSpawnDistance());
        butterfly.Initialize(shouldMoveRight);
    }

    private void SpawnCircular(Vector3 spawnPosition, bool shouldMoveRight)
    {
        GameObject prefab = circularButterflyPrefab != null ? circularButterflyPrefab : butterflyPrefab;

        GameObject butterflyObject = Instantiate(
            prefab,
            spawnPosition,
            Quaternion.identity
        );

        CircularButterfly butterfly = butterflyObject.GetComponent<CircularButterfly>();

        if (butterfly == null)
        {
            Debug.LogError("Circular Butterfly Prefab is missing CircularButterfly.cs");
            Destroy(butterflyObject);
            return;
        }

        butterfly.destroyX = Mathf.Max(butterfly.destroyX, MaxSpawnDistance() + circleRadius);
        butterfly.InitializeCircular(shouldMoveRight, circleRadius, circleAngularSpeed);
    }

    private float MaxSpawnDistance()
    {
        return Mathf.Max(Mathf.Abs(leftSpawnX), Mathf.Abs(rightSpawnX)) + 1f;
    }
}
