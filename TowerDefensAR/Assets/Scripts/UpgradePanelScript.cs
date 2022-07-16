using UnityEngine;

public class UpgradePanelScript : MonoBehaviour
{
    [SerializeField] private GameObject UpgradePanel;

    public void ShowOrHide()
    {
        if (selectplane.plate) UpgradePanel.SetActive(true); 
    }
}