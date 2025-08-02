using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public KeyCode toggleKey;

    [Header("UI按钮")]
    [Tooltip("菜单按钮")]
    [SerializeField] private GameObject menuButton;
    [Tooltip("关卡选择按钮")]
    [SerializeField] private GameObject levelSelectionButton;

    [Header("UI面板")]
    [Tooltip("菜单面板")]
    [SerializeField] private GameObject menuPanel;
    [Tooltip("关卡选择面板")]
    [SerializeField] private GameObject levelSelectionPanel;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            menuButton.SetActive(!menuButton.activeSelf);
            menuPanel.SetActive(false);
            levelSelectionButton.SetActive(!levelSelectionButton.activeSelf);
            levelSelectionPanel.SetActive(false);

        }
    }

    /// <summary>
    /// 菜单面板切换
    /// </summary>
    public void MenuPanelChange()
    {
        levelSelectionPanel.SetActive(false);
        menuPanel.SetActive(!menuPanel.activeSelf);
    }


    /// <summary>
    /// 关卡选择面板切换
    /// </summary>
    public void LevelSelectionPanelChange()
    {
        menuPanel.SetActive(false);
        levelSelectionPanel.SetActive(!levelSelectionPanel.activeSelf);
    }

}
