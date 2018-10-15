using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    public enum MovementActivatorType { HeadTilt, Touch }

    public MovementActivatorType MovementActivator;

    public float Speed = 1;
    public float Threshold = 0.3f;

    private Camera _camera;

    void Start() {
        _camera = GetComponentInChildren<Camera>();
    }

	void FixedUpdate () {
	    int move = 0;

	    if (MovementActivator == MovementActivatorType.HeadTilt) {

	        var deviceX = Input.acceleration.x;
	        var deviceY = Input.acceleration.y;

	        if (Mathf.Abs(deviceX) > Threshold) {
	            move = (int) Mathf.Sign(deviceX);
	        }
	    } else if (MovementActivator == MovementActivatorType.Touch) {

	        if (Input.touches.Any()) {
	            move = 1;
            }
        }

	    var rot = _camera.transform.rotation;
	    var mov = Vector3.forward * move * Time.fixedDeltaTime * Speed;
	    var nmov = rot * mov;

	    transform.Translate(nmov.x, 0, nmov.z);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor {
    private SerializedProperty _movementActivatorProperty;
    private SerializedProperty _speedProperty;
    private SerializedProperty _thresholdProperty;

    void OnEnable() {
        _movementActivatorProperty = serializedObject.FindProperty("MovementActivator");
        _speedProperty = serializedObject.FindProperty("Speed");
        _thresholdProperty = serializedObject.FindProperty("Threshold");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_movementActivatorProperty);
        EditorGUILayout.PropertyField(_speedProperty);

        var type = (PlayerMovement.MovementActivatorType) _movementActivatorProperty.enumValueIndex;
        if (type == PlayerMovement.MovementActivatorType.HeadTilt) {
            EditorGUILayout.PropertyField(_thresholdProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif