 using UnityEngine;
using System.Collections;
 using System.Linq;

public class GridPanelUI : MonoBehaviour
{
    public static GridPanelUI Instance {
        get { return _Instance; }
    }

    private static GridPanelUI _Instance;

    public  Transform[] m_Grids;

    void Awake()
    {
        _Instance = this;
    }

    public  Transform GetEmptyGrid()
    {
        for (var i = 0; i < m_Grids.Length; i++)
            {
                if (m_Grids[i].childCount == 0)
                    return m_Grids[i];
            }
        
            return null;  
    }
}
