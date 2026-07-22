using UnityEngine;

public class CircularButterfly : ButterflyBase
{
    [Header("Circular Movement")]
    public float circleRadius = 1.6f;
    public float circleAngularSpeed = 2.2f;

    private Vector3 circleCenter;
    private float circleAngle;

    public void InitializeCircular(
        bool shouldMoveRight,
        float newCircleRadius,
        float newCircleAngularSpeed
    )
    {
        circleRadius = newCircleRadius;
        circleAngularSpeed = newCircleAngularSpeed;

        Initialize(shouldMoveRight);
    }

    protected override void OnInitializeMovement()
    {
        circleAngle = moveRight ? Mathf.PI : 0f;

        Vector3 initialCircleOffset = new Vector3(
            Mathf.Cos(circleAngle) * circleRadius,
            Mathf.Sin(circleAngle) * circleRadius,
            0f
        );

        circleCenter = transform.position - initialCircleOffset;
    }

    protected override void MoveButterfly()
    {
        float directionX = moveRight ? 1f : -1f;

        circleCenter += Vector3.right * directionX * moveSpeed * Time.deltaTime;
        circleAngle += circleAngularSpeed * Time.deltaTime;

        Vector3 circleOffset = new Vector3(
            Mathf.Cos(circleAngle) * circleRadius,
            Mathf.Sin(circleAngle) * circleRadius,
            0f
        );

        transform.position = circleCenter + circleOffset;
    }

    protected override float GetCheckX()
    {
        return circleCenter.x;
    }
}
