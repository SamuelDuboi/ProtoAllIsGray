using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage Infos", fileName = "NewStageInfos")]
public class StageInfos : ScriptableObject
{
    public SceneField stageScene;
    public string stageName;
    public Sprite stageScreen;
}
