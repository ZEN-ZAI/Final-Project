using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Character", menuName = "Software House/Character")]
public class Character : ScriptableObject
{
    public int characterID;
    public string race;
    public int speed;

    public Sprite icon;
    public Sprite sprite;
    public GameObject characterPrefab;
}