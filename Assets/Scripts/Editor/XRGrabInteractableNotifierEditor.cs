using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(XRGrabInteractableNotifier))]
public class XRGrabInteractableNotifierEditor : UnityEditor.Editor
{
    private SerializedProperty _attachTransform;
    private SerializedProperty _movementType;
    private SerializedProperty _convaiNPCManager;
    private SerializedProperty _isWeapon;
    private SerializedProperty _grabText;
    private SerializedProperty _touchNPCText;
    private SerializedProperty _hitNPCText;
    private SerializedProperty _throwNPCText;
    private SerializedProperty _hitSpeedThreshold;
    private SerializedProperty _speedUpdateInterval;
    

    private void OnEnable()
    {
        _attachTransform = serializedObject.FindProperty("m_AttachTransform");
        _movementType = serializedObject.FindProperty("m_MovementType");
        _convaiNPCManager = serializedObject.FindProperty("convaiNPCManager");
        _isWeapon = serializedObject.FindProperty("isWeapon");
        _grabText = serializedObject.FindProperty("grabText");
        _touchNPCText = serializedObject.FindProperty("touchNPCText");
        _hitNPCText = serializedObject.FindProperty("hitNPCText");
        _throwNPCText = serializedObject.FindProperty("throwNPCText");
        _hitSpeedThreshold = serializedObject.FindProperty("hitSpeedThreshold");
        _speedUpdateInterval = serializedObject.FindProperty("speedUpdateInterval");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_attachTransform);
        EditorGUILayout.PropertyField(_movementType);
        EditorGUILayout.PropertyField(_convaiNPCManager);
        EditorGUILayout.PropertyField(_isWeapon);
        EditorGUILayout.PropertyField(_grabText);
        EditorGUILayout.PropertyField(_touchNPCText);
        EditorGUILayout.PropertyField(_hitNPCText);
        EditorGUILayout.PropertyField(_throwNPCText);
        EditorGUILayout.PropertyField(_hitSpeedThreshold);
        EditorGUILayout.PropertyField(_speedUpdateInterval);

        serializedObject.ApplyModifiedProperties();
    }
}
