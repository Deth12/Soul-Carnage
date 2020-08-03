using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SavesController))]
public class SavesEditor : Editor
{
    private GUIStyle categoryTitle;
    
    public override void OnInspectorGUI()
    {
        categoryTitle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter, 
            fontStyle = FontStyle.Bold
        };
        
        
        DrawDefaultInspector();
        SavesController ctrl = (SavesController) target;
        
        DrawPlayerPrefs(ctrl);
        DrawShopState(ctrl);
        DrawSettingsState(ctrl);
    }
    
    private int addSoulsAmount;
    private void DrawPlayerPrefs(SavesController ctrl)
    {
        EditorGUILayout.Space ();
        EditorGUILayout.LabelField("PlayerPrefs", categoryTitle);
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Total kills: ", GUILayout.MaxWidth(125));
        EditorGUILayout.LabelField(ctrl.GetTotalKills().ToString(), GUILayout.MaxWidth(100)); 
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Reset Total Kills"))
        {
            ctrl.ResetField("TotalKills");
        }
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Souls: ", GUILayout.MaxWidth(125));
        EditorGUILayout.LabelField(ctrl.GetSouls().ToString(), GUILayout.MaxWidth(100)); 
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Reset Souls"))
            ctrl.ResetField("Souls");
        
        EditorGUILayout.BeginHorizontal();
        addSoulsAmount  = EditorGUILayout.IntField("Souls amount: ", addSoulsAmount);
        if (GUILayout.Button("Add Souls", GUILayout.MaxWidth(100f)))
            ctrl.AddSouls(addSoulsAmount);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space ();
    }
    
    private void DrawShopState(SavesController ctrl)
    {
        EditorGUILayout.Space ();
        ShopState s = ctrl.GetShopState();
        EditorGUILayout.LabelField("Shop", categoryTitle);
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bought Weapons: ", GUILayout.MaxWidth(125));
        EditorGUILayout.LabelField(s.boughtId.Count.ToString()); 
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        for(int a = 0; a < s.boughtId.Count; a++)
        {
            EditorGUILayout.LabelField("[Weapon ID]: " + s.boughtId[a].ToString());
        }
        EditorGUILayout.EndVertical();
        
        if (GUILayout.Button("Reset bought weapons"))
        {
            ctrl.ResetBoughtWeapons();
        }
        if (GUILayout.Button("Delete Save Files"))
        {
            ctrl.DeleteJSONFiles();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space ();
    }
    
    private void DrawSettingsState(SavesController ctrl)
    {
        EditorGUILayout.Space ();
        SettingsState ss = ctrl.GetSettingsState();
        EditorGUILayout.LabelField("Settings", categoryTitle);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Volume: ", GUILayout.MaxWidth(125));
        EditorGUILayout.LabelField(ss.volume.ToString(), GUILayout.MaxWidth(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Shadows: ", GUILayout.MaxWidth(125));
        EditorGUILayout.LabelField(ss.shadowsQuality.ToString(), GUILayout.MaxWidth(100));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resolution: ", GUILayout.MaxWidth(125));
        EditorGUILayout.LabelField(ss.resolutionScale.ToString(), GUILayout.MaxWidth(100));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Anti Aliasing: ", GUILayout.MaxWidth(125));
        EditorGUILayout.LabelField(ss.aaQuality.ToString(), GUILayout.MaxWidth(100));
        EditorGUILayout.EndHorizontal();
        
        if (GUILayout.Button("Reset settings"))
        {
            ctrl.ResetSettings();
        }
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space ();
    }
}