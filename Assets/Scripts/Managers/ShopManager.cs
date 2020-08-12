using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
        shopState = GameProfile.LoadShopState();
    }
    
    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private Transform itemsContainter = null;

    [SerializeField] private ScrollRect itemsScroll = null;

    [SerializeField] private Image swordsPage = null;
    [SerializeField] private Image scythesPage = null;
    [SerializeField] private Image axesPage = null;
    
    public Color pageSelectedColor;
    public Color pageDeselectedColor;
    
    private List<ShopItem> items = new List<ShopItem>();
    private ShopItem equippedItem;
    private ShopState shopState;
    
    [SerializeField] private Transform weaponHolder;
    
    public void UpdateShopUI()
    {
        foreach (ShopItem i in items)
        {
            i.IsEquipped = false;
            if (i.item.id == shopState.selectedId)
                i.Equip();
            i.IsBought = (shopState.boughtId.Contains(i.item.id));
        }
        ChangeShopPage(1);
    }
    
    public void AddItemToShop(WeaponTemplate item, GameObject obj)
    {
        GameObject g = Instantiate(itemPrefab, itemsContainter);
        ShopItem i = g.GetComponent<ShopItem>();
        i.PopulateItem(item.weaponId, item.weaponType, item.weaponName, item.shopIcon, item.price, obj);
        items.Add(i);
    }

    public void EquipItem(ShopItem item)
    {
        if (equippedItem != null)
            equippedItem.IsEquipped = false;
        equippedItem = item;
        shopState.selectedId = item.item.id;
        GameProfile.SaveShopState(shopState);
    }

    public void BuyItem(ShopItem item)
    {
        if (GameProfile.Souls >= item.item.price)
        {
            Debug.Log(item.item.name + " >> BUY");
            GameProfile.Souls -= item.item.price;
            item.IsBought = true;
            shopState.boughtId.Add(item.item.id);
            GameProfile.SaveShopState(shopState);
        }
    }

    public void ChangeShopPage(int wType)
    {
        itemsScroll.normalizedPosition = Vector2.zero;;
        foreach (ShopItem i in items)
        {
            i.gameObject.SetActive(i.item.type == (WeaponType)wType);
        }
        scythesPage.color = wType == 1 ? pageSelectedColor : pageDeselectedColor;
        swordsPage.color = wType == 2 ? pageSelectedColor : pageDeselectedColor;
        axesPage.color = wType == 3 ? pageSelectedColor : pageDeselectedColor;
    }

    // FOR TESTING PURPOSES
    public void AddMoney()
    {
        GameProfile.Souls += 200;
    }
    
}
