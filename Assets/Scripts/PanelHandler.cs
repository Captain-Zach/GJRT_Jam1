using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    [SerializeField] Canvas LevelSelectorCanvas;
    [SerializeField] Canvas CreditsCanvas;
    [SerializeField] Canvas TipsAndTricksCanvas;

    private SecondaryMenu ActiveMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClosePanel()
    {
        if (LevelSelectorCanvas.gameObject.activeSelf) LevelSelectorCanvas.gameObject.SetActive(false);
        if (CreditsCanvas.gameObject.activeSelf) CreditsCanvas.gameObject.SetActive(false);
        if (TipsAndTricksCanvas.gameObject.activeSelf) TipsAndTricksCanvas.gameObject.SetActive(false);

    }

    public void OpenPanel(SecondaryMenu menu)
    {
        if (!LevelSelectorCanvas.gameObject.activeSelf && menu == SecondaryMenu.LevelSelector) LevelSelectorCanvas.gameObject.SetActive(true);
        if (!TipsAndTricksCanvas.gameObject.activeSelf && menu == SecondaryMenu.LevelSelector) TipsAndTricksCanvas.gameObject.SetActive(true);
        if (!CreditsCanvas.gameObject.activeSelf && menu == SecondaryMenu.LevelSelector) CreditsCanvas.gameObject.SetActive(true);
    }

    public void SwitchPanel(SecondaryMenu menu)
    {
        ClosePanel();
        OpenPanel(menu);
    }

    public enum SecondaryMenu
    {
        LevelSelector = 0,
        Credits = 1,
        TipsAndTricks = 2
    }
}
