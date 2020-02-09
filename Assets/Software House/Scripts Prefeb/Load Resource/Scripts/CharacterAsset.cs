using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterAsset : MonoBehaviour
{
    #region Singleton
    public static CharacterAsset instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public List<Character> characterAsset;

    private void Start()
    {
        characterAsset = Resources.LoadAll<Character>("Character").ToList();
    }

    public Character GetCharacter(int characterID)
    {
        return characterAsset.Find(e => e.characterID == characterID);
    }

    public Sprite GetCharacterSprite(int characterID)
    {
        return characterAsset.Find(e => e.characterID == characterID).sprite;
    }

    public Sprite GetCharacterIcon(int characterID)
    {
        return characterAsset.Find(e => e.characterID == characterID).icon;
    }

    public int RandomCharacterID()
    {
        return characterAsset[Random.Range(0, characterAsset.Count)].characterID;
    }
}