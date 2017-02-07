using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ButtonManager : MonoBehaviour {

    public void DeleteButtonClick(Image selectedImage)
    {
        if (selectedImage.IsActive() == false) return;

        selectedImage.gameObject.SetActive(false);

        Destroy(selectedImage.transform.parent.FindChild("Item(Clone)").gameObject);
        ItemModel.DeleteItem(selectedImage.transform.parent.name);

        selectedImage.transform.SetParent(null);
    }

    public void AddButtonClick()
    {
        int random = Random.Range(0, 10);
        KnapsackManager.Instance.StoreItem(random);
    }

    public void SortItemButtonClick()
    {
        var gridItem = ItemModel.m_GridItem;

        var itemValueArray = new Item[gridItem.Values.Count];
        var itemKeyArray = new string[gridItem.Keys.Count];

        gridItem.Values.CopyTo(itemValueArray,0);
        gridItem.Keys.CopyTo(itemKeyArray,0);

        //将value排序
        itemValueArray = itemValueArray.OrderBy(p => p).ToArray();

        //新建一个排序词典，将排序后的value替换
        var tempGridItem = new SortedDictionary<string,Item>();
        for (int i = 0; i < itemKeyArray.Length; i++)
        {
            tempGridItem[itemKeyArray[i]] = itemValueArray[i];
        }

        gridItem = tempGridItem;
        ItemModel.m_GridItem = gridItem;

        var girds = GridPanelUI.Instance.m_Grids;
        var gridItemList = gridItem.ToList();

        int j = 0;
        for (int i = 0; i < girds.Length; i++)
        {
            if(girds[i].childCount == 0)
                continue;

            girds[i].FindChild("Item(Clone)").GetComponent<ItemUI>().UpdateItemName(gridItemList[j].Value.Name + "\n" + "<size=14><color=green>" + gridItemList[j].Value.ItemType + "</color></size>");
            j++;
        }     
    }
}
