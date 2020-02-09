using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CommentAsset : MonoBehaviour
{
    #region Singleton
    public static CommentAsset instance;
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

    private void Start()
    {
        positive = Resources.Load<Sprite>("Comment/Sprite/Positive");
        negative = Resources.Load<Sprite>("Comment/Sprite/Negative");
        neutral = Resources.Load<Sprite>("Comment/Sprite/Neutral");

        positiveComment = Resources.LoadAll<Comment>("Comment/Positive").ToList();
        negativeComment = Resources.LoadAll<Comment>("Comment/Negative").ToList();
        neutralComment = Resources.LoadAll<Comment>("Comment/Neutral").ToList();

        CombineAsset();
    }

    public Sprite positive;
    public Sprite negative;
    public Sprite neutral;

    public List<Comment> positiveComment;
    public List<Comment> negativeComment;
    public List<Comment> neutralComment;

    public List<Comment> commentAsset;

    private void CombineAsset()
    {
        foreach (Comment comment in positiveComment)
        {
            commentAsset.Add(comment);
        }

        foreach (Comment comment in negativeComment)
        {
            commentAsset.Add(comment);
        }

        foreach (Comment comment in neutralComment)
        {
            commentAsset.Add(comment);
        }
    }

    public string GetComment(int commentID,string component)
    {
        Comment comment = commentAsset.Find(e => e.commentID == commentID);
        string sentence = String.Copy(comment.sentence);

        return sentence.Replace("{0}", component);
    }
}
