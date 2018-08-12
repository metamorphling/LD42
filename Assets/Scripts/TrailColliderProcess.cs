using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColliderProcess : MonoBehaviour {
    public BoxCollider collider;
    WaitForSeconds timer;

    void Start ()
    {
        timer = new WaitForSeconds(3);
        StartCoroutine(ColliderActivation());
    }

    IEnumerator ColliderActivation()
    {
        yield return timer;
        collider.enabled = true;
    }
}
