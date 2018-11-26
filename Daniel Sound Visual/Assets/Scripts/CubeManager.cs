using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CubeManager : MonoBehaviour {

    [SerializeField] private MeshRenderer _cubePrefab;
    [SerializeField] private float _distanceFromCenter = 40f;
    [SerializeField] private Vector3 _maxScale = new Vector3(.5f,200f, .5f);
    [SerializeField] private Transform _center;
    [SerializeField] private float _scaleSpeed = 1.5f;
    [SerializeField, ColorUsageAttribute(true, true, 0, 8, 0.125f, 3)] private Color _minEmission;
    [SerializeField, ColorUsageAttribute(true, true, 0, 8, 0.125f, 3)] private Color _maxEmission;
    [SerializeField] private bool _individualColors = false;
    [SerializeField] private bool _logarithmic = false;
    private Cube[] _cubes;

    public void CreateCircle(int count)
    {
        if (_cubes != null)
        {
            for (int i = 0; i < _cubes.Length; i++)
            {
                Destroy(_cubes[i].Trans.gameObject);
            }
            _cubes = null;
        }

        _cubes = new Cube[count];
        var angle = 360f / (float)count;

        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(_cubePrefab);

            var cube = new Cube();
            cube.Mat = (obj.material = new Material(obj.material));
            cube.Trans = obj.transform;                       
            cube.Trans.parent = _center;
            cube.Trans.position = _center.position;
            cube.Trans.rotation *= Quaternion.Euler(0, angle * i, 0);
            cube.Trans.position += cube.Trans.rotation * new Vector3(0, 0, _distanceFromCenter);
            cube.Trans.localScale = _maxScale;
            _cubes[i] = cube;
        }
    }

    public void UpdateSpectrumData(ref float[] spectrumData)
    {
        if (_cubes == null || _cubes.Length != spectrumData.Length)
            return;
        float max = spectrumData.Max();
        for (int i = 0; i < spectrumData.Length / 2; i++)
        {
            Scale(i, spectrumData[i], max);
            Scale(_cubes.Length - i - 1, spectrumData[i + 5], max);
        }
    }

    void Scale(int index, float spectrum, float max)
    {
        var cube = _cubes[index];
        var logMultiplier = _logarithmic ? (index != 0 ? 1 + Mathf.Log(index) : 1) / 2 : 1;
        var targetScale = new Vector3(_maxScale.x, _maxScale.y*spectrum* logMultiplier, _maxScale.z);
        var distanceDelta = (cube.Trans.localScale.y > spectrum*_maxScale.y ? 4 : 1)*_scaleSpeed*_maxScale.y*
                            Time.deltaTime;
        cube.Trans.localScale = 
            Vector3.MoveTowards(cube.Trans.localScale, targetScale, distanceDelta);
        cube.Mat.SetColor("_EmissionColor",
            Color32.Lerp(_minEmission, _maxEmission, (_individualColors) ? spectrum / .2f : max / .5f));
    }

    private struct Cube
    {
        public Material Mat;
        public Transform Trans;
    }

}
