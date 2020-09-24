using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using ShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    public UniversalRenderPipelineAsset pipeline;

    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Dropdown shadowsDropdown = null;
    [SerializeField] private Slider resolutionSlider = null;
    [SerializeField] private Dropdown aaDropdown = null;
    [SerializeField] private Dropdown weatherDropdown = null;
    [SerializeField] private Dropdown fpsDropdown = null;
    [SerializeField] private GameObject fpsObject = null;
    
    private void Start()
    {
        // InitSettings();
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        shadowsDropdown.onValueChanged.AddListener(ChangeShadowsQuality);
        resolutionSlider.onValueChanged.AddListener(ChangeResolutionScale);
        aaDropdown.onValueChanged.AddListener(ChangeAAQuality);
        weatherDropdown.onValueChanged.AddListener(SwitchWeather);
        fpsDropdown.onValueChanged.AddListener(ShowFps);
        LoadSettings();
    }

    private void LoadSettings()
    {
        SettingsState s = GameProfile.LoadSettings();
        
        volumeSlider.value = s.volume;
        volumeSlider.onValueChanged.Invoke(s.volume);
        
        shadowsDropdown.value = s.shadowsQuality;
        shadowsDropdown.onValueChanged.Invoke(s.shadowsQuality);
        
        resolutionSlider.value = s.resolutionScale;
        resolutionSlider.onValueChanged.Invoke(s.resolutionScale);

        aaDropdown.value = s.aaQuality;
        aaDropdown.onValueChanged.Invoke(s.aaQuality);

        weatherDropdown.value = s.weather;
        weatherDropdown.onValueChanged.Invoke(s.weather);
        
        fpsDropdown.value = s.showFps;
        fpsDropdown.onValueChanged.Invoke(s.showFps);
    }

    public void SaveSettings()
    {
        SettingsState s = new SettingsState();
        s.volume = volumeSlider.value;
        s.shadowsQuality = shadowsDropdown.value;
        s.resolutionScale = resolutionSlider.value;
        s.aaQuality = aaDropdown.value;
        s.weather = weatherDropdown.value;
        s.showFps = fpsDropdown.value;
        GameProfile.SaveSettings(s);
    }

    // private void InitSettings()
    // {
    //     switch (MainLightShadowResolution)
    //     {
    //         case ShadowResolution._512:
    //             shadowsDropdown.value = 1;
    //             break;
    //         case ShadowResolution._1024:
    //             shadowsDropdown.value = 2;
    //             break;
    //         case ShadowResolution._2048:
    //             shadowsDropdown.value = 3;
    //             break;
    //         case ShadowResolution._4096:
    //             shadowsDropdown.value = 4;
    //             break;
    //         default:
    //             shadowsDropdown.value = 0;
    //             break;
    //     }
    //     resolutionSlider.value = pipeline.renderScale;
    // }

    private void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

    private void ChangeShadowsQuality(int value)
    {
        switch (value)
        {
            case 0:
                pipeline.shadowCascadeOption = ShadowCascadesOption.NoCascades;
                MainLightShadowResolution = ShadowResolution._256;
                pipeline.shadowDistance = 0;
                break;
            case 1:
                pipeline.shadowCascadeOption = ShadowCascadesOption.NoCascades;
                pipeline.shadowDistance = 50;
                MainLightShadowResolution = ShadowResolution._512;
                break;
            case 2:
                pipeline.shadowCascadeOption = ShadowCascadesOption.NoCascades;
                pipeline.shadowDistance = 120;
                MainLightShadowResolution = ShadowResolution._1024;
                break;
            case 3:
                pipeline.shadowCascadeOption = ShadowCascadesOption.TwoCascades;
                pipeline.shadowDistance = 150;
                MainLightShadowResolution = ShadowResolution._1024;
                break;
            case 4:
                pipeline.shadowCascadeOption = ShadowCascadesOption.FourCascades;
                pipeline.shadowDistance = 250;
                MainLightShadowResolution = ShadowResolution._1024;
                break;
        }
    }

    private void ChangeResolutionScale(float value)
    {
        pipeline.renderScale = value;
    }

    private void ChangeAAQuality(int value)
    {
        switch (value)
        {
            case 0:
                pipeline.msaaSampleCount = 0;
                break;
            case 1:
                pipeline.msaaSampleCount = 2;
                break;
            case 2:
                pipeline.msaaSampleCount = 4;
                break;
        }
    }

    private void SwitchWeather(int value)
    {
        switch (value)
        {
            case 0:
                WeatherSystem.Instance.DisableWeather();
                break;
            case 1:
                WeatherSystem.Instance.EnableWeather();
                break;
        }
    }

    private void ShowFps(int value)
    {
        switch (value)
        {
            case 0:
                fpsObject.SetActive(false);
                break;
            case 1:
                fpsObject.SetActive(true);
                break;
        }
    }
    
    
    private System.Type universalRenderPipelineAssetType;
    private FieldInfo mainLightShadowmapResolutionFieldInfo;
 
    private void InitializeShadowMapFieldInfo()
    {
        universalRenderPipelineAssetType = (GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).GetType();
        mainLightShadowmapResolutionFieldInfo = universalRenderPipelineAssetType.GetField("m_MainLightShadowmapResolution", BindingFlags.Instance | BindingFlags.NonPublic);
    }
   
    public ShadowResolution MainLightShadowResolution
    {
        get
        {
            if (mainLightShadowmapResolutionFieldInfo == null)
            {
                InitializeShadowMapFieldInfo();
            }
            return (ShadowResolution) mainLightShadowmapResolutionFieldInfo.GetValue(GraphicsSettings.currentRenderPipeline);
        }
        set
        {
            if (mainLightShadowmapResolutionFieldInfo == null)
            {
                InitializeShadowMapFieldInfo();
            }
            mainLightShadowmapResolutionFieldInfo.SetValue(GraphicsSettings.currentRenderPipeline, value);
        }
    }
}
