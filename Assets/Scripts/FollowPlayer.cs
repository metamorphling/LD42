using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    float distance;
    public float distanceDamping = 2.0f;

    private void Start()
    {
        distance = target.position.z - transform.position.z;
        float currentDistance = target.position.z - transform.position.z;
        float wantedDistance = target.position.z - distance;
    }

    void Update()
    {
        float currentDistance =  target.position.z - transform.position.z;
        float wantedDistance = target.position.z - distance;
        currentDistance = Mathf.Lerp(transform.position.z, wantedDistance, distanceDamping * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, currentDistance);
    }
}