using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeManager), typeof(SpectrumReader), typeof(AudioSource))]
public class MainManager : MonoBehaviour
{
    [SerializeField, Tooltip("Must be a power of 2! Min = 64. Max = 8192.")] private int _sampleCount = 256;
    private CubeManager _cm;

    void Start ()
    {
        if ((_sampleCount & (_sampleCount - 1)) != 0 || _sampleCount < 64 || _sampleCount > 8192)
            throw new Exception("Given sample count is not a compatible number");
	    _cm = GetComponent<CubeManager>();
	    _cm.CreateCircle(_sampleCount);
	    SpectrumReader.Initialize(this, GetComponent<AudioSource>(), _sampleCount, _cm.UpdateSpectrumData);
	}
}
