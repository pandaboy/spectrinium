using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(FrozenUI_SpellSlot))]
public class FrozenUI_SpellSlotInspector : FrozenUI_IconSlotInspector
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		EditorGUIUtility.labelWidth = 120f;
		FrozenUI_SpellSlot mSlot = target as FrozenUI_SpellSlot;

		EditorGUILayout.Space();

		FrozenUI_Cooldown cooldown = EditorGUILayout.ObjectField("Cooldown Handle", mSlot.cooldownHandle, typeof(FrozenUI_Cooldown), true) as FrozenUI_Cooldown;
		
		if (mSlot.cooldownHandle != cooldown)
			mSlot.cooldownHandle = cooldown;
	}
}

