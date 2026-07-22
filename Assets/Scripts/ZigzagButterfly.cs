using UnityEngine;

public class ZigzagButterfly : ButterflyBase
{
    [Header("Zigzag (move up and down while flying)")]
    public float zigzagAmplitude = 1.5f;
    public float zigzagFrequency = 2f;

    private float baseY;
    private float zigzagTime;

    public void InitializeZigzag(
        bool shouldMoveRight,
        float newZigzagAmplitude,
        float newZigzagFrequency
    )
    {
        zigzagAmplitude = Mathf.Max(0.1f, newZigzagAmplitude);
        zigzagFrequency = Mathf.Max(0.1f, newZigzagFrequency);

        Initialize(shouldMoveRight);
    }

    protected override void OnInitializeMovement()
    {
        baseY = transform.position.y;
        zigzagTime = 0f;
    }

    protected override void MoveButterfly()
    {
        Vector2 direction = moveRight ? Vector2.right : Vector2.left;

        transform.Translate(
            direction * moveSpeed * Time.deltaTime
        );

        zigzagTime += zigzagFrequency * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = baseY + Mathf.Sin(zigzagTime) * zigzagAmplitude;
        transform.position = position;
    }
}
