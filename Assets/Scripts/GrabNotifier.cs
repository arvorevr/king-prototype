using Convai.Scripts.Runtime.Core;
using UnityEngine;

public class GrabNotifier : MonoBehaviour
{
    [SerializeField] private ConvaiNPCManager convaiNPCManager;
    [SerializeField] private string grabText = "I grabbed this object!";
    private ConvaiPlayerInteractionManager convaiInteraction;

    public void OnGrab()
    {
        if (convaiInteraction == null && convaiNPCManager.activeConvaiNPC != null)
        {
            convaiInteraction = convaiNPCManager.activeConvaiNPC.GetComponent<ConvaiPlayerInteractionManager>();
            convaiInteraction?.HandleInputSubmission(grabText);
        }
    }
}