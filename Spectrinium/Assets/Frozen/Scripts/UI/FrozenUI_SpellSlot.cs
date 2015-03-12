using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Frozen UI/Spell Slot")]
public class FrozenUI_SpellSlot : FrozenUI_IconSlot
{
	public FrozenUI_Cooldown cooldownHandle;
	private FrozenUI_SpellInfo spellInfo;

	public override void OnStart()
	{
		// Try finding the cooldown handler
		if (this.cooldownHandle == null) this.cooldownHandle = this.GetComponent<FrozenUI_Cooldown>();
		if (this.cooldownHandle == null) this.cooldownHandle = this.GetComponentInChildren<FrozenUI_Cooldown>();
	}

	/// <summary>
	/// Gets the spell info of the spell assigned to this slot.
	/// </summary>
	/// <returns>The spell info.</returns>
	public FrozenUI_SpellInfo GetSpellInfo()
	{
		return spellInfo;
	}

	/// <summary>
	/// Determines whether this slot is assigned.
	/// </summary>
	/// <returns><c>true</c> if this instance is assigned; otherwise, <c>false</c>.</returns>
	public override bool IsAssigned()
	{
		return (this.spellInfo != null);
	}

	/// <summary>
	/// Assign the slot by spell info.
	/// </summary>
	/// <param name="spellInfo">Spell info.</param>
	public bool Assign(FrozenUI_SpellInfo spellInfo)
	{
		if (spellInfo == null)
			return false;

		// Use the base class assign
		if (this.Assign(spellInfo.Icon))
		{
			// Set the spell info
			this.spellInfo = spellInfo;
			
			// Check if we have a cooldown handler
			if (this.cooldownHandle != null)
				this.cooldownHandle.OnAssignSpell(spellInfo);

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
		if (source is FrozenUI_SpellSlot)
		{
			FrozenUI_SpellSlot sourceSlot = source as FrozenUI_SpellSlot;
			
			if (sourceSlot != null)
				return this.Assign(sourceSlot.GetSpellInfo());
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
		
		// Clear the spell info
		this.spellInfo = null;
		
		// Check if we have a cooldown handler
		if (this.cooldownHandle != null)
			this.cooldownHandle.OnUnassign();
	}

	/// <summary>
	/// Determines whether this slot can swap with the specified target slot.
	/// </summary>
	/// <returns><c>true</c> if this instance can swap with the specified target; otherwise, <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	public override bool CanSwapWith(Object target)
	{
		return (target is FrozenUI_SpellSlot);
	}

	/// <summary>
	/// Raises the click event.
	/// </summary>
	public override void OnClick()
	{
		if (!this.IsAssigned())
			return;
		
		// Check if the slot is on cooldown
		if (this.cooldownHandle != null)
		{
			if (this.cooldownHandle.IsOnCooldown)
				return;
			
			this.cooldownHandle.StartCooldown(this.spellInfo.Cooldown);
		}
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
			FrozenUI_Tooltip.SetTitle(this.spellInfo.Name);
			FrozenUI_Tooltip.SetDescription(this.spellInfo.Description);
			
			if (this.spellInfo.Flags.Has(SpellInfo_Flags.Passive))
			{
				FrozenUI_Tooltip.AddAttribute("Passive", "");
			}
			else
			{
				// Power consumption
				if (this.spellInfo.PowerCost > 0f)
				{
					if (this.spellInfo.Flags.Has(SpellInfo_Flags.PowerCostInPct))
						FrozenUI_Tooltip.AddAttribute(this.spellInfo.PowerCost.ToString("0") + "%", " Energy");
					else
						FrozenUI_Tooltip.AddAttribute(this.spellInfo.PowerCost.ToString("0"), " Energy");
				}
				
				// Range
				if (this.spellInfo.Range > 0f)
				{
					if (this.spellInfo.Range == 1f)
						FrozenUI_Tooltip.AddAttribute("Melee range", "");
					else
						FrozenUI_Tooltip.AddAttribute(this.spellInfo.Range.ToString("0"), " yd range");
				}
				
				// Cast time
				if (this.spellInfo.CastTime == 0f)
					FrozenUI_Tooltip.AddAttribute("Instant", "");
				else
					FrozenUI_Tooltip.AddAttribute(this.spellInfo.CastTime.ToString("0.0"), " sec cast");
				
				// Cooldown
				if (this.spellInfo.Cooldown > 0f)
					FrozenUI_Tooltip.AddAttribute(this.spellInfo.Cooldown.ToString("0.0"), " sec cooldown");
			}
			
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

