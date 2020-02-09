using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Comment", menuName = "Software House/Comment")]
public class Comment : ScriptableObject
{
    public int commentID;
    public SentimentType sentimentType;
    public string sentence;
}
