using System;
using UnityEngine;

public class ScoreSingleton
{
    private int _score = 0;
    private float _timeRemaining = 60;

    public int Score
    {
        get
        {
            return _score;
        }
    }

    public float TimeRemaining
    {
        get
        {
            return _timeRemaining;
        }
    }

    private ScoreSingleton()
    {
    }

    public void Reset()
    {
        _score = 0;
        _timeRemaining = 60;
    }

    public void Add(int value) {
        _score += value;
    }

    public void AddTime(float seconds)
    {
        _timeRemaining += seconds;
    }

    public string TimeRemainingString()
    {
        var t = TimeSpan.FromSeconds(_timeRemaining);
        return t.ToString("mm\\:ss");
    }

    private static ScoreSingleton _instance;
    public static ScoreSingleton Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ScoreSingleton();
            return _instance;
        }
    }
}