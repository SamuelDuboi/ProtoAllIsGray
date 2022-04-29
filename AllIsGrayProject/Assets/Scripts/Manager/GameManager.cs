using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene Management References")]
    [SerializeField]
    SceneManagementUI sceneManagementUI;

    [Header("Game Instance References")]
    [SerializeField, ReadOnly]
    public static  GameInstanceHandler currentGameInstance;

    [Header("Global Game Infos")]
    public PlayerColorBank colorBank;

    public List<InputDevice> playerDevices = new List<InputDevice>();

    private void Awake()
    {
        #region Singleton/DDOL
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        #endregion
    }

    #region Scenes Management Methods
    
    [Button("Launch Fade In")]
    public void TestFadeIn()
    {
        sceneManagementUI.FadeInEnds += TestFadeOut;
        sceneManagementUI.StartFadeIn();
    }

    [Button("Launch Fade Out")]
    public void TestFadeOut()
    {
        sceneManagementUI.FadeInEnds -= TestFadeOut;
        sceneManagementUI.FadeOutEnds += TestInvoke;
        sceneManagementUI.StartFadeOut();
    }

    public void TestInvoke()
    {
        print("Invoke Done");
        sceneManagementUI.FadeOutEnds -= TestInvoke;
    }
    #endregion

    #region Game Instance Management

    public void ReferenceGameInstance(GameInstanceHandler instance)
    {
        currentGameInstance = instance;
    }

    #endregion
}


