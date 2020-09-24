using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public static WeatherSystem Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    [Header("General")]
    [SerializeField] private Transform player;
    [SerializeField] private float spawnerHeight = 30f;
    [SerializeField] private Transform weatherSpawner;

    [Header("Weather Effects")]
    public ParticleSystem[] weatherList;
    [SerializeField] private ParticleSystem currentWeather;
    [SerializeField] private Light mainDirectionalLight;

    [SerializeField] [Range(0f, 1f)] private float emptyWeatherChance = 0.3f;
    [SerializeField] private float weatherChangeInterval = 60f;
    [SerializeField] private float timeToChange = 0f;

    [Header("Day/Night cycles")]
    [SerializeField] private float dayCycle = 120f;
    [SerializeField] private float minLightIntensity = 1.5f;
    [SerializeField] private float maxLightIntensity = 5.5f;

    private bool isWeatherActivated = false;
    private bool isWeatherDisabled = false;

    public void Activate()
    {
        timeToChange = weatherChangeInterval / 5;
        if(!isWeatherDisabled)
            isWeatherActivated = true;
        StartCoroutine(DayCycle(maxLightIntensity, minLightIntensity));
    }

    IEnumerator DayCycle(float initialIntensity, float targetIntensity)
    {
        float elapsed = 0f;
        while (elapsed < dayCycle)
        {
            elapsed += Time.deltaTime;
            mainDirectionalLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, elapsed / dayCycle);
            yield return null;
        }
        StartCoroutine(DayCycle(targetIntensity, initialIntensity));
        Debug.Log("Cycle Done");
    }
    

    public void DisableWeather()
    {
        isWeatherDisabled = true;
        isWeatherActivated = false;
        if(currentWeather != null)
            currentWeather.Stop();
    }

    public void EnableWeather()
    {
        isWeatherDisabled = false;
        if (GameManager.Instance.IsGameStarted)
            isWeatherActivated = true;
        timeToChange = weatherChangeInterval / 5;
    }

    private void Update()
    {
        if(!isWeatherDisabled)
            FollowPlayer();
        if (isWeatherActivated)
            Tick();
    }

    private void FollowPlayer()
    {
        weatherSpawner.transform.position 
            = new Vector3(player.transform.position.x, spawnerHeight, player.transform.position.z);
    }
    
    private void Tick()
    {
        timeToChange -= Time.deltaTime;
        if (timeToChange <= 0)
        {
            timeToChange = weatherChangeInterval;
            RandomizeWeather();
        }
    }

    private void RandomizeWeather()
    {
        if(currentWeather != null)
            currentWeather.Stop();
        if (Random.Range(0f, 1f) > emptyWeatherChance)
        {
            currentWeather = weatherList[Random.Range(0, weatherList.Length)];
            currentWeather.Play();
        }
        else
        {
            // Empty weather, lower delay
            timeToChange /= 2f;
        }
    }
    
}
