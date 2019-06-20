using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealth : MonoBehaviour
{

    [SerializeField] Image _heartImagePrefab;

    [Tooltip("Starts from Empty")]
    [SerializeField]
    private Sprite[] _heartSprites;

    private Image[] _heartImages;



    private const int HP_PER_HEART = 4;

    int _noOHearts;

    private void Start()
    {
        _heartImages = GetComponentsInChildren<Image>();
        FindObjectOfType<PlayerHealth>().OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(int current, int max)
    {
        _noOHearts = max / HP_PER_HEART;

        if (_heartImages.Length < _noOHearts)
        {
            var heartsToAdd = _noOHearts - _heartImages.Length;
            for (int i = 0; i < heartsToAdd; i++)
            {
                Image newHeart = Instantiate(_heartImagePrefab, transform);
            }
            _heartImages = GetComponentsInChildren<Image>();
        }

        int diff = max - current;
        var div = diff / HP_PER_HEART;
        var remain = diff % HP_PER_HEART;


        var fullHearts = _noOHearts - (div + 1);

        for (int i = 0; i < _noOHearts; i++)
        {
            if (i < fullHearts)
                _heartImages[i].sprite = _heartSprites[4];
            if (i == fullHearts)
                _heartImages[i].sprite = _heartSprites[HP_PER_HEART - remain];
            if (i > fullHearts)
                _heartImages[i].sprite = _heartSprites[0];
        }

        //for (int i = 0; i < fullHearts; i++)
        //{
        //    ;
        //}
        //_heartImages[fullHearts].sprite = _heartSprites[HP_PER_HEART - remain];
        //for (int i = _heartImages.Length - 1; i > _heartImages.Length - (1 + div); i--)
        //{

        //}

        Debug.Log(div);
        Debug.Log(remain);
    }

}
