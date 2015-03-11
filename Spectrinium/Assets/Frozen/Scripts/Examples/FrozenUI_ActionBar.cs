using UnityEngine;
using System.Collections;

public class FrozenUI_ActionBar : MonoBehaviour {

	public FrozenUI_Spells spellDatabase;
	public FrozenUI_SpellSlot[] slots;

	void Start()
	{
		if (this.spellDatabase == null)
		{
			this.enabled = false;
			return;
		}

		// Assign example spells
		this.AssignSpellSlot(0, spellDatabase.Get(0));
		this.AssignSpellSlot(1, spellDatabase.Get(1));
		this.AssignSpellSlot(2, spellDatabase.Get(2));
		this.AssignSpellSlot(3, spellDatabase.Get(4));
		this.AssignSpellSlot(4, spellDatabase.Get(5));
	}

	private void AssignSpellSlot(int slotIndex, FrozenUI_SpellInfo spellInfo)
	{
		if (slots[slotIndex] != null)
			slots[slotIndex].Assign(spellInfo);
	}
}
