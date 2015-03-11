using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UISprite))]
public class FrozenUI_SpellSlot_LevelBar : MonoBehaviour {

	private const int fillSpriteHeight = 8;
	private const string fillSprite = "MobaActionBar_SpellSlot_LevelBar_Fill";
	private const string separatorSprite = "MobaActionBar_SpellSlot_LevelBar_Separator";

	private UISprite bar;
	public RectOffset padding;

	[SerializeField] private int mMaxLevel = 4;

	public int MaxLevel {
		get { return this.mMaxLevel; }
		set {
			this.mMaxLevel = value;
			this.UpdateMaxLevel();
		}
	}

	public int currentLevel = 1;

	private List<Transform> mSeparators = new List<Transform>();
	private Dictionary<int, UISprite> mFillBars = new Dictionary<int, UISprite>();

	void Start()
	{
		this.bar = this.GetComponent<UISprite>();
		this.UpdateSeparators();
		this.UpdateCurrentLevel();
	}

	[ContextMenu("Update current level")]
	public void UpdateCurrentLevel()
	{
		this.SetLevel(this.currentLevel);
	}

	[ContextMenu("Update max level")]
	private void UpdateMaxLevel()
	{
		this.UpdateSeparators();

		if (this.currentLevel > this.MaxLevel)
			this.currentLevel = this.MaxLevel;

		this.UpdateCurrentLevel();
	}

	public int FillBarWidth()
	{
		int width = (this.bar.width - this.padding.left - this.padding.right - (this.MaxLevel - 1));

		return Mathf.CeilToInt((float)width / (float)this.MaxLevel);
	}

	public void UpdateSeparators()
	{
		// Cleanup the separators
		foreach (Transform trans in this.mSeparators)
		{
			if (trans != null)
				DestroyImmediate(trans.gameObject);
		}

		// Clear the list 
		this.mSeparators.Clear();

		// Remove all the current bars
		foreach (KeyValuePair<int, UISprite> pair in this.mFillBars)
		{
			if (pair.Value != null)
				DestroyImmediate(pair.Value.gameObject);
		}

		// Clear the list
		this.mFillBars.Clear();

		// Get the size of a fill bar
		int fillWidth = this.FillBarWidth();

		// Starting offset for placing new fill bars
		int currentOffset = this.padding.left;

		// Place the separators
		for (int i = 0; i < (this.MaxLevel - 1); i++)
		{
			// Add the fill bar width to the offset
			currentOffset += fillWidth;

			UISprite separator = this.CreateSeparator();
			if (separator != null)
			{
				separator.transform.localPosition = new Vector3(currentOffset, -2, 0f);
				
				// Add to the offset
				currentOffset += 1;
			}

			// Add the separator to the list
			this.mSeparators.Add(separator.transform);
		}
	}

	public void UpdateFillBars()
	{
		// Get the size of a fill bar
		int fillWidth = this.FillBarWidth();
		int space = this.bar.width - this.padding.right;

		// Update the fill bar size and position
		foreach (KeyValuePair<int, UISprite> pair in this.mFillBars)
		{
			UISprite fillBar = pair.Value;
			
			if (fillBar != null)
			{
				fillBar.width = fillWidth;
				fillBar.transform.localPosition = new Vector3((this.padding.left + (pair.Key * (fillWidth + 1))), -this.padding.top, 0f);

				int topRightCorner = ((int)fillBar.transform.localPosition.x + fillBar.width);

				// Check if the bar is too wide
				if (topRightCorner > space)
					fillBar.width = fillWidth - (topRightCorner - space);

				fillBar.Update();
			}
		}
	}

	public void SetLevel(int level)
	{
		if (level < 0 || level > this.MaxLevel)
			return;

		// Cleanup unnecessary bars
		if (this.mFillBars.Count > level)
		{
			for (int i = level; i < this.MaxLevel; i++)
			{
				if (this.mFillBars.ContainsKey(i))
				{
					UISprite fillBar = this.mFillBars[i];

					if (fillBar != null)
						Destroy(fillBar.gameObject);

					this.mFillBars.Remove(i);
				}
			}
		}

		// Place fill bars if unnecessary
		if (this.mFillBars.Count < level)
		{
			for (int i = this.mFillBars.Count; i < level; i++)
			{
				UISprite fillBar = this.CreateFillBar();

				if (fillBar == null)
				{
					Debug.LogWarning(this.GetType() + " failed to create fill bar sprite!", this);
					break;
				}

				// Add to the list
				this.mFillBars.Add(i, fillBar);
			}
		}

		// Update the fill bars size and position
		this.UpdateFillBars();

		// Save the level as current
		this.currentLevel = level;
	}

	private UISprite CreateFillBar()
	{
		GameObject obj = new GameObject("Fill Bar");

		// Apply layer
		obj.layer = this.gameObject.layer;

		// Add sprite component
		obj.AddComponent<UISprite>();

		UISprite sprite = obj.GetComponent<UISprite>();

		if (sprite == null)
		{
			DestroyImmediate(obj);
			return null;
		}

		sprite.pivot = UIWidget.Pivot.TopLeft;
		sprite.type = UISprite.Type.Sliced;
		sprite.depth = this.bar.depth + 1;

		sprite.atlas = this.bar.atlas;
		sprite.spriteName = fillSprite;
		sprite.MakePixelPerfect();

		sprite.height = fillSpriteHeight;

		// Change the parent
		obj.transform.parent = this.transform;

		// Fix scale
		obj.transform.localScale = new Vector3(1f, 1f, 1f);

		return sprite;
	}

	private UISprite CreateSeparator()
	{
		GameObject obj = new GameObject("Separator");
		
		// Apply layer
		obj.layer = this.gameObject.layer;
		
		// Add sprite component
		obj.AddComponent<UISprite>();
		
		UISprite sprite = obj.GetComponent<UISprite>();
		
		if (sprite == null)
		{
			DestroyImmediate(obj);
			return null;
		}
		
		sprite.pivot = UIWidget.Pivot.TopLeft;
		sprite.type = UISprite.Type.Simple;
		sprite.depth = this.bar.depth + 1;
		
		sprite.atlas = this.bar.atlas;
		sprite.spriteName = separatorSprite;
		sprite.MakePixelPerfect();

		// Change the parent
		obj.transform.parent = this.transform;
		
		// Fix scale
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		
		return sprite;
	}
}
