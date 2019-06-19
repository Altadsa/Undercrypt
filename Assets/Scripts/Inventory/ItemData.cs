using UnityEngine;

public abstract class ItemData : ScriptableObject, IItem
{

    [SerializeField] protected int _itemId;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;

    public int ItemId => _itemId;
    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;

}