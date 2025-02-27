using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private bool closeAfterExit = true;
    [SerializeField] private GameObject requiredItem;
    [SerializeField] private float openTime = 1.0f;
    [SerializeField] private Vector3 openRotation = new Vector3(0, 90f, 0);

    public bool IsOpen { get; private set; }
    private bool _isOpening;
    private bool _isClosing;
    private float _rotateTimer;
    private Vector3 _initialRotation;
    private int _nearCharacterCount;

    private void Start()
    {
        _rotateTimer = 0;
        _isOpening = false;
        _isClosing = false;
        _initialRotation = transform.rotation.eulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (requiredItem && !(other.TryGetComponent<Inventory>(out var inventory) &&
                              inventory.Items.Contains(requiredItem)))
        {
            return;
        }

        if (!IsOpen || _isClosing)
        {
            _isOpening = true;
        }

        // _rotateTimer = 0;
        _isClosing = false;
        _nearCharacterCount = _nearCharacterCount < 0 ? 1 : _nearCharacterCount + 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOpen)
        {
            return;
        }

        _nearCharacterCount--;
        if (_nearCharacterCount <= 0 && closeAfterExit)
        {
            _isOpening = false;
            _isClosing = true;
            // _rotateTimer = 0;
        }
    }

    private void Update()
    {
        HandleOpening();
        HandleClosing();
    }

    private void HandleOpening()
    {
        if (_isOpening)
        {
            _rotateTimer += Time.deltaTime;
            if (_rotateTimer >= openTime)
            {
                IsOpen = true;
                _isOpening = false;
                transform.rotation = Quaternion.Euler(openRotation);
            }
            else
            {
                var rotation = Vector3.Lerp(_initialRotation, openRotation, _rotateTimer / openTime);
                transform.rotation = Quaternion.Euler(rotation);
            }
        }
    }

    private void HandleClosing()
    {
        if (_isClosing)
        {
            _rotateTimer -= Time.deltaTime;
            if (_rotateTimer <= 0)
            {
                IsOpen = false;
                _isClosing = false;
                _rotateTimer = 0;
                transform.rotation = Quaternion.Euler(_initialRotation);
            }
            else
            {
                var rotation = Vector3.Lerp(_initialRotation, openRotation, _rotateTimer / openTime);
                transform.rotation = Quaternion.Euler(rotation);
            }
        }
    }
}