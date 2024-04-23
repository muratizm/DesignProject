using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Inventory/Item/Map")]
public class MapSO : ItemSO
{
    [SerializeField] private GameObject _mapItemUI;
    public override void Use()
    {
        GameObject map = Instantiate(_mapItemUI);
        map.transform.SetParent(GameObject.Find("Canvas").transform, false);
        map.SetActive(true);
        map.tag = Constants.Tags.CLOSEABLE_BY_ESC_TAG;
    }
}