using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealth : MonoBehaviour
{
    private Image[] _heartImages;

    [Tooltip("Starts from Empty")]
    [SerializeField]
    private Sprite[] _heartSprites;

    private const int HP_PER_HEART = 4;

    private void Start()
    {
        _heartImages = GetComponentsInChildren<Image>();
        FindObjectOfType<PlayerHealth>().OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(int current, int max)
    {
        int diff = max - current;
        var div = diff / HP_PER_HEART;
        var remain = diff % HP_PER_HEART;

        var noToFill = max / HP_PER_HEART - (div + 1);

        for (int i = 0; i < noToFill; i++)
        {
            _heartImages[i].sprite = _heartSprites[4];
        }
        _heartImages[noToFill].sprite = _heartSprites[HP_PER_HEART - remain];
        for (int i = _heartImages.Length-1; i > _heartImages.Length - (1 + div); i--)
        {
            _heartImages[i].sprite = _heartSprites[0];
        }

        Debug.Log(div);
        Debug.Log(remain);
    }

}
