using UnityEngine;
using System.Collections;

public class FrozenUI_Assign_SpellSlot : MonoBehaviour {

	public FrozenUI_SpellSlot slot;
	public FrozenUI_Spells spellDatabase;
	public int assignSpell;

	void Start()
	{
		if (this.slot == null)
			this.slot = this.GetComponent<FrozenUI_SpellSlot>();

		if (this.slot == null || this.spellDatabase == null)
		{
			this.Destruct();
			return;
		}

		this.slot.Assign(this.spellDatabase.GetByID(this.assignSpell));
		this.Destruct();
	}

	private void Destruct()
	{
		DestroyImmediate(this);
	}
}
