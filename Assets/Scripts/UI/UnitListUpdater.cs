using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UnitListUpdater : MonoBehaviour
{


  [SerializeField]
  public GameObject itemPrefab;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    var units = GameManager.instance.GetPlayerEntities();
    var unitIds = units.Select(p => p.Id);


    var uiItems = GetComponentsInChildren<UnitDisplay>().Select(u => u.data);
    var uiItemIds = uiItems.Select(p => p.Id);

    foreach (var item in units)
    {
      if (!uiItemIds.Contains(item.Id))
      {
        var newItem = Instantiate(itemPrefab);
        newItem.GetComponent<UnitDisplay>().data = item;
        newItem.transform.parent = gameObject.transform;
      }
    }

    foreach (var item in uiItems)
    {
      if (!unitIds.Contains(item.Id))
      {
        Destroy(item.gameObject);
      }
    }

  }
}
