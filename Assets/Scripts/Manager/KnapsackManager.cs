using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LitJson;
using UnityEditor.VersionControl;
using UnityEngine.UI;

public class KnapsackManager : MonoBehaviour
{
    private static KnapsackManager _Instance;
    public static KnapsackManager Instance { get { return _Instance;}}

    public Image m_SelecedImage;

    public GridPanelUI  m_GridPanelUI;
    public TooltipUI    m_TooltipUI;
    public DragItemUI   m_DragItemUI;

    public Dictionary<int, Item> m_ItemList;

    private bool isDrag = false;
    private bool isShow = false;

    void Awake()
    {
        _Instance = this;

        Load();

        GridUI.OnEnter += GridUI_OnEnter;
        GridUI.OnExit += GridUI_OnExit;
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;
        GridUI.OnClick += GridUI_OnClick;

        LoadJson();
    }

    //加载一些背包中的对象
    void Load()
    {
        m_ItemList = new Dictionary<int, Item>();

        Weapon w1 = new Weapon(0, "牛刀", "牛B的刀！", 20, 10, "", 100);
        Weapon w2 = new Weapon(1, "羊刀", "杀羊刀。", 15, 10, "", 20);
        Weapon w3 = new Weapon(2, "宝剑", "大宝剑！", 120, 50, "", 500);
        Weapon w4 = new Weapon(3, "军枪", "可以对敌人射击，很厉害的一把枪。", 1500, 125, "", 720);

        Consumable c1 = new Consumable(4, "红瓶", "加血", 25, 11, "", 20, 0);
        Consumable c2 = new Consumable(5, "蓝瓶", "加蓝", 39, 19, "", 0, 20);

        Armor a1 = new Armor(6, "头盔", "保护脑袋！", 128, 83, "", 5, 40, 1);
        Armor a2 = new Armor(7, "护肩", "上古护肩，锈迹斑斑。", 1000, 0, "", 15, 40, 11);
        Armor a3 = new Armor(8, "胸甲", "皇上御赐胸甲。", 153, 0, "", 25, 30, 11);
        Armor a4 = new Armor(9, "护腿", "预防风寒，从腿做起", 999, 60, "", 19, 30, 51);

        m_ItemList.Add(w1.Id, w1);
        m_ItemList.Add(w2.Id, w2);
        m_ItemList.Add(w3.Id, w3);
        m_ItemList.Add(w4.Id, w4);
        m_ItemList.Add(c1.Id, c1);
        m_ItemList.Add(c2.Id, c2);
        m_ItemList.Add(a1.Id, a1);
        m_ItemList.Add(a2.Id, a2);
        m_ItemList.Add(a3.Id, a3);
        m_ItemList.Add(a4.Id, a4);  
    }

    void LoadJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/" + "json.txt");

        var gridItemDic = new SortedDictionary<string,Item>();

        gridItemDic = JsonMapper.ToObject<SortedDictionary<string, Item>>(json);

        var jsonData = JsonMapper.ToObject(json);

        var keys = jsonData.Keys;

