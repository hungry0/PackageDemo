using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GridUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerClickHandler
{
    public static Action<Transform> OnEnter;
    public static Action OnExit;

    public static Action<Transform> OnLeftBeginDrag;
    public static Action<Transform,Transform> OnLeftEndDrag;

    public static Action<Transform> OnClick;

	void Start () {
	}
	
	void Update () {
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnEnter != null)
            OnEnter(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnExit != null)
            OnExit();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnLeftBeginDrag != null)
        {
            OnLeftBeginDrag(transform);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //鼠标左键
        if (eventData.button == PointerEventData.InputButton.Left && OnLeftEndDrag != null)
        {
            if (eventData.pointerEnter == null)
            {
                OnLeftEndDrag(transform, null);
            }
            else
            {
                OnLeftEndDrag(transform, eventData.pointerEnter.transform);
            }
        }

    }

    //没有此接口方法 则不能拖拽
    //TODO 待查原因
    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
        {
            OnClick(transform);
        }
    }
}
