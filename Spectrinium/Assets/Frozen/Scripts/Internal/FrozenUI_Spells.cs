using UnityEngine;
using System.Collections;

public class FrozenUI_Spells : ScriptableObject {

	public FrozenUI_SpellInfo[] spells;

	/// <summary>
	/// Get the specified SpellInfo by index.
	/// </summary>
	/// <param name="index">Index.</param>
	public FrozenUI_SpellInfo Get(int index)
	{
		return (spells[index]);
	}

	/// <summary>
	/// Gets the specified SpellInfo by ID.
	/// </summary>
	/// <returns>The SpellInfo or NULL if not found.</returns>
	/// <param name="ID">The spell ID.</param>
	public FrozenUI_SpellInfo GetByID(int ID)
	{
		for (int i = 0; i < this.spells.Length; i++)
		{
			if (this.spells[i].ID == ID)
				return this.spells[i];
		}

		return null;
	}
}