using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpectrumReader : MonoBehaviour {

    public delegate void UpdateAction(ref float[] data);
    private UpdateAction _updateSpectrumCallback { get; set; }
    private AudioSource _audio;

    float[] _samples = new float[256];

	void Update () {
        if (_updateSpectrumCallback != null)
        {
            _audio.GetSpectrumData(_samples, 0, FFTWindow.Rectangular);
            _updateSpectrumCallback(ref _samples);
        }
    }

    public static SpectrumReader Initialize(MonoBehaviour component, AudioSource audio, int samples)
    {
        return Initialize(component, audio, samples, null);
    }

    public static SpectrumReader Initialize(MonoBehaviour component, AudioSource audio, int samples, UpdateAction callback)
    {
        var sr = component.GetComponent<SpectrumReader>();
        if (!sr)
            sr = component.gameObject.AddComponent<SpectrumReader>();
        sr._samples = new float[samples];
        sr._audio = audio;
        sr._updateSpectrumCallback = callback;

        return sr;
    }
}
