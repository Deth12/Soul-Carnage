using UnityEngine;

[CreateAssetMenu(menuName = "Settings/SavesController")]
public class SavesController : ScriptableObject
{
    private ShopState shopState;
    
    public int GetTotalKills()
    {
        return GameProfile.TotalKills;
    }

    public int GetSouls()
    {
        return GameProfile.Souls;
    }

    public void AddSouls(int amount)
    {
        GameProfile.Souls += amount;
    }

    public ShopState GetShopState()
    {
        return GameProfile.LoadShopState();
    }

    public void ResetField(string name)
    {
        PlayerPrefs.DeleteKey(name);
    }

    public void DeleteJSONFiles()
    {
        GameProfile.DeleteSaveFiles();
        GameProfile.InitializeJSONFiles();
    }
    
    public void ResetBoughtWeapons()
    {
        GameProfile.SaveShopState(new ShopState());
    }

    public SettingsState GetSettingsState()
    {
        return GameProfile.LoadSettings();
    }

    public void ResetSettings()
    {
        GameProfile.SaveSettings(new SettingsState());
    }
    
}