        foreach (var key in keys)
        {
            switch (jsonData[key]["ItemType"].ToString())
            {
                case "Armor":
                    Armor armor = new Armor(int.Parse(jsonData[key]["Id"].ToString()),jsonData[key]["Name"].ToString(),jsonData[key]["Description"].ToString()
                                            , int.Parse(jsonData[key]["BuyPrice"].ToString()), int.Parse(jsonData[key]["SellPrice"].ToJson()),
                                            jsonData[key]["Icon"].ToString(), int.Parse(jsonData[key]["Power"].ToString()),
                                            int.Parse(jsonData[key]["Defend"].ToString()), int.Parse(jsonData[key]["Agility"].ToString()));

                    this.CreateNewItem(armor,GameObject.Find(key).transform);

                    break;
                case "Consumable":

                    Consumable consuable = new Consumable(
                        int.Parse(jsonData[key]["Id"].ToString()), jsonData[key]["Name"].ToString(), jsonData[key]["Description"].ToString()
                                            , int.Parse(jsonData[key]["BuyPrice"].ToString()), int.Parse(jsonData[key]["SellPrice"].ToJson()),
                                            jsonData[key]["Icon"].ToString(), int.Parse(jsonData[key]["Hp"].ToString()), int.Parse(jsonData[key]["Mp"].ToString())
                        );

                    this.CreateNewItem(consuable, GameObject.Find(key).transform);

                    break;
                case "Weapon":

                    Weapon weapon = new Weapon(
                                int.Parse(jsonData[key]["Id"].ToString()), jsonData[key]["Name"].ToString(), jsonData[key]["Description"].ToString()
                                            , int.Parse(jsonData[key]["BuyPrice"].ToString()), int.Parse(jsonData[key]["SellPrice"].ToJson()),
                                            jsonData[key]["Icon"].ToString(),int.Parse(jsonData[key]["Damage"].ToString())
                          );

                    this.CreateNewItem(weapon, GameObject.Find(key).transform);

                    break;
                default:
                    break;
            }
        }
    }

	void Update ()
	{
	    Vector2 position;
	    RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("KnapsackUI").transform as RectTransform,
	        Input.mousePosition, null, out position);

	    if (isDrag)
	    {
            m_DragItemUI.Show();
            m_DragItemUI.SetLocalPosition(position);
	    }
	    else if(isShow)
	    {
            m_TooltipUI.Show();
            m_TooltipUI.SetLocalPosition(position);
	    }
	}

    //获得提示板上要显示的内容
    string GetTooltipText(Item item)
    {
        if (item == null) return string.Empty;

        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("<color=red>{0}</color>\n\n", item.Name);

        switch (item.ItemType)
        {
            case "Armor":
                Armor armor = item as Armor;
                if(armor != null)
                    sb.AppendFormat("力量：{0}\n防御：{1}\n敏捷：{2}\n\n", armor.Power, armor.Defend, armor.Agility);
                break;
            case "Consumable":
                Consumable consumable = item as Consumable;
                if(consumable != null)
                    sb.AppendFormat("HP:{0}\nMP:{1}\n\n", consumable.Hp, consumable.Mp);
                break;
            case "Weapon":
                Weapon weapon = item as Weapon;
                if(weapon != null)
                    sb.AppendFormat("攻击：{0}\n\n", weapon.Damage);
                break;
            default:
                break;
        }

        sb.AppendFormat(
            "<size=25><color=white>购买价格：{0}\n出售价格：{1}\n</color></size><color=yellow><size=20>描述：{2}</size></color>",
            item.BuyPrice, item.SellPrice, item.Description);

        return sb.ToString();
    } 

    //添加一个Item
    public void StoreItem(int itemId)
    {
        if (!m_ItemList.ContainsKey(itemId)) return;

        Transform emptyGrid = m_GridPanelUI.GetEmptyGrid();

        if (emptyGrid == null)
        {
            Debug.LogError("背包已满");
            return;
        }

        Item currItem = m_ItemList[itemId];

        this.CreateNewItem(currItem,emptyGrid);  
    }

    //创建一个Item，使用Resources加载
    void CreateNewItem(Item item, Transform parentTransform)
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");

        itemPrefab.GetComponent<ItemUI>().UpdateItemName(item.Name + "\n" + "<size=14><color=green>" + item.ItemType + "</color></size>");

        GameObject itemGO = GameObject.Instantiate(itemPrefab);

        itemGO.transform.SetParent(parentTransform);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localScale = Vector3.one;

        ItemModel.StoreItem(parentTransform.name, item);
    }

    #region Unity事件触发的方法

    /// <summary>
    /// 点击事件，实现选择框的显示
    /// </summary>
    /// <param name="gridTransform">点击的Item的rectTransform</param>
    void GridUI_OnClick(Transform gridTransform)
    {
        if (gridTransform.tag == "Grid" && gridTransform.childCount != 0)
        {
            m_SelecedImage.gameObject.SetActive(true);
            m_SelecedImage.transform.SetParent(gridTransform);
            m_SelecedImage.transform.localPosition = Vector3.zero;
            m_SelecedImage.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// 触发了OnEnter方法时，实现提示板的显示
    /// </summary>
    /// <param name="gridTransform">触发的Item的位置</param>
    void GridUI_OnEnter(Transform gridTransform)
    {
        Item item = ItemModel.GetItem(gridTransform.name);

        if (item == null) return;

        string content = GetTooltipText(item);
        m_TooltipUI.UpdateToolTip(content);

        isShow = true;
    }

    /// <summary>
    /// 触发事件结束时，提示板隐藏
    /// </summary>
    void GridUI_OnExit()
    {
        isShow = false;
        m_TooltipUI.Hide();
    }

    /// <summary>
    /// 拖动事件，实现了当前事件下的Item的销毁
    /// </summary>
    /// <param name="gridTransform">触发当前事件的Item的rectTransform</param>
    void GridUI_OnLeftBeginDrag(Transform gridTransform)
    {
        m_SelecedImage.gameObject.SetActive(false);
        m_SelecedImage.transform.SetParent(null);

        if (gridTransform.childCount == 0)
        {
            return;
        }
        else
        {
            isDrag = true;

            Item item = ItemModel.GetItem(gridTransform.name);

            m_DragItemUI.UpdateItemName(item.Name);

            Destroy(gridTransform.FindChild("Item(Clone)").gameObject);
        }  
    }

    /// <summary>
    /// 结束拖拽，实现物品的交换或者恢复原位置
    /// </summary>
    /// <param name="prevTransform"></param>
    /// <param name="enterTransform"></param>
    void GridUI_OnLeftEndDrag(Transform prevTransform, Transform enterTransform)
    {
        isDrag = false;

        m_DragItemUI.Hide();

        if (enterTransform == null) //拖到其他位置，丢弃
        {
            ItemModel.DeleteItem(prevTransform.name);
            Debug.Log("物品已扔");
        }
        else if (enterTransform.tag == "Grid")      //和别的网格交换或者自己交换
        {
            if (prevTransform == enterTransform)    //和自己交换
            {
                Debug.Log("果然是同一位置！");

                Item item = ItemModel.GetItem(prevTransform.name);

                if (item != null)
                {
                    this.CreateNewItem(item, prevTransform);
                }
            }
            else        //和别的网格交换
            {
                if (enterTransform.childCount == 0) //如果拖拽到的位置为空
                {
                    Item item = ItemModel.GetItem(prevTransform.name);

                    //保证拖拽的网格内部有物体，如果没有，则不会创建新的物体
					if (item == null)   return;

                    ItemModel.DeleteItem(prevTransform.name);
                    this.CreateNewItem(item,enterTransform);
                }
                else   //有物体
                {
                    Destroy(enterTransform.FindChild("Item(Clone)").gameObject);

                    Item curItem = ItemModel.GetItem(prevTransform.name);
                    Item nextItem = ItemModel.GetItem(enterTransform.name);

                    this.CreateNewItem(curItem, enterTransform);
                    this.CreateNewItem(nextItem, prevTransform);
                }
            }
        }
        else //拖到别的位置，恢复到自身状态
        {
            Item item = ItemModel.GetItem(prevTransform.name);

            if(item != null)
                this.CreateNewItem(item,prevTransform);
        }
    }

    #endregion


    void OnApplicationQuit()
    {
        var json = JsonMapper.ToJson(ItemModel.m_GridItem);
        File.WriteAllText(Application.dataPath + "/" + "json.txt", json);
    }
}
