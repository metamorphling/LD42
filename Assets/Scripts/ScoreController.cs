using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreController : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;
    public Text txt;
    public float score;

    private void OnValidate()
    {
        txt = GetComponent<Text>();
    }

    public void Reset()
    {
        txt.text = "0";
    }

    private void Start()
    {
        score = 0;
        txt.text = score.ToString();
        _signalBus.Subscribe<SignalIncreaseScore>(IncreaseScore);
        _signalBus.Subscribe<SignalGameState>(Reset);
    }

    void IncreaseScore(SignalIncreaseScore args)
    {
        score += args.score;
        txt.text = score.ToString("0.00");
    }
}
