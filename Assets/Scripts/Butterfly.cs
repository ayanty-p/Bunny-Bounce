using UnityEngine;

public class Butterfly : ButterflyBase
{
    protected override void MoveButterfly()
    {
        Vector2 direction = moveRight ? Vector2.right : Vector2.left;

        transform.Translate(
            direction * moveSpeed * Time.deltaTime
        );
    }
}
