using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoad : MonoBehaviour
{
    [SerializeField] private float minWait = 2.5f;
    
    // Loading bar
    [SerializeField] private RectTransform fxHolder = null;
    [SerializeField] private Image fillImg = null;
    [SerializeField] [Range(0, 1)] private float progress = 0f;

    private void Start()
    {
        GameProfile.InitializeJSONFiles();
        StartCoroutine(LoadAsynchronously("MainScene", minWait));
    }

    IEnumerator LoadAsynchronously(string levelName, float minWait) 
    {
        float timer = 0f;
        float minLoadTime = minWait;
     
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);;
        operation.allowSceneActivation = false;
     
        while (!operation.isDone) 
        {
            timer += Time.deltaTime;
            progress = 1f - (timer / minWait);
            fillImg.fillAmount = progress;
            fxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -progress * 360f));
            if (timer > minLoadTime)
                operation.allowSceneActivation = true;
            yield return null;
        }
        yield return null;
    }
}
