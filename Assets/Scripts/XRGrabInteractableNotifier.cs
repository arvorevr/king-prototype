using System.Collections;
using Convai.Scripts.Runtime.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XRGrabInteractableNotifier : XRGrabInteractable
{
    [Header("Notifier Data")] [SerializeField]
    private ConvaiNPCManager convaiNPCManager;

    [SerializeField] private bool isWeapon;
    [SerializeField] private string grabText = "I grabbed this object!";
    [SerializeField] private string touchNPCText = "I touched with this object!";
    [SerializeField] private string hitNPCText = "I hit with this object!";
    [SerializeField] private string throwNPCText = "I threw this object!";
    [SerializeField] private float hitSpeedThreshold = 1.0f;
    [SerializeField] private float speedUpdateInterval = 0.1f;

    private ConvaiPlayerInteractionManager _convaiInteraction;
    private bool _touchNpc;
    private bool _selectTextSubmitted;
    private Vector3 _lastPosition;
    private float _currentTimer;
    private float _currentSpeed;

    public bool IsGrabbedByNpc { get; set; }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        // if (!isSelected || !_touchNpc) return;
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

        if (_touchNpc)
        {
            if (_currentSpeed > hitSpeedThreshold)
            {
                if (!convaiNPCManager.activeConvaiNPC) return;
                _convaiInteraction = convaiNPCManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
                _convaiInteraction?.HandleInputSubmission(hitNPCText);
            }
            else
            {
                if (!convaiNPCManager.activeConvaiNPC) return;
                _convaiInteraction = convaiNPCManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
                _convaiInteraction?.HandleInputSubmission(touchNPCText);
            }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        IsGrabbedByNpc = false;

        if (_convaiInteraction || !convaiNPCManager.activeConvaiNPC) return;
        StartCoroutine(SubmitSelectText());
    }

    private IEnumerator SubmitSelectText()
    {
        _selectTextSubmitted = true;
        if (convaiNPCManager.activeConvaiNPC)
        {
            // Debug.Log($"Text: {grabText} - {convaiNPCManager.activeConvaiNPC.characterName}");
            _convaiInteraction = convaiNPCManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
            _convaiInteraction?.HandleInputSubmission(grabText);
        }

        yield return new WaitForSeconds(15f);
        _selectTextSubmitted = false;
        _convaiInteraction = null;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        // Debug.Log("OnSelectExited: " + gameObject.name);
        IsGrabbedByNpc = false;

        if (convaiNPCManager.activeConvaiNPC && _selectTextSubmitted)
        {
            convaiNPCManager.activeConvaiNPC.InterruptCharacterSpeech();
            _selectTextSubmitted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Debug.Log($"Speed: {_currentSpeed}");
            _touchNpc = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            _touchNpc = false;
        }
    }
}