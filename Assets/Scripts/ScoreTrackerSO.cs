using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreTrackerSO", menuName = "Scriptable Objects/ScoreTrackerSO")]
public class ScoreTrackerSO : ScriptableObject
{
    [SerializeField] private int _highScore = 0;
    [SerializeField] private List<int> _scores = new List<int>();

    public int HighScore { get => _highScore; set => _highScore = value; }
    public List<int> Scores { get => _scores; set => _scores = value; }
}
