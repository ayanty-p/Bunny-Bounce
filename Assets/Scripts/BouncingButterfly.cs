using UnityEngine;

public class BouncingButterfly : ButterflyBase
{
    [Header("Bounce (bounce off screen edges)")]
    public float bounceMinX = -7f;
    public float bounceMaxX = 7f;

    public void InitializeBouncing(
        bool shouldMoveRight,
        float newBounceMinX,
        float newBounceMaxX
    )
    {
        bounceMinX = newBounceMinX;
        bounceMaxX = newBounceMaxX;

        Initialize(shouldMoveRight);
    }

    protected override void Update()
    {
        MoveButterfly();
        CheckBounceOffScreen();
    }

    protected override void MoveButterfly()
    {
        Vector2 direction = moveRight ? Vector2.right : Vector2.left;

        transform.Translate(
            direction * moveSpeed * Time.deltaTime
        );
    }

    private void CheckBounceOffScreen()
    {
        float checkX = transform.position.x;

        if (checkX > bounceMaxX || checkX < bounceMinX)
        {
            moveRight = !moveRight;
            UpdateFacingDirection();

            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, bounceMinX, bounceMaxX);
            transform.position = position;
        }
    }
}
