using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Score
{
    public List<string> names;
    public List<int> scores;

    public static Score CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Score>(jsonString);
    }

    public void SaveToJson(Dictionary<string, int> dict)
    {
        names = dict
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();
        scores = dict
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Value)
            .ToList();
        string json = JsonUtility.ToJson(this);
        File.WriteAllText("scores.json", json);
    }
}