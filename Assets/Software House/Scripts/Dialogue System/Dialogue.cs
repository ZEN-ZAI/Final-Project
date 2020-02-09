using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue {

    public Sprite characterSprite;
    public string characterName;
    [TextArea(1,100)]
    public string[] sentances;


}
