using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public KeyCode toggleKey;

    [Header("UI��ť")]
    [Tooltip("�˵���ť")]
    [SerializeField] private GameObject menuButton;
    [Tooltip("�ؿ�ѡ��ť")]
    [SerializeField] private GameObject levelSelectionButton;

    [Header("UI���")]
    [Tooltip("�˵����")]
    [SerializeField] private GameObject menuPanel;
    [Tooltip("�ؿ�ѡ�����")]
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
    /// �˵�����л�
    /// </summary>
    public void MenuPanelChange()
    {
        levelSelectionPanel.SetActive(false);
        menuPanel.SetActive(!menuPanel.activeSelf);
    }


    /// <summary>
    /// �ؿ�ѡ������л�
    /// </summary>
    public void LevelSelectionPanelChange()
    {
        menuPanel.SetActive(false);
        levelSelectionPanel.SetActive(!levelSelectionPanel.activeSelf);
    }

}
