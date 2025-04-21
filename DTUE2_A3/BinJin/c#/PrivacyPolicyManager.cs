
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyManager : MonoBehaviour
{
    public GameObject policyPanel;
    public void ShowPolicy()
    {
        policyPanel.SetActive(true);
    }

    public void HidePolicy()
    {
        policyPanel.SetActive(false);
    }
}
