using UnityEngine;
using System.Collections;

public class GUIBarManager : MonoBehaviour {

    private ExperienceSystem WaveLength_bar;
    private int last_level = 1;

    private HealthSystem health_bar;
    private ManaSystem healths_bar;

    private GlobeBarSystem Spectium_test_bar;

    public Rect HealthBarDimens;
    public bool VerticleHealthBar;
    public Texture HealthBubbleTexture;
    public Texture HealthTexture;
    public float HealthBubbleTextureRotation;

    public Rect WaveLengthBarDimens;
    public bool VerticleWaveLength;
    public Texture WaveLengthBubbleTexture;
    public Texture WaveLengthTexture;
    public float WaveLengthTextureRotation;

    public Rect HealthBarDmension;
    public Rect HealthBarScrollerDimension;
    public bool VerticleHealthsBar;
    public Texture HealthsBubbleTexture;
    public Texture HealthsTexture;
    public float HealthsBubbleTextureRotation;

    public Rect SpectiumBarDimens;
    public bool VerticleSpectium;
    public Texture SpectiumBubbleTexture;
    public Texture SpectiumTexture;
    public float SpectiumBubbleTextureRotation;
	
	void Start () {
        health_bar = new HealthSystem(HealthBarDimens, VerticleHealthBar, HealthBubbleTexture, HealthTexture, HealthBubbleTextureRotation);
        WaveLength_bar = new ExperienceSystem(WaveLengthBarDimens, VerticleWaveLength, WaveLengthBubbleTexture, WaveLengthTexture, WaveLengthTextureRotation);
        healths_bar = new ManaSystem(HealthBarDmension, HealthBarScrollerDimension, VerticleHealthsBar, HealthsBubbleTexture, HealthsTexture, HealthsBubbleTextureRotation);
        Spectium_test_bar = new GlobeBarSystem(SpectiumBarDimens, VerticleSpectium, SpectiumBubbleTexture, SpectiumTexture, SpectiumBubbleTextureRotation);

        WaveLength_bar.Initialize();
        health_bar.Initialize();
        healths_bar.Initialize();
        Spectium_test_bar.Initialize();
	}

    public void OnGUI()
    {
        //health_bar.DrawBar();

        WaveLength_bar.DrawBar();

        healths_bar.DrawBar();

        Spectium_test_bar.DrawBar();

        // Edit from other like spectium
        //if (GUI.Button(new Rect(health_bar.getScrollBarRect().x + (health_bar.getScrollBarRect().width / 2) - (128 / 2), health_bar.getScrollBarRect().y + (health_bar.getScrollBarRect().height / 2) - 30, 128, 20), "Increase Health"))
        //{
        //    health_bar.IncrimentBar(Random.Range(1, 6));
        //}
        //else if (GUI.Button(new Rect(health_bar.getScrollBarRect().x + (health_bar.getScrollBarRect().width / 2) - (128 / 2), health_bar.getScrollBarRect().y + (health_bar.getScrollBarRect().height / 2) + (20 / 2), 128, 20), "Decrease Health"))
        //{
        //    health_bar.IncrimentBar(Random.Range(-6, -1));
        //}

        if (GUI.Button(new Rect(healths_bar.getScrollBarRect().x + (healths_bar.getScrollBarRect().width / 2) - (128 / 2), healths_bar.getScrollBarRect().y + (healths_bar.getScrollBarRect().height / 2) - 50, 128, 20), "Increase Health"))
        {
            healths_bar.IncrimentBar(Random.Range(1, 12));
        }
        else if (GUI.Button(new Rect(healths_bar.getScrollBarRect().x + (healths_bar.getScrollBarRect().width / 2) - (128 / 2), healths_bar.getScrollBarRect().y + (healths_bar.getScrollBarRect().height / 2) + 30, 128, 20), "Decrease Health"))
        {
            healths_bar.IncrimentBar(Random.Range(-12, -1));
        }

        if (GUI.Button(new Rect(WaveLength_bar.getScrollBarRect().x + (WaveLength_bar.getScrollBarRect().width / 2) - (132 / 2), WaveLength_bar.getScrollBarRect().y + (WaveLength_bar.getScrollBarRect().height / 2) - 50, 132, 20), "Incrase WaveLength"))
        {
            WaveLength_bar.IncrimentBar(Random.Range(1, 12));
        }
        else if (GUI.Button(new Rect(WaveLength_bar.getScrollBarRect().x + (WaveLength_bar.getScrollBarRect().width / 2) - (140 / 2), WaveLength_bar.getScrollBarRect().y + (WaveLength_bar.getScrollBarRect().height / 2) + 30, 140, 20), "Decrease WaveLength"))
        {
            WaveLength_bar.IncrimentBar(Random.Range(-12, -1));
        }

        if (GUI.Button(new Rect(Spectium_test_bar.getScrollBarRect().x + (Spectium_test_bar.getScrollBarRect().width / 2) - (140 / 2), Spectium_test_bar.getScrollBarRect().y + (Spectium_test_bar.getScrollBarRect().height / 2) - 85, 140, 20), "Increase Spectrinium"))
        {
            Spectium_test_bar.IncrimentBar(Random.Range(1, 12));
        }
        else if (GUI.Button(new Rect(Spectium_test_bar.getScrollBarRect().x + (Spectium_test_bar.getScrollBarRect().width / 2) - (145 / 2), Spectium_test_bar.getScrollBarRect().y + (Spectium_test_bar.getScrollBarRect().height / 2) + 70, 145, 20), "Decrease Spectrinium"))
        {
            Spectium_test_bar.IncrimentBar(Random.Range(-12, -1));
        }
    }
	
	
	void Update () {
        WaveLength_bar.Update();

        if (WaveLength_bar.getLevel() - last_level >= 1)
        {
            // level changed: change stuff here
            //Debug.Log("DING! You Are Now Level " + exp_bar.getLevel());

            last_level = WaveLength_bar.getLevel();
        }

        health_bar.Update();

        healths_bar.Update();

        Spectium_test_bar.Update();
	}
}
