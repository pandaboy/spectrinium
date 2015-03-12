using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
#if UNITY_3_5
[CustomEditor(typeof(UIIconButton))]
#else
[CustomEditor(typeof(UIIconButton), true)]
#endif
public class UIIconButtonInspector : UIButtonExtendedInspector
{
	protected override void DrawProperties()
	{
		UIIconButton btn = target as UIIconButton;
		
		NGUIEditorTools.DrawProperty("Icon Type", serializedObject, "iconType");
		
		// Check for change in the icon type
		SerializedProperty sp = serializedObject.FindProperty("iconType");
		UIIconButton.IconType type = (UIIconButton.IconType)sp.enumValueIndex;
		
		// If changed, let's set the type manually before the serialized object is updated
		// That way we'll update the sprite visually in the editor
		if (type != btn.iconType)
			btn.SetIconType(type);

		base.DrawProperties();
	}
}