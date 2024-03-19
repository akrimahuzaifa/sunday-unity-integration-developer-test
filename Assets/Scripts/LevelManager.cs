using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<string> SceneNames;

    public static LevelManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Level Manager Awake");
        if (instance == null || instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadAllScenes();
    }

    //For Editor -> Inspector
    [ContextMenu("Get All Build Scenes")]
    void LoadAllScenes()
    {
        // List to store scene names
        SceneNames = new List<string>();

        // Iterate through all scenes in the build settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            SceneNames.Add(System.IO.Path.GetFileNameWithoutExtension(scenePath));
        }
    }

    public string GetLevelName(string sceneName)
    {
        foreach (string isceneName in SceneNames)
        {
            if (sceneName == isceneName)
            {
                return isceneName;
            }
        }
        Debug.Log("No scence found with this name");
        return "";
    }

    public string GetSceneByIndex(int index)
    {
        return SceneNames[index];
    }

    public Scene GetActiveScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene;
    }

    public int GetActiveSceneIndex()
    {
        //Debug.Log($"Scene Index: {GetActiveScene().buildIndex}");
        return GetActiveScene().buildIndex;
    }

    public void ReloadCurrentLevel()
    {
        Debug.Log($"Reloaded Level: {GetActiveScene().name}");
        SceneManager.LoadScene(GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = GetActiveSceneIndex();
        //used for out of index expection to load first level again
        if (currentSceneIndex == (SceneNames.Count - 1))
        {
            Debug.Log("NO NEXT LEVEL FOUND!\n\tRestart Game from Level 1");
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
            Debug.Log($"Level Loaded: {GetSceneByIndex(currentSceneIndex + 1)}");
        }
    }
}
