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

    [Header("New Game Infos")]
    public List<InputDevice> playerDevices = new List<InputDevice>();
    public StageInfos stageToPlay;
    public GameSettings instanceSettings;

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

    #region GameCreation

    public void ResetGameCreation()
    {
        playerDevices = new List<InputDevice>();
        stageToPlay = null;
        instanceSettings = null;
    }

    public void GoToStage(GameSettings gameSettings)
    {
        instanceSettings = gameSettings;
        sceneManagementUI.FadeInEnds += LoadStage;
        sceneManagementUI.StartFadeIn();
    }

    public void LoadStage()
    {
        sceneManagementUI.FadeInEnds -= LoadStage;
        SceneManager.LoadScene(stageToPlay.stageScene);
        SceneManager.sceneLoaded += StageEnter;
    }

    public void StageEnter(Scene _scene, LoadSceneMode _mode)
    {
        currentGameInstance.InitSession(instanceSettings);
        SceneManager.sceneLoaded -= StageEnter;
        sceneManagementUI.FadeOutEnds += StartSession;
        sceneManagementUI.StartFadeOut();
    }

    public void StartSession()
    {

    }

    #endregion
}


