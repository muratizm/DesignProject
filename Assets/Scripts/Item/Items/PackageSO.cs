using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Package", menuName = "Inventory/Item/Package")]
public class PackageSO : ItemSO
{
    [SerializeField] private GameObject _packageItemUI;
    public override void Use()
    {

        GameObject map = Instantiate(_packageItemUI);
        map.transform.SetParent(GameObject.Find("Canvas").transform, false);
        map.SetActive(true);
        map.tag = Constants.Tags.CLOSEABLE_BY_ESC_TAG;
    }
}