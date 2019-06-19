using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class SaveData
{

}

public class PlayerSaveData
{

}

[System.Serializable]
public class InventorySaveData
{
    public List<int> ItemIds;
    public int Arrows;
    public int Keys;

    public InventorySaveData()
    {
        var inv = Inventory.Instance;
        ItemIds = new List<int>();
        for (int i = 0; i < inv.EquippableItems.Count; i++)
        {
            ItemIds.Add(inv.EquippableItems[i].ItemId);
        }

        Arrows = inv.Arrows;
        Keys = inv.Keys;
    }
}

public static class SaveSystem
{
    public static void SaveGame(InventorySaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/SaveGame.Uncr";
        using (FileStream stream = new FileStream(savePath, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
        }
    }

    public static InventorySaveData LoadSave()
    {
        string savePath = Application.persistentDataPath + "/SaveGame.Uncr";
        if (File.Exists(savePath))
        {
            using (FileStream stream = new FileStream(savePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as InventorySaveData;
            }
        }
        else
        {
            Debug.LogError("No Save data found at " + savePath);
            return null;
        }
    }
}