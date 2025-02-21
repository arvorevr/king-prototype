using System.Collections;
using Convai.Scripts.Runtime.Core;
using Convai.Scripts.Runtime.Features;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XRGrabInteractableNotifier : XRGrabInteractable
{
    [Header("Notifier Data")]
    [SerializeField] private string objectName;
    [SerializeField] private bool isWeapon;
    [SerializeField] private float hitSpeedThreshold = 1.0f;
    [SerializeField] private float throwSpeedThreshold = 1.0f;
    [SerializeField] private float speedUpdateInterval = 0.1f;

    private ConvaiNPCManager _convaiNpcManager;
    private ConvaiPlayerInteractionManager _convaiInteraction;
    private Rigidbody _rigidbody;
    private bool _selectTextSubmitted;
    private Vector3 _lastPosition;
    private float _currentTimer;
    private float _currentSpeed;

    private string _grabText;
    private string _touchNpcText;
    private string _hitNpcText;
    private string _throwNpcText;
    private string _stealText;
    
    public GameObject NpcGrabbingObject { get; set; }

    private void Start()
    {
        _convaiNpcManager = ConvaiNPCManager.Instance;
        _rigidbody = GetComponent<Rigidbody>();
        _grabText = $"[Player Action] I grabbed the {objectName}";
        _touchNpcText = $"[Player Action] I touched you with the {objectName}";
        _hitNpcText = $"[Player Action] I hit you with the {objectName}";
        _throwNpcText = $"[Player Action] I threw the {objectName} at you";
        _stealText = $"[Player Action] I stole the {objectName} from you";
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (!isSelected) return;

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            _currentTimer += Time.deltaTime;
            if (_currentTimer >= speedUpdateInterval)
            {
                _currentSpeed = Vector3.Distance(_lastPosition, transform.position) / speedUpdateInterval;
                _currentTimer = 0;
                _lastPosition = transform.position;
            }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (NpcGrabbingObject)
        {
            if(NpcGrabbingObject.TryGetComponent(out ConvaiActionsHandler npcActions))
            {
                npcActions.Drop(gameObject);
            }
            
            if (_convaiNpcManager.activeConvaiNPC)
            {
                _convaiInteraction = _convaiNpcManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
                _convaiInteraction?.HandleInputSubmission(_stealText);
            }
        }

        base.OnSelectEntered(args);

        NpcGrabbingObject = null;

        if (_convaiInteraction || !_convaiNpcManager.activeConvaiNPC) return;
        StartCoroutine(SubmitSelectText());
    }

    private IEnumerator SubmitSelectText()
    {
        _selectTextSubmitted = true;
        if (_convaiNpcManager.activeConvaiNPC)
        {
            _convaiInteraction = _convaiNpcManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
            _convaiInteraction?.HandleInputSubmission(_grabText);
        }

        yield return new WaitForSeconds(15f);
        _selectTextSubmitted = false;
        _convaiInteraction = null;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        NpcGrabbingObject = null;
        transform.parent = null;
        _rigidbody.isKinematic = false;

        if (_convaiNpcManager.activeConvaiNPC && _selectTextSubmitted)
        {
            _convaiNpcManager.activeConvaiNPC.InterruptCharacterSpeech();
            _selectTextSubmitted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Character")) return;
        if (isSelected)
        {
            if (_currentSpeed > hitSpeedThreshold)
            {
                if (!_convaiNpcManager.activeConvaiNPC) return;
                _convaiInteraction = _convaiNpcManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
                _convaiInteraction?.HandleInputSubmission(_hitNpcText);
            }
            else
            {
                if (!_convaiNpcManager.activeConvaiNPC) return;
                _convaiInteraction = _convaiNpcManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
                _convaiInteraction?.HandleInputSubmission(_touchNpcText);
            }
        }
        else
        {
            if (!(_rigidbody.linearVelocity.magnitude >= throwSpeedThreshold)) return;
            if (!_convaiNpcManager.activeConvaiNPC) return;
            _convaiInteraction = _convaiNpcManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
            _convaiInteraction?.HandleInputSubmission(_throwNpcText);
        }
    }
}