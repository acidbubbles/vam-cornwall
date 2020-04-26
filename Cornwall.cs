using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cornwall : MVRScript
{
    public const float Padding = 0.2f;

    private JSONStorableFloat _intensityJSON;
    private JSONStorableFloat _variabilityJSON;
    private AudioSourceControl _headAudioSource;
    private float _next;
    private Dictionary<int, List<NamedAudioClip>> _clips;

    public override void Init()
    {
        try
        {
            _headAudioSource = containingAtom.GetStorableByID("HeadAudioSource") as AudioSourceControl;

            if (_headAudioSource == null) throw new InvalidOperationException($"Cannot initialize {nameof(Cornwall)} on atom {containingAtom.name} because it does not have a head audio source.");

            _clips = new Dictionary<int, List<NamedAudioClip>>
            {
                { 0, Clips("FemPixieW1057") },
                { 1, Clips("FemPixieW1058") },
                { 2, Clips("FemPixieW1059") },
            };

            int minValue = _clips.Keys.Min();
            int maxValue = _clips.Keys.Max();

            _intensityJSON = new JSONStorableFloat("Intensity", 0f, minValue, maxValue);
            RegisterFloat(_intensityJSON);
            CreateSlider(_intensityJSON);

            _variabilityJSON = new JSONStorableFloat("Variability", (maxValue - minValue) / 8f, 0f, (maxValue - minValue) / 2f);
            RegisterFloat(_variabilityJSON);
            CreateSlider(_variabilityJSON);
        }
        catch (Exception e)
        {
            SuperController.LogError($"{nameof(Cornwall)}.{nameof(Init)}: {e}");
        }
    }

    public void Update()
    {
        if (_clips == null || Time.time < _next)
            return;

        try
        {
            var intensity = (int)Mathf.Round(_intensityJSON.val + UnityEngine.Random.Range(-_variabilityJSON.val, _variabilityJSON.val));

            List<NamedAudioClip> clips;
            if (!_clips.TryGetValue(intensity, out clips))
                return;

            var clip = clips[UnityEngine.Random.Range(0, clips.Count - 1)];

            _headAudioSource.PlayIfClear(clip);
            _next = Time.time + clip.sourceClip.length + Padding;
        }
        catch (Exception e)
        {
            SuperController.LogError($"{nameof(Cornwall)}.{nameof(Update)}: {e}");
        }
    }

    private List<NamedAudioClip> Clips(params string[] names)
    {
        var list = new List<NamedAudioClip>(names.Length);
        foreach (var name in names)
            list.Add(EmbeddedAudioClipManager.singleton.GetClip(name));
        return list;
    }

    public void PrintAvailableClips()
    {
        foreach (var y in EmbeddedAudioClipManager.singleton.embeddedClips)
        {
            SuperController.LogMessage(y.uid);
        }
    }
}
