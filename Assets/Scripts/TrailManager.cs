using UnityEngine;
using Zenject;

public class TrailManager : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;
    public float periodicity;
    public Transform currentPosition;
    public GameObject toSpawn;
    public Transform trailStorage;
    float timePassed;
    bool isPlaying;

    void Start()
    {
        timePassed = 0;
        _signalBus.Subscribe<SignalGameState>(OnChangeGameState);
    }

    void Update()
    {
        if (isPlaying == false)
            return;

        timePassed += Time.deltaTime;
        if (timePassed > periodicity)
        {
            timePassed = 0;
            var go = Instantiate(toSpawn);
            go.transform.position = currentPosition.position;
            go.transform.SetParent(trailStorage);
        }
    }

    void OnChangeGameState(SignalGameState args)
    {
        timePassed = 0;
        isPlaying = args.isPlaying;
        if (args.isPlaying == false)
        {
            GetComponent<TrailRenderer>().Clear();
            for (int i = 0; i < trailStorage.childCount; i++)
                Destroy(trailStorage.GetChild(i).gameObject);
        }
    }
}
