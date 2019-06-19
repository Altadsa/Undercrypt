using UnityEngine;

public interface IAxisInput
{
    void ReadInput();
    bool HasAxisInput { get; }
    float Horizontal { get; }
    float Vertical { get; }
}

public interface ICommandInput
{
    bool CommandKey { get; }

}

public interface IHealth
{
    void UpdateHealth(int changeInHealth);
}

public interface IItem
{
    int ItemId { get; }
    Sprite Icon { get; }
    string Name { get; }
    string Description { get; }
}

public interface IEquippableItem : IItem
{
    GameObject ItemPrefab { get; }
}

public interface IConsumableItem : IItem
{
    int Quantity { get; }
}