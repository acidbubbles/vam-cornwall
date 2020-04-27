using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cornwall : MVRScript
{
    private JSONStorableFloat _intensityJSON;
    private JSONStorableFloat _variabilityJSON;
    private JSONStorableFloat _paddingJSON;
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
                { 0, Clips("FemPixieW1001") },
                { 1, Clips("FemPixieW1002") },
                { 2, Clips("FemPixieW1003") },
                { 3, Clips("FemPixieW1004") },
                { 4, Clips("FemPixieW1005") },
                { 5, Clips("FemPixieW1006") },
                { 6, Clips("FemPixieW1007") },
                { 7, Clips("FemPixieW1008") },
                { 8, Clips("FemPixieW1009") },
                { 9, Clips("FemPixieW1010") },
                { 10, Clips("FemPixieW1011") },
                { 11, Clips("FemPixieW1012") },
                { 12, Clips("FemPixieW1013") },
                { 13, Clips("FemPixieW1014") },
                { 14, Clips("FemPixieW1015") },
                { 15, Clips("FemPixieW1016") },
                { 16, Clips("FemPixieW1017") },
                { 17, Clips("FemPixieW1018") },
                { 18, Clips("FemPixieW1019") },
                { 19, Clips("FemPixieW1020") },
                { 20, Clips("FemPixieW1021") },
                { 21, Clips("FemPixieW1022") },
                { 22, Clips("FemPixieW1023") },
                { 23, Clips("FemPixieW1024") },
                { 24, Clips("FemPixieW1025") },
                { 25, Clips("FemPixieW1026") },
                { 26, Clips("FemPixieW1027") },
                { 27, Clips("FemPixieW1028") },
                { 28, Clips("FemPixieW1029") },
                { 29, Clips("FemPixieW1030") },
                { 30, Clips("FemPixieW1031") },
                { 31, Clips("FemPixieW1032") },
                { 32, Clips("FemPixieW1033") },
                { 33, Clips("FemPixieW1034") },
                { 34, Clips("FemPixieW1035") },
                { 35, Clips("FemPixieW1036") },
                { 36, Clips("FemPixieW1037") },
                { 37, Clips("FemPixieW1038") },
                { 38, Clips("FemPixieW1039") },
                { 39, Clips("FemPixieW1040") },
                { 40, Clips("FemPixieW1041") },
                { 41, Clips("FemPixieW1042") },
                { 42, Clips("FemPixieW1043") },
                { 43, Clips("FemPixieW1044") },
                { 44, Clips("FemPixieW1045") },
                { 45, Clips("FemPixieW1046") },
                { 46, Clips("FemPixieW1047") },
                { 47, Clips("FemPixieW1048") },
                { 48, Clips("FemPixieW1049") },
                { 49, Clips("FemPixieW1050") },
                { 50, Clips("FemPixieW1051") },
                { 51, Clips("FemPixieW1052") },
                { 52, Clips("FemPixieW1053") },
                { 53, Clips("FemPixieW1054") },
                { 54, Clips("FemPixieW1055") },
                { 55, Clips("FemPixieW1056") },
                { 56, Clips("FemPixieW1057") },
                { 57, Clips("FemPixieW1058") },
                { 58, Clips("FemPixieW1059") },
                { 59, Clips("FemPixieW1060") },
                { 60, Clips("FemPixieW1061") },
                { 61, Clips("FemPixieW1062") },
                { 62, Clips("FemPixieW1063") },
                { 63, Clips("FemPixieW1064") },
                { 64, Clips("FemPixieW1065") },
                { 65, Clips("FemPixieW1066") },
                { 66, Clips("FemPixieW1067") },
                { 67, Clips("FemPixieW1068") },
                { 68, Clips("FemPixieW1069") },
                { 69, Clips("FemPixieW1070") },
                { 70, Clips("FemPixieW1071") },
                { 71, Clips("FemPixieW1072") },
                { 72, Clips("FemPixieW1073") },
                { 73, Clips("FemPixieW1074") },
                { 74, Clips("FemPixieW1075") },
                { 75, Clips("FemPixieW1076") },
                { 76, Clips("FemPixieW1077") },
                { 77, Clips("FemPixieW1078") },
                { 78, Clips("FemPixieW1079") },
                { 79, Clips("FemPixieW1080") },
                { 80, Clips("FemPixieW1081") },
                { 81, Clips("FemPixieW1082") },
                { 82, Clips("FemPixieW1083") },
                { 83, Clips("FemPixieW1084") },
                { 84, Clips("FemPixieW1085") },
                { 85, Clips("FemPixieW1086") },
                { 86, Clips("FemPixieW1087") },
                { 87, Clips("FemPixieW1088") },
                { 88, Clips("FemPixieW1089") },
                { 89, Clips("FemPixieW1090") },
                { 90, Clips("FemPixieW1091") },
                { 91, Clips("FemPixieW1092") },
                { 92, Clips("FemPixieW1093") },
                { 93, Clips("FemPixieW1094") },
                { 94, Clips("FemPixieW1095") },
                { 95, Clips("FemPixieW1096") },
                { 96, Clips("FemPixieW1097") },
                { 97, Clips("FemPixieW1098") },
                { 98, Clips("FemPixieW1099") },
            };

            int minValue = _clips.Keys.Min();
            int maxValue = _clips.Keys.Max();

            _intensityJSON = new JSONStorableFloat("Intensity", 0f, minValue, maxValue);
            RegisterFloat(_intensityJSON);
            CreateSlider(_intensityJSON);

            _variabilityJSON = new JSONStorableFloat("Variability", (maxValue - minValue) / 8f, 0f, (maxValue - minValue) / 2f);
            RegisterFloat(_variabilityJSON);
            CreateSlider(_variabilityJSON);

            _paddingJSON = new JSONStorableFloat("Padding", 0.2f, 0f, 5f, false);
            RegisterFloat(_paddingJSON);
            CreateSlider(_paddingJSON);
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
            _next = Time.time + clip.sourceClip.length + _paddingJSON.val;
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
