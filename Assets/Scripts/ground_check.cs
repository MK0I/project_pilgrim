using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class ground_check : MonoBehaviour
{
    public enum shapeType // Database of Shapes
    {
        Capsule,
        Box
    }

    // Inspector Settings and Fine Tuning
    [Header("Shape Type Settings")]
    public shapeType shape = shapeType.Capsule;

    public Vector2 size = new Vector2(0.5f, 0.2f);
    public float radius = 0.25f;

    [Header("Detection Setting")]
    public LayerMask groundLayer;
    public Vector2 offset = new Vector2(0f, -0.5f);

    // For Debugging
    [SerializeField] public bool showGizmos = true;

    // Ground Check Proper; Encapsulated for Reusability
    public bool IsGrounded { get; private set; }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        Vector2 worldPos = (Vector2)transform.position + offset;

        switch (shape) // Checks for shapes
        {
            case shapeType.Capsule:
                IsGrounded = Physics2D.OverlapCapsule(
                   worldPos,                            // Position of the check
                   size,                                // Size
                   CapsuleDirection2D.Vertical,         // Capsule Direction
                   0f,                                  // Rotation
                   groundLayer                          // Tag
                );

                break;

            case shapeType.Box:
                IsGrounded = Physics2D.OverlapBox(
                    worldPos,
                    size,
                    0f,
                    groundLayer
                );

                break;
        }

    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Vector2 worldPos = (Vector2)transform.position + offset;

        if (shape == shapeType.Box)
        {
            Gizmos.DrawWireCube(worldPos, size);
        }
        else if (shape == shapeType.Capsule)
        {
            // Capsule Visualizer
            Gizmos.DrawWireSphere(worldPos + Vector2.up * (size.y / 2 - radius), radius);
            Gizmos.DrawWireSphere(worldPos - Vector2.up * (size.y / 2 - radius), radius);
            Gizmos.DrawLine(worldPos + new Vector2(-radius, size.y / 2 - radius), worldPos + new Vector2(-radius, -size.y / 2 + radius));
            Gizmos.DrawLine(worldPos + new Vector2(radius, size.y / 2 - radius), worldPos + new Vector2(radius, -size.y / 2 + radius));
        }

    }

}
