using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private int m_startMoney,
        m_startLives;

    [SerializeField]
    private Text m_moneyAmountText;

    [SerializeField]
    private Text m_livesAmountText;

    private int m_currentMoney;

    public int CurrentMoney
    {
        get
        {
            return m_currentMoney;
        }
        set
        {
            m_currentMoney = value;
            m_moneyAmountText.text = "$" + m_currentMoney.ToString();
        }
    }

    private int m_currentLives;

    public int CurrentLives
    {
        get
        {
            return m_currentLives;
        }
        set
        {
            m_currentLives = value;
            m_livesAmountText.text = m_currentLives.ToString() + " LIVES";
        }
    }

   
    private void Awake()
    {
        CurrentMoney = m_startMoney;
        CurrentLives = m_startLives;
    }


}
