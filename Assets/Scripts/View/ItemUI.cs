using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text m_ItemName;

    public void UpdateItemName(string name)
    {
        m_ItemName.text = name;
    }
}
