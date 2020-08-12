using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform WeaponHolder = null;
    
    void Start()
    {
        foreach (WeaponTemplate w in ResourcesManager.Instance.Weapons)
        {
            GameObject go = Instantiate(w.weaponPrefab, transform.position, transform.rotation, WeaponHolder);
            go.transform.localPosition = w.posTemplate.HandPos;
            go.transform.localRotation = w.posTemplate.HandRot;
            ShopManager.Instance.AddItemToShop(w, go);
        }
        ShopManager.Instance.UpdateShopUI();
    }
}
