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
    public GameObject Flashlight;
    public GameObject Text;
    private bool facingdown;
    private bool GadgetOn; 
    private float timer;
    private int neededsequence = 3;
    private int currentsequence = 0;

    private Camera _camera;

    void Start() {
        _camera = GetComponentInChildren<Camera>();
    }

	void FixedUpdate () {
        timer += Time.fixedDeltaTime;

        int move = 0;
        
        if (MovementActivator == MovementActivatorType.HeadTilt) {

	        var deviceX = Input.acceleration.x;
	        var deviceY = Input.acceleration.y;
            Text.GetComponent<Text>().text = currentsequence.ToString() + "/"+neededsequence.ToString();
            if ((Mathf.Abs(deviceY) < (1-(Threshold))))
            {
                if (!facingdown)
                {
                    if (timer < 3)
                    {
                        currentsequence += 1;
                        if(currentsequence == neededsequence)
                        {
                            Flashlight.SetActive(!Flashlight.active);
                            currentsequence = 0;
                        }

                    }
                    else
                    {
                        currentsequence = 1;
                    }
                    facingdown = true;
                    timer = 0;
                    
                }
                
            }
            else 
            {
                if (facingdown)
                {
                    facingdown = false;
                }
                
            }
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
    private SerializedProperty _flashlight;
    private SerializedProperty _text;

    void OnEnable() {
        _movementActivatorProperty = serializedObject.FindProperty("MovementActivator");
        _speedProperty = serializedObject.FindProperty("Speed");
        _thresholdProperty = serializedObject.FindProperty("Threshold");
        _flashlight = serializedObject.FindProperty("Flashlight");
        _text = serializedObject.FindProperty("Text");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_movementActivatorProperty);
        EditorGUILayout.PropertyField(_speedProperty);
        EditorGUILayout.PropertyField(_flashlight);
        EditorGUILayout.PropertyField(_text);

        var type = (PlayerMovement.MovementActivatorType) _movementActivatorProperty.enumValueIndex;
        if (type == PlayerMovement.MovementActivatorType.HeadTilt) {
            EditorGUILayout.PropertyField(_thresholdProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif