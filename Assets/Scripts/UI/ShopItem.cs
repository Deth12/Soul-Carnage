using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ItemInfo item { get; private set; }
    
    [SerializeField] private Text nameText = null;
    [SerializeField] private Image itemIcon = null;
    [SerializeField] private Text priceText = null;
    
    [SerializeField] private GameObject itemObject = null;

    [SerializeField] private GameObject selectedStyle = null;
    [SerializeField] private GameObject deselectedStyle = null;
    [SerializeField] private GameObject buyableStyle = null;

   // private bool _isSelected;

    public bool IsEquipped
    {
        get => selectedStyle.activeSelf;
        set
        {
            itemObject.SetActive(value);
            selectedStyle.SetActive(value);
            deselectedStyle.SetActive(!value);
        }
    }

    public bool IsBought
    {
        get => buyableStyle.activeSelf;
        set
        {
            buyableStyle.SetActive(!value);
        }
    }

    public void Equip()
    {
        if (IsEquipped)
            return;
        Debug.Log("Equip item:" + item.name);
        IsEquipped = true;
        ShopManager.Instance.EquipItem(this);
    }

    public void Buy()
    {
        Debug.Log("Buy item: " + item.name);
        ShopManager.Instance.BuyItem(this);
    }

    public void PopulateItem(int id, WeaponType type, string name, Sprite icon, int price, GameObject obj)
    {
        item = new ItemInfo(id, type, name, icon, price);
        itemObject = obj;
        UpdateShopItem();
    }

    public void UpdateShopItem()
    {
        nameText.text = item.name;
        itemIcon.sprite = item.icon;
        priceText.text = item.price.ToString();
    }
    
}

[System.Serializable]
public class ItemInfo
{
    public int id;
    public WeaponType type;
    public string name;
    public Sprite icon;
    public int price;

    public ItemInfo(int id, WeaponType type, string name, Sprite icon, int price)
    {
        this.id = id;
        this.type = type;
        this.name = name;
        this.icon = icon;
        this.price = price;
    }
}


