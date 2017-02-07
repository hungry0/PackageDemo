using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public Text contentText;
    public Text outlineText;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void UpdateToolTip(string content)
    {
        contentText.text = content;
        outlineText.text = content;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
