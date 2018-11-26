using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationModule : MonoBehaviour
{

    [SerializeField]
    private GameObject _object;
    [SerializeField]
    private float _rotationSpeed = 300f;

    [SerializeField]
    private Space _rotationSpace = Space.Self;
    [SerializeField]
    private Vector3 _rotationDirection = Vector3.right;

    [SerializeField]
    private bool _randomRotation = false;
    [SerializeField]
    private bool _continuesRandomRotation = false;
    [SerializeField]
    private float _directionChangeSpeed = .1f;

    private Vector3 _rotationTarget;

    // Use this for initialization
    void Start()
    {
        if (_randomRotation)
        {
            _rotationDirection = _rotationTarget = (new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))).normalized;
            if (_continuesRandomRotation)
            {
                StartCoroutine(ChangeRotation());
            }
        }
        else
        {
            _rotationDirection = _rotationTarget = _rotationDirection.normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_object != null)
        {
            _rotationDirection = Vector3.MoveTowards(_rotationDirection, _rotationTarget, _directionChangeSpeed);
            _object.transform.Rotate(_rotationDirection * _rotationSpeed * Time.deltaTime, _rotationSpace);
        }
    }

    IEnumerator ChangeRotation()
    {
        _rotationTarget = (new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))).normalized;
        yield return new WaitForSeconds(Random.Range(.5f, 1f));
        StartCoroutine(ChangeRotation());
    }
}
