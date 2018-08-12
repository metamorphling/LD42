using UnityEngine;

public class TrailStorageController : MonoBehaviour
{
    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }
}
