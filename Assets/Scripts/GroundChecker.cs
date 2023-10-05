using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private LayerMask groundLayer;

    public bool onGround
    {
        get
        {
            return Physics.OverlapSphere(transform.position, checkRadius, groundLayer).Length > 1;
        }
    }
}
