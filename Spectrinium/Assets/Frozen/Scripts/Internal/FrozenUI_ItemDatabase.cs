using UnityEngine;
using System.Collections;

public class FrozenUI_ItemDatabase : ScriptableObject {

	public FrozenUI_ItemInfo[] items;
	
	/// <summary>
	/// Get the specified ItemInfo by index.
	/// </summary>
	/// <param name="index">Index.</param>
	public FrozenUI_ItemInfo Get(int index)
	{
		return (this.items[index]);
	}
	
	/// <summary>
	/// Gets the specified ItemInfo by ID.
	/// </summary>
	/// <returns>The ItemInfo or NULL if not found.</returns>
	/// <param name="ID">The item ID.</param>
	public FrozenUI_ItemInfo GetByID(int ID)
	{
		for (int i = 0; i < this.items.Length; i++)
		{
			if (this.items[i].ID == ID)
				return this.items[i];
		}
		
		return null;
	}
}