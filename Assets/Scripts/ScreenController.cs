using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class ScreenController : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;
    [Inject]
    GameManager gm;
    [Inject]
    ScoreController sc;

    Image img;
    public List<Text> texts;
    bool isPlaying;


    void Start()
    {
        _signalBus.Subscribe<SignalChangeColors>(OnChangeColor);
        _signalBus.Subscribe<SignalGameState>(OnChangeGameState);
    }

    private void OnValidate()
    {
        img = GetComponent<Image>();
    }

    void OnChangeColor(SignalChangeColors args)
    {
    }

    void OnChangeGameState(SignalGameState args)
    {
        if (args.isPlaying == false)
        {

            img.DOFade(1, 1f);
            texts[0].text = "Score: " + sc.score.ToString("0");
            foreach (var txt in texts)
                txt.DOFade(1, 0.6f);
            isPlaying = false;
        }
    }

    private void Update()
    {
        if (isPlaying == false && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        isPlaying = true;
        img.DOFade(0, 1f);
        foreach (var txt in texts)
            txt.DOFade(0, 0.6f);
        _signalBus.Fire(new SignalGameState { isPlaying = true } );
    }

}
