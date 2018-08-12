using UnityEngine;
using Zenject;

public class DebugController : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;
    bool enable = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            enable = !enable;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(enable);
            }
        }
    }
}
