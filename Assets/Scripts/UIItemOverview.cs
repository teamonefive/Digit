using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemOverview : MonoBehaviour
{
    public ItemOverview m_itemOverview;

    public Text m_nameTextField;
    public Text m_statsTextField;
    public Text m_descriptionTextField;


    private void Start() {
        m_nameTextField = m_itemOverview.m_name;
        m_statsTextField = m_itemOverview.m_stats;
        m_descriptionTextField = m_itemOverview.m_description;
    }
}
