using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(FrozenUI_OnOffSwitch))]
public class FrozenUI_OnOffSwitchInspector : Editor
{
	private FrozenUI_OnOffSwitch mSwitch;

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		NGUIEditorTools.SetLabelWidth(120f);
		mSwitch = target as FrozenUI_OnOffSwitch;

		EditorGUILayout.Space();

		NGUIEditorTools.DrawProperty("UI Sprite", serializedObject, "targetSprite");

		if (mSwitch.targetSprite != null)
		{
			EditorGUILayout.Space();

			if (NGUIEditorTools.DrawHeader("Appearance"))
			{
				NGUIEditorTools.BeginContents();

				SerializedObject obj = new SerializedObject(mSwitch.targetSprite);
				obj.Update();
				SerializedProperty atlas = obj.FindProperty("mAtlas");

				if (atlas != null)
				{
					EditorGUILayout.Space();
					NGUIEditorTools.DrawSpriteField("On Sprite", obj, atlas, obj.FindProperty("mSpriteName"));
					NGUIEditorTools.DrawSpriteField("Off Sprite", serializedObject, atlas, serializedObject.FindProperty("offSprite"), true);
					NGUIEditorTools.DrawProperty("Make Pixel Perfect", serializedObject, "makePixelPerfect");
				}

				obj.ApplyModifiedProperties();
				NGUIEditorTools.EndContents();
			}
		}

		serializedObject.ApplyModifiedProperties();
	}
}
