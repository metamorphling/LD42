﻿using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreController : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;
    Text txt;
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
        txt.text = score.ToString();
        _signalBus.Subscribe<SignalIncreaseScore>(IncreaseScore);
    }

    void IncreaseScore(SignalIncreaseScore args)
    {
        score += args.score;
        txt.text = score.ToString("0.00");
    }
}