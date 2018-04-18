namespace Mapbox.Editor
{
	using UnityEngine;
	using Mapbox.Unity.Map;
	using UnityEditor;
	using System;
	using System.Collections.Generic;

	[CustomPropertyDrawer(typeof(PrefabItemOptions))]
	public class PrefabItemOptionsDrawer : PropertyDrawer
	{

		static float _lineHeight = EditorGUIUtility.singleLineHeight;
		private int shiftLeftPixels = -22;
		private GUIContent prefabLocationsTitle = new GUIContent
		{
			text = "Prefab Locations",
			tooltip = "The properties for creating POI filters"
		};


		private GUIContent findByDropDown = new GUIContent
		{
			text = "Find by",
			tooltip = "The type of filter you would like to use for looking up POIs"
		};

		private GUIContent categoriesDropDown = new GUIContent
		{
			text = "Category",
			tooltip = "Would you like to filter them by categories for the POI?"
		};

		private GUIContent  densitySlider = new GUIContent
		{
			text = "Density",
			tooltip = "This slider defines the density of POIs in a region"
		};

		private GUIContent nameField = new GUIContent
		{
			text = "Name",
			tooltip = "All the POIs containing this string will be shown on the map"
		};

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position.y += 1.2f*_lineHeight;

			var prefabItemCoreOptions = property.FindPropertyRelative("coreOptions");
			GUILayout.Label(prefabItemCoreOptions.FindPropertyRelative("sublayerName").stringValue + " Properties");
			GUILayout.Space(2.5f*EditorGUIUtility.singleLineHeight);

			//Prefab Game Object
			position.y += _lineHeight;
			var spawnPrefabOptions = property.FindPropertyRelative("spawnPrefabOptions");
			EditorGUI.PropertyField(new Rect(position.x, position.y,position.width,2*_lineHeight),spawnPrefabOptions);
			GUILayout.Space(1);

			//Prefab Locations title
			GUILayout.Label(prefabLocationsTitle);

			//FindBy drop down
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(findByDropDown);
			var findByProp = property.FindPropertyRelative("findByType");
			GUILayout.Space(shiftLeftPixels);
			findByProp.enumValueIndex = EditorGUILayout.Popup(findByProp.enumValueIndex, findByProp.enumDisplayNames);
			EditorGUILayout.EndHorizontal();

			switch((LocationPrefabFindBy)findByProp.enumValueIndex)
			{
				case (LocationPrefabFindBy.MapboxCategory):
					ShowCategoryOptions(property);
					break;
				case(LocationPrefabFindBy.AddressOrLatLon):
					ShowAddressOrLatLonUI(property);
					break;
				case (LocationPrefabFindBy.POIName):
					ShowPOINames(property);
					break;
				default:
					break;
			}

			EditorGUI.EndProperty();
		}

		private void ShowCategoryOptions(SerializedProperty property)
		{
			//Category drop down
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(categoriesDropDown);

			var categoryProp = property.FindPropertyRelative("categories");
			GUILayout.Space(shiftLeftPixels);
			categoryProp.intValue = (int)(LocationPrefabCategories)(EditorGUILayout.EnumFlagsField((LocationPrefabCategories)categoryProp.intValue));
			EditorGUILayout.EndHorizontal();

			ShowDensitySlider(property);
		}

		private void ShowAddressOrLatLonUI(SerializedProperty property)
		{
			
		}


		private void ShowPOINames(SerializedProperty property)
		{
			//Name field
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(nameField);

			var categoryProp = property.FindPropertyRelative("poiName");
			GUILayout.Space(shiftLeftPixels);

			categoryProp.stringValue = EditorGUILayout.TextField(categoryProp.stringValue);
			EditorGUILayout.EndHorizontal();

			ShowDensitySlider(property);
		}

		private void ShowDensitySlider(SerializedProperty property)
		{
			//Density slider
			var densityProp = property.FindPropertyRelative("density");
			if (Application.isPlaying)
			{
				GUI.enabled = false;
			}

			EditorGUILayout.PropertyField(densityProp, densitySlider);
			var integ = densityProp.intValue;
			GUI.enabled = true;
			densityProp.serializedObject.ApplyModifiedProperties();
		}

		private Rect GetNewRect(Rect position)
		{
			return new Rect(position.x, position.y, position.width, _lineHeight);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return _lineHeight;
		}
	}
}