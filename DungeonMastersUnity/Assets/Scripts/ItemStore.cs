using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class Item
{
    public string Title;
    public string Description;
    public SlotType Type;
    public Sprite Sprite;
    public Item(string title, string desc, SlotType type, Sprite sprite)
    {
        Title = title;
        Sprite = sprite;
        Description = desc;
        Type = type;
    }
}

public class ItemStore : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    private void Awake()
    {
        _items = new();
        
    }
    
    public void AddItem(string title, string desc, SlotType type){
        
        Sprite sprite = Resources.Load<Sprite>($"Items/{title}");
        if(sprite == null)
        {
            Debug.LogError($"Sprite not found {title}");
        }
        var item = new Item(title, desc, type, sprite);
        _items.Add(item);
    }
    public IEnumerable<Item> GetItemsOfType(SlotType type){
        return _items.Where(x => x.Type == type);
    }
}
