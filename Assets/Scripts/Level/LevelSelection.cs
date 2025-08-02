using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // 关卡管理器脚本
    public LevelsManager levelsManager;

    /// <summary>
    /// 切换关卡
    /// </summary>
    /// <param name="levelNumber"></param>
    public void LevelSelectionButton(int levelNumber)
    {
        StartCoroutine(levelsManager.SwitchLevelController(levelNumber));
    }
}
