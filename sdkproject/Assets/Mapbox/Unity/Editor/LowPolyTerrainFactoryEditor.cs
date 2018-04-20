﻿namespace Mapbox.Editor
{
	using UnityEngine;
	using UnityEditor;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Map;

	[CustomEditor(typeof(LowPolyTerrainFactory))]
	public class LowPolyTerrainFactoryEditor : FactoryEditor
	{
		public SerializedProperty layerProperties;
		private MonoScript script;

		void OnEnable()
		{
			layerProperties = serializedObject.FindProperty("_elevationOptions");
			var terrainType = layerProperties.FindPropertyRelative("elevationLayerType");
			terrainType.enumValueIndex = (int)ElevationLayerType.LowPolygonTerrain;

			script = MonoScript.FromScriptableObject((LowPolyTerrainFactory)target);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			GUI.enabled = false;
			script = EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false) as MonoScript;
			GUI.enabled = true;
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(layerProperties);
			EditorGUILayout.Space();

			serializedObject.ApplyModifiedProperties();
		}
	}
}