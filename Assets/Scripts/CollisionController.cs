using UnityEngine;
using Zenject;

public class CollisionController : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            _signalBus.Fire(new SignalGameState { isPlaying = false });
        }
    }

}
