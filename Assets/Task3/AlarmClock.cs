using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    [Header("Alarm Clock Ringing Settings")]
    [SerializeField] float _rotationAmplitude = 10f; 
    [SerializeField] float _rotationSpeed = 5f;      
    [SerializeField] float _ringTime;

    [Header("Bell Hammer Ringing Settings")]
    [SerializeField] Transform _bellHammer;
    [SerializeField] float _bellHammerRotationAmplitude = 10f;
    [SerializeField] float _bellHammerRotationSpeed = 5f;

    [Header("Bells Ringing Settings")]
    [SerializeField] Transform[] _bells;
    [SerializeField] float _bellsScaleAmplitude = 10f;
    [SerializeField] float _bellsScaleSpeed = 5f;

    bool _isRinging = false;       
    float _initialRotationZ, _initialHammerRotZ, _initialBellsScaleY;       
    float _timer;

    Animator _animator;

    int _scaleDir = 1;

    void Start()
    {
        _initialRotationZ = transform.eulerAngles.z;
        _initialHammerRotZ = _bellHammer.eulerAngles.z;
        _initialBellsScaleY = _bells[0].localScale.y;

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isRinging)
        {
            float rotationZ = _initialRotationZ + Mathf.Sin(Time.time * _rotationSpeed) * _rotationAmplitude;
            float bellHammerRotationZ = _initialHammerRotZ + Mathf.Sin(Time.time * _bellHammerRotationSpeed) * _bellHammerRotationAmplitude;
            float bellsScaleY = _initialBellsScaleY + Mathf.Sin(Time.time * _bellsScaleSpeed) * _bellsScaleAmplitude;

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
            _bellHammer.rotation = Quaternion.Euler(0, 0, bellHammerRotationZ);
            foreach (var bell in _bells)
            {
                bell.localScale += (_scaleDir * Vector3.up) * bellsScaleY;
            }

            if (_ringTime > 0)
            {
                _timer -= Time.deltaTime;
                if (_timer < 0) {
                    _timer = 0;
                    _isRinging = false;
                    _animator.SetTrigger("stopped_ringing");
                    StopRinging();
                } 
            }

            _scaleDir *= -1;
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                _animator.SetTrigger("ring");
            }
        }
    }
    public void StartRinging()
    {
        _isRinging = true;
        _timer = _ringTime;
    }

    public void StopRinging()
    {
        _isRinging = false;

        transform.rotation = Quaternion.Euler(0, 0, _initialRotationZ);
        _bellHammer.rotation = Quaternion.Euler(0, 0, _initialHammerRotZ);
        foreach (var bell in _bells)
        {
            bell.localScale = ((bell.localScale.x * Vector3.right) + (bell.localScale.z * Vector3.forward)) + Vector3.up * _initialBellsScaleY;
        }
    }
}
