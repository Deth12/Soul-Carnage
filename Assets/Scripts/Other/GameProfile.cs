using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public static class GameProfile
{
    private static int _score;
    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChange?.Invoke(value);
        }
    }
    
    public static int Souls
    {
        get => PlayerPrefs.GetInt("Souls", 0);
        set
        {
            PlayerPrefs.SetInt("Souls", value);
            OnSoulsChange?.Invoke(value);
        }
    }

    public static int TotalKills
    {
        get => PlayerPrefs.GetInt("TotalKills", 0);
        set => PlayerPrefs.SetInt("TotalKills", value);
    }

    public static System.Action<int> OnScoreChange;
    public static System.Action<int> OnSoulsChange;
    
    public static void InitializeJSONFiles()
    {
        string path = "";
        
        // Shop State
        path = Application.persistentDataPath + "/ShopState.json";
        if (!File.Exists(path))
        {
            Debug.Log("ShopState.json [NOT FOUND]\nCreating new...");
            SaveShopState(new ShopState());
        }
        
        // Settings State
        path = Application.persistentDataPath + "/SettingsState.json";
        if (!File.Exists(path))
        {
            Debug.Log("SettinsState.json [NOT FOUND]\nCreating new...");
            SaveSettings(new SettingsState());
        }
    }

    public static void DeleteSaveFiles()
    {
        string path = "";
        path = Application.persistentDataPath + "/ShopState.json";
        if (File.Exists(path))
            File.Delete(path);
        path = Application.persistentDataPath + "/SettingsState.json";
        if (File.Exists(path))
            File.Delete(path);
    }

    public static void SaveShopState(ShopState shopState)
    {
        JSONObject shopJson = new JSONObject();
        shopJson.Add("SelectedWeaponId", shopState.selectedId);
        JSONArray jsonArray = new JSONArray();
        Debug.Log("Saving bought weapons:");
        foreach (var item in shopState.boughtId)
            jsonArray.Add(item);
        shopJson.Add("BoughtWeaponsId", jsonArray);
        string path = Application.persistentDataPath + "/ShopState.json";
        File.WriteAllText(path, shopJson.ToString());

        /*
        JSONObject shopState = new JSONObject();
        shopState.SerializeBinary(shop);
        shopState.Add("SelectedWeaponId", selectedWeaponId);
        shopState.Add("BoughtItems", boughtItems);
        shopState.
        string path = Application.persistentDataPath + "/ShopState.json";
        File.WriteAllText(path, shopState.ToString());
        Debug.Log("ShopState.json [UPDATED]\n" + shopState.ToString());
        */
    }

    public static ShopState LoadShopState()
    {
        string path = Application.persistentDataPath + "/ShopState.json";
        string jsonString = File.ReadAllText(path);
        JSONObject shopJson = JSON.Parse(jsonString) as JSONObject;
        ShopState shopState = new ShopState();
        shopState.selectedId = shopJson?["SelectedWeaponId"];
        JSONArray jsonArray = shopJson?["BoughtWeaponsId"].AsArray;
        if (jsonArray != null)
        {
            foreach (JSONNode node in jsonArray)
            {
                int id = (int) node;
                if(!shopState.boughtId.Contains(id))
                    shopState.boughtId.Add(id);
            }
        }
        return shopState;
    }

    public static void SaveSettings(SettingsState s)
    {
        JSONObject settingsState = new JSONObject();
        settingsState.Add("Volume", s.volume);
        settingsState.Add("ShadowsQuality", s.shadowsQuality);
        settingsState.Add("ResolutionScale", s.resolutionScale);
        settingsState.Add("AAQuality", s.aaQuality);
        string path = Application.persistentDataPath + "/SettingsState.json";
        File.WriteAllText(path, settingsState.ToString());
        Debug.Log("SettingsState.json [UPDATED]\n" + settingsState.ToString());
    }

    public static SettingsState LoadSettings()
    {
        string path = Application.persistentDataPath + "/SettingsState.json";
        string jsonString = File.ReadAllText(path);
        JSONObject settingsState = JSON.Parse(jsonString) as JSONObject;
        SettingsState s = new SettingsState();
        s.volume = settingsState?["Volume"];
        s.shadowsQuality = settingsState?["ShadowsQuality"];
        s.resolutionScale = settingsState?["ResolutionScale"];
        s.aaQuality = settingsState?["AAQuality"];
        return s;
    }
        
}

[System.Serializable]
public class ShopState
{
    public int selectedId;
    public List<int> boughtId = new List<int>();

    public ShopState()
    {
        selectedId = 1;
        boughtId.Add(1);
    }
}

public class SettingsState
{
    public float volume;
    public int shadowsQuality;
    public float resolutionScale;
    public int aaQuality;

    public SettingsState()
    {
        volume = 1f;
        shadowsQuality = 2;
        resolutionScale = 0.8f;
        aaQuality = 0;
    }
}
