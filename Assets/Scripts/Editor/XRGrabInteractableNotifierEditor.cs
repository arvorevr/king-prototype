using UnityEditor;

[CustomEditor(typeof(XRGrabInteractableNotifier))]
public class XRGrabInteractableNotifierEditor : Editor
{
    private SerializedProperty _attachTransform;
    private SerializedProperty _movementType;
    private SerializedProperty _isWeapon;
    private SerializedProperty _objectName;
    private SerializedProperty _hitSpeedThreshold;
    private SerializedProperty _throwSpeedThreshold;
    private SerializedProperty _speedUpdateInterval;
    

    private void OnEnable()
    {
        _attachTransform = serializedObject.FindProperty("m_AttachTransform");
        _movementType = serializedObject.FindProperty("m_MovementType");
        _isWeapon = serializedObject.FindProperty("isWeapon");
        _objectName = serializedObject.FindProperty("objectName");
        _hitSpeedThreshold = serializedObject.FindProperty("hitSpeedThreshold");
        _throwSpeedThreshold = serializedObject.FindProperty("throwSpeedThreshold");
        _speedUpdateInterval = serializedObject.FindProperty("speedUpdateInterval");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_attachTransform);
        EditorGUILayout.PropertyField(_movementType);
        EditorGUILayout.PropertyField(_isWeapon);
        EditorGUILayout.PropertyField(_objectName);
        EditorGUILayout.PropertyField(_hitSpeedThreshold);
        EditorGUILayout.PropertyField(_throwSpeedThreshold);
        EditorGUILayout.PropertyField(_speedUpdateInterval);

        serializedObject.ApplyModifiedProperties();
    }
}
