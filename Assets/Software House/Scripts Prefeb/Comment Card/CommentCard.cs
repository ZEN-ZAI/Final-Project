using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommentCard : MonoBehaviour
{
    public Image emoticon;
    public TMP_Text commentText;

    public void Set(CommentData commentData)
    {
        if (commentData.sentiment == SentimentType.Negative)
        {
            emoticon.sprite = CommentAsset.instance.negative;
        }
        else if (commentData.sentiment == SentimentType.Positive)
        {
            emoticon.sprite = CommentAsset.instance.positive;
        }
        else if (commentData.sentiment == SentimentType.Neutral)
        {
            emoticon.sprite = CommentAsset.instance.neutral;
        }

        commentText.text = CommentAsset.instance.GetComment(commentData.commentID, commentData.component);
    }
}
