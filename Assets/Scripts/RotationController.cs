using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Transform target;
    Vector3 direction;

    void Update()
    {
        direction = (target.position - transform.position).normalized;
        direction.z = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
