using UnityEngine;

public abstract class ButterflyBase : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Destroy")]
    public float destroyX = 10f;
    public float destroyMargin = 1.5f;

    [HideInInspector]
    public bool moveRight;

    protected GameManager gameManager;
    protected SpriteRenderer spriteRenderer;

    private bool hasEnteredScreen;
    private bool movementInitialized;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found");
        }

        if (!movementInitialized)
        {
            Initialize(moveRight);
        }
    }

    public void Initialize(bool shouldMoveRight)
    {
        moveRight = shouldMoveRight;
        UpdateFacingDirection();
        OnInitializeMovement();
        hasEnteredScreen = false;
        movementInitialized = true;
    }

    protected virtual void OnInitializeMovement()
    {
    }

    protected virtual void Update()
    {
        MoveButterfly();
        CheckOutsideScreen();
    }

    protected abstract void MoveButterfly();

    protected void UpdateFacingDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = moveRight;
        }
    }

    protected virtual float GetCheckX()
    {
        return transform.position.x;
    }

    private void CheckOutsideScreen()
    {
        float checkX = GetCheckX();

        if (TryGetCameraHorizontalBounds(out float leftBound, out float rightBound))
        {
            float safeDestroyMargin = Mathf.Max(destroyMargin, 1.5f);
            float leftDestroyBound = leftBound - safeDestroyMargin;
            float rightDestroyBound = rightBound + safeDestroyMargin;

            if (!hasEnteredScreen)
            {
                hasEnteredScreen =
                    checkX >= leftDestroyBound &&
                    checkX <= rightDestroyBound;

                if (!hasEnteredScreen)
                {
                    return;
                }
            }

            if (moveRight && checkX > rightDestroyBound)
            {
                Destroy(gameObject);
            }
            else if (!moveRight && checkX < leftDestroyBound)
            {
                Destroy(gameObject);
            }

            return;
        }

        float safeDestroyX = Mathf.Max(Mathf.Abs(destroyX), 10f);

        if (moveRight && checkX > safeDestroyX)
        {
            Destroy(gameObject);
        }
        else if (!moveRight && checkX < -safeDestroyX)
        {
            Destroy(gameObject);
        }
    }

    private bool TryGetCameraHorizontalBounds(out float leftBound, out float rightBound)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            leftBound = 0f;
            rightBound = 0f;
            return false;
        }

        float cameraDistance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        Vector3 leftWorld = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0.5f, cameraDistance)
        );
        Vector3 rightWorld = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 0.5f, cameraDistance)
        );

        leftBound = Mathf.Min(leftWorld.x, rightWorld.x);
        rightBound = Mathf.Max(leftWorld.x, rightWorld.x);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (gameManager != null)
        {
            gameManager.GameOver("GAME OVER!!");
        }

        Destroy(gameObject);
    }
}
