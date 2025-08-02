using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // �ؿ��������ű�
    public LevelsManager levelsManager;

    /// <summary>
    /// �л��ؿ�
    /// </summary>
    /// <param name="levelNumber"></param>
    public void LevelSelectionButton(int levelNumber)
    {
        StartCoroutine(levelsManager.SwitchLevelController(levelNumber));
    }
}
