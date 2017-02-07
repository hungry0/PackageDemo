using UnityEngine;
using System.Collections;

public class DragItemUI : ItemUI {

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
