using UnityEngine;
using System.Collections;

public class FrozenUI_Assign_ItemSlot : MonoBehaviour {
	
	public FrozenUI_ItemSlot slot;
	public FrozenUI_ItemDatabase itemDatabase;
	public int assignItem;
	
	void Start()
	{
		if (this.slot == null)
			this.slot = this.GetComponent<FrozenUI_ItemSlot>();
		
		if (this.slot == null || this.itemDatabase == null)
		{
			this.Destruct();
			return;
		}
		
		this.slot.Assign(this.itemDatabase.GetByID(this.assignItem));
		this.Destruct();
	}
	
	private void Destruct()
	{
		DestroyImmediate(this);
	}
}
