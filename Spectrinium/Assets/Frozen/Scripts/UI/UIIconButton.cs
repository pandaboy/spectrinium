using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIIconButton : UIButtonExtended {

	public enum IconType : int
	{
		World 		= 0,
		Bell 		= 1,
		Diamond 	= 2,
		Drop		= 3,
		Fist		= 4,
		Flag		= 5,
		Hammer		= 6,
		Heart		= 7,
		Lab			= 8,
		Locket		= 9,
		Museum		= 10,
		Network		= 11,
		Profile		= 12,
		Puzzle		= 13,
		Save		= 14,
		Settings	= 15,
		Star		= 16,
		Storm		= 17,
		Swords		= 18,
		Tune		= 19,
		Pinner		= 20,
	}

	public static readonly Dictionary<IconType, string> iconSprites = new Dictionary<IconType, string>()
	{
		{ IconType.World, 		"IconButton_Icon_World" },
		{ IconType.Bell, 		"IconButton_Icon_Bell" },
		{ IconType.Diamond, 	"IconButton_Icon_Diamond" },
		{ IconType.Drop, 		"IconButton_Icon_Drop" },
		{ IconType.Fist, 		"IconButton_Icon_Fist" },
		{ IconType.Flag, 		"IconButton_Icon_Flag" },
		{ IconType.Hammer, 		"IconButton_Icon_Hammer" },
		{ IconType.Heart, 		"IconButton_Icon_Heart" },
		{ IconType.Lab, 		"IconButton_Icon_Lab" },
		{ IconType.Locket, 		"IconButton_Icon_Locket" },
		{ IconType.Museum, 		"IconButton_Icon_Museum" },
		{ IconType.Network, 	"IconButton_Icon_Network" },
		{ IconType.Profile, 	"IconButton_Icon_Profile" },
		{ IconType.Puzzle, 		"IconButton_Icon_Puzzle" },
		{ IconType.Save, 		"IconButton_Icon_Save" },
		{ IconType.Settings, 	"IconButton_Icon_Settings" },
		{ IconType.Star, 		"IconButton_Icon_Star" },
		{ IconType.Storm, 		"IconButton_Icon_Storm" },
		{ IconType.Swords, 		"IconButton_Icon_Swords" },
		{ IconType.Tune, 		"IconButton_Icon_Tune" },
		{ IconType.Pinner, 		"IconButton_Icon_Pinner" },
	};


	public static Vector3 defaultOffset = new Vector3(1f, -1f, 0f);
	public static readonly Dictionary<IconType, Vector3> offsets = new Dictionary<IconType, Vector3>()
	{
		{ IconType.Bell, 	new Vector3(1f, 0f, 0f) },
		{ IconType.Drop, 	new Vector3(0f, -1f, 0f) },
		{ IconType.Profile, new Vector3(0f, -1f, 0f) },
		{ IconType.Puzzle, 	new Vector3(0f, 0f, 0f) },
	};

	public IconType iconType = IconType.World;

	protected override void OnInit()
	{
		base.OnInit();

		this.UpdateIconSprite();
	}

	public void SetIconType(IconType type)
	{
		// Set the type
		this.iconType = type;

		// Update the sprite
		this.UpdateIconSprite();
	}

	public void UpdateIconSprite()
	{
		// Make sure we have that type in the sprites dictionary
		if (!iconSprites.ContainsKey(this.iconType))
			return;

		if (this.additionalTarget != null && this.additionalTarget is UISprite)
		{
			// Apply the sprite name
			UISprite sprite = this.additionalTarget as UISprite;
			sprite.spriteName = iconSprites[this.iconType];
			sprite.MakePixelPerfect();

			// Apply offset
			sprite.cachedTransform.localPosition = (offsets.ContainsKey(this.iconType)) ? offsets[this.iconType] : defaultOffset;
		}
	}
}
