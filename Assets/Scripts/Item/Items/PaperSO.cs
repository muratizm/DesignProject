using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Paper", menuName = "Inventory/Item/Paper")]
public class PaperSO : ItemSO
{
    [SerializeField] private GameObject _paperItemUI;
    public override void Use()
    {
        GameObject map = Instantiate(_paperItemUI);
        map.transform.SetParent(GameObject.Find("Canvas").transform, false);
        map.SetActive(true);
        map.tag = Constants.Tags.CLOSEABLE_BY_ESC_TAG;
    }
}