using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMgr : MonoBehaviour 
{

	public class InvItem
	{
		public int unique_id;
		public int type;
		public string name;
		public Image icon;
	}

	Dictionary<int, InvItem> _inv_items = new Dictionary<int, InvItem>();

	void Start () 
	{
		_inv_items.Clear ();
		LoadItemDB ();
	}
	
	void Update () 
	{
		
	}

	private void LoadItemDB()
	{
//		for (int i = 0; i < MAX_ITEM_COUNT; i++) 
//		{
//			InvItem item = new InvItem ();
//			item.unique_id = item_key;
//			item.type = item_type;
//			item.name = item_name;
//			item.icon = item_icon;
//			_inv_items.Add (item.unique_id, item);
//		}
	}

}
