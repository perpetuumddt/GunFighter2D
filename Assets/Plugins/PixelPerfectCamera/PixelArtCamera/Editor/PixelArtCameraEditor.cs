using UnityEditor;
using UnityEngine;

namespace Gunfighter.Plugins.PixelPerfectCamera.PixelArtCamera.Editor
{
	[CustomEditor(typeof(PixelArtCamera))]
	public class PixelArtCameraEditor : global::UnityEditor.Editor {
		SerializedProperty _pixels;
		SerializedProperty _pixelsPerUnit;
		SerializedProperty _smooth;
		SerializedProperty _forceSquarePixels;

		SerializedProperty _screenResolution;
		SerializedProperty _upscaledResolution;
		SerializedProperty _internalResolution;
		SerializedProperty _finalBlitStretch;

		SerializedProperty _mainCamera;
		SerializedProperty _mainCanvas;

		SerializedProperty _requireStencilBuffer;
	
		void OnEnable () {
			_pixels = serializedObject.FindProperty("pixels");		
			_pixelsPerUnit = serializedObject.FindProperty("pixelsPerUnit");
			_smooth = serializedObject.FindProperty("smooth");
			_forceSquarePixels = serializedObject.FindProperty("forceSquarePixels");
			_screenResolution = serializedObject.FindProperty("screenResolution");
			_upscaledResolution = serializedObject.FindProperty("upscaledResolution");
			_internalResolution = serializedObject.FindProperty("internalResolution");
			_finalBlitStretch = serializedObject.FindProperty("finalBlitStretch");
			_mainCamera = serializedObject.FindProperty("mainCamera");
			_mainCanvas = serializedObject.FindProperty("mainCanvas");
			_requireStencilBuffer = serializedObject.FindProperty("requireStencilBuffer");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			//GUILayout.Label ("Smooth");
			DrawDefaultInspector ();
			_pixels.vector2IntValue = EditorGUILayout.Vector2IntField("Target Pixel Dimensions", _pixels.vector2IntValue);
			_pixelsPerUnit.floatValue = EditorGUILayout.FloatField("Pixels Per Unit", _pixelsPerUnit.floatValue);
			_smooth.boolValue = EditorGUILayout.Toggle("Smooth", _smooth.boolValue);
			_forceSquarePixels.boolValue = EditorGUILayout.Toggle("Force Square Pixels", _forceSquarePixels.boolValue);
			_requireStencilBuffer.boolValue = EditorGUILayout.Toggle("Require Stencil Buffer (For Masks)", _requireStencilBuffer.boolValue);
			EditorGUILayout.LabelField("Screen: " + _screenResolution.vector2IntValue.x + "×" + _screenResolution.vector2IntValue.y);
			EditorGUILayout.LabelField("Pixel Resolution: " + _internalResolution.vector2IntValue.x + "×" + _internalResolution.vector2IntValue.y);
			EditorGUILayout.LabelField("Upscaled Resolution: " + _upscaledResolution.vector2IntValue.x + "×" + _upscaledResolution.vector2IntValue.y);
			Vector2 pixelSize = Vector2.zero;
			pixelSize.x = (float)_screenResolution.vector2IntValue.x / (float)_internalResolution.vector2IntValue.x / _finalBlitStretch.vector2Value.x;
			pixelSize.y = (float)_screenResolution.vector2IntValue.y / (float)_internalResolution.vector2IntValue.y / _finalBlitStretch.vector2Value.y;
			EditorGUILayout.LabelField("Pixel Scale: " + pixelSize.x + "×" + pixelSize.y);

			serializedObject.ApplyModifiedProperties ();
		}
	}
}
