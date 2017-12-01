using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    [SerializeField]
    private GameObject m_ui;

    [SerializeField]
    private Text m_priceText,
        m_sellPriceText;

    private Node m_targetNode;

    [SerializeField]
    private Button m_upgradeButton;


    public void SetTarget(Node node)
    {
        m_targetNode = node;
        transform.position = m_targetNode.transform.position;
        m_ui.SetActive(true);
        m_upgradeButton.interactable = !node.IsUpgraded;
        m_sellPriceText.text = Mathf.RoundToInt(node.UsedTurretBlueprint.Cost / 2).ToString();
        if (node.IsUpgraded)
        {
            m_priceText.text = "DONE";
        }
        else
        {
            m_priceText.text = node.UsedTurretBlueprint.UpgradeCost.ToString();
        }
        
    }

    public void Hide()
    {
        m_ui.SetActive(false);
    }

    public void Upgrade()
    {
        BuildManager.Instance.UpgradeTurret();
    }

    public void Sell()
    {
        BuildManager.Instance.SellTurret();
    }
}
