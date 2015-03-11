using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Frozen UI/Item Slot")]
public class FrozenUI_ItemSlot : FrozenUI_IconSlot
{
	private FrozenUI_ItemInfo itemInfo;
	
	public override void OnStart()
	{
	}
	
	/// <summary>
	/// Gets the ItemInfo of the item assigned to this slot.
	/// </summary>
	/// <returns>The item info.</returns>
	public FrozenUI_ItemInfo GetItemInfo()
	{
		return itemInfo;
	}
	
	/// <summary>
	/// Determines whether this slot is assigned.
	/// </summary>
	/// <returns><c>true</c> if this instance is assigned; otherwise, <c>false</c>.</returns>
	public override bool IsAssigned()
	{
		return (this.itemInfo != null);
	}
	
	/// <summary>
	/// Assign the slot by item info.
	/// </summary>
	/// <param name="itemInfo">Item info.</param>
	public bool Assign(FrozenUI_ItemInfo itemInfo)
	{
		if (itemInfo == null)
			return false;
		
		// Use the base class assign
		if (this.Assign(itemInfo.Icon))
		{
			// Set the item info
			this.itemInfo = itemInfo;

			// Success
			return true;
		}
		
		return false;
	}
	
	/// <summary>
	/// Assign the slot by the passed source slot.
	/// </summary>
	/// <param name="source">Source.</param>
	public override bool Assign(Object source)
	{
		if (source is FrozenUI_ItemSlot)
		{
			FrozenUI_ItemSlot sourceSlot = source as FrozenUI_ItemSlot;
			
			if (sourceSlot != null)
				return this.Assign(sourceSlot.GetItemInfo());
		}
		
		// Default
		return false;
	}
	
	/// <summary>
	/// Unassign this slot.
	/// </summary>
	public override void Unassign()
	{
		// Remove the icon
		base.Unassign();
		
		// Clear the item info
		this.itemInfo = null;
	}
	
	/// <summary>
	/// Determines whether this slot can swap with the specified target slot.
	/// </summary>
	/// <returns><c>true</c> if this instance can swap with the specified target; otherwise, <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	public override bool CanSwapWith(Object target)
	{
		return (target is FrozenUI_ItemSlot);
	}

	/// <summary>
	/// Raises the click event.
	/// </summary>
	public override void OnClick()
	{
		if (!this.IsAssigned())
			return;
	}
	
	/// <summary>
	/// Raises the tooltip event.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show.</param>
	public override void OnTooltip(bool show)
	{
		if (show && this.IsAssigned())
		{
			// Set the title and description
			FrozenUI_Tooltip.SetTitle(this.itemInfo.Name);
			FrozenUI_Tooltip.SetDescription(this.itemInfo.Description);

			// Item types
			FrozenUI_Tooltip.AddAttribute(this.itemInfo.Type, "");
			FrozenUI_Tooltip.AddAttribute(this.itemInfo.Subtype, "");

			if (this.itemInfo.ItemType == 1)
			{
				FrozenUI_Tooltip.AddAttribute(this.itemInfo.Damage.ToString(), " Damage");
				FrozenUI_Tooltip.AddAttribute(this.itemInfo.AttackSpeed.ToString("0.0"), " Attack speed");

				FrozenUI_Tooltip.AddAttribute_SingleColumn("(" + ((float)this.itemInfo.Damage / this.itemInfo.AttackSpeed).ToString("0.0") + " damage per second)", "");
			}
			else
			{
				FrozenUI_Tooltip.AddAttribute(this.itemInfo.Block.ToString(), " Block");
				FrozenUI_Tooltip.AddAttribute(this.itemInfo.Armor.ToString(), " Armor");
			}

			FrozenUI_Tooltip.AddAttribute_SingleColumn("", "+" + this.itemInfo.Stamina.ToString() + " Stamina", new RectOffset(0, 0, 7, 0));
			FrozenUI_Tooltip.AddAttribute_SingleColumn("", "+" + this.itemInfo.Strength.ToString() + " Strength");

			// Set the tooltip position
			FrozenUI_Tooltip.SetPosition(this.iconSprite as UIWidget);
			
			// Show the tooltip
			FrozenUI_Tooltip.Show();
			
			// Prevent hide
			return;
		}
		
		// Default hide
		FrozenUI_Tooltip.Hide();
	}
}

