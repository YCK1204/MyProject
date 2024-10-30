using UnityEngine;

public class CollisionChecker
{
    private float groundCheckDistance;
    private LayerMask whatIsGround;
    public bool IsGrounded { get; private set; }

    public CollisionChecker(float groundCheckDistance, LayerMask whatIsGround)
    {
        this.groundCheckDistance = groundCheckDistance;
        this.whatIsGround = whatIsGround;
    }

    public void CheckGrounded(Vector3 position)
    {
        IsGrounded = Physics2D.Raycast(position, Vector2.down, groundCheckDistance, whatIsGround);
    }
}
