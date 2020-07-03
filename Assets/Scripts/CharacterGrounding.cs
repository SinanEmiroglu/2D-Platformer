using UnityEngine;

public class CharacterGrounding : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private LayerMask layerMask;

    private Transform groundedObject;

    //Question mark means Vector3 could also be null. Vector3 normally is not nullable.
    private Vector3? groundedObjectLastPosition;

    public bool IsGrounded { get; private set; }

    public Vector2 GroundedDirection { get; private set; }

    private void Update()
    {
        foreach (var position in positions)
        {
            CheckFootForGrounding(position);
            if (IsGrounded)
                break;
        }

        StickToMovingObjects();
    }

    private void StickToMovingObjects()
    {
        if (groundedObject != null)
        {
            if (groundedObjectLastPosition.HasValue &&
                groundedObjectLastPosition != groundedObject.position)
            {
                Vector3 delta = groundedObject.position - groundedObjectLastPosition.Value;
                transform.position += delta;
            }
            groundedObjectLastPosition = groundedObject.position;
        }
        else
        {
            groundedObjectLastPosition = null;
        }
    }

    private void CheckFootForGrounding(Transform foot)
    {
        var raycastHit = Physics2D.Raycast(foot.position, Vector2.down, maxDistance, layerMask);
        Debug.DrawRay(foot.position, foot.forward * maxDistance, Color.cyan);

        if (raycastHit.collider != null)
        {
            if (groundedObject != raycastHit.collider.transform)
            {
                groundedObjectLastPosition = raycastHit.collider.transform.position;
            }
            groundedObject = raycastHit.collider.transform;
            IsGrounded = true;
            GroundedDirection = foot.forward;
        }
        else
        {
            groundedObject = null;
            IsGrounded = false;
        }
    }
}