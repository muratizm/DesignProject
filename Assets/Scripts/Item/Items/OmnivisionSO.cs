using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Omnivison", menuName = "Inventory/Item/Omnivision")]
public class OmnivisionSO : ItemSO
{
    private Camera minimapCamera;
    [SerializeField] private float maxSize = 250f;



    public override void Use()
    {
        Debug.Log("Showing us everywhere!");
        ItemOperations.Instance.UseOmniverseItem(maxSize);
    }


}