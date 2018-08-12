using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerController : MonoBehaviour
{
    Text txt;
    bool isPlaying = false;
    float startTime;

    [Inject]
    readonly SignalBus _signalBus;

    private void OnValidate()
    {
        txt = GetComponent<Text>();
    }

    private void Start()
    {
        _signalBus.Subscribe<SignalGameState>(OnChangeGameState);
    }

    private void Update()
    {
        if (isPlaying == false)
            return;

        txt.text = (Time.time - startTime).ToString("0.00");
    }

    public void Reset()
    {
        txt.text = "0.00";
    }

    void OnChangeGameState(SignalGameState args)
    {
        txt.text = "0.00";
        startTime = Time.time;
        isPlaying = args.isPlaying;
    }

    public void NextLevel()
    {
        _signalBus.Fire(new SignalIncreaseScore() { score = (Time.time - startTime) });
    }
}
