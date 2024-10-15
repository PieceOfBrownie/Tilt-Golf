using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MaterialPropertyBlockManager))]
[CanEditMultipleObjects]
public class MaterialPropertyBlockEditor : Editor
{
    MaterialPropertyBlockManager customMaterialValues;

    SerializedProperty materialIndex,
                       customColor,
                       customMetallic,  customSmoothness,
                       customNormalMap, customHeightMap,  customOcclusion,      
                       customTiling,    customOffset;


    void OnEnable()
    {
        customMaterialValues = (MaterialPropertyBlockManager)target;

        materialIndex = serializedObject.FindProperty("materialIndex");


        customColor = serializedObject.FindProperty("customColor");

        customMetallic   = serializedObject.FindProperty("customMetallic");
        customSmoothness = serializedObject.FindProperty("customSmoothness");

        customNormalMap = serializedObject.FindProperty("customNormalMap");
        customHeightMap = serializedObject.FindProperty("customHeightMap");
        customOcclusion = serializedObject.FindProperty("customOcclusion");

        customTiling = serializedObject.FindProperty("customTiling");
        customOffset = serializedObject.FindProperty("customOffset");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        if (customMaterialValues.gameObject.GetComponent<MeshRenderer>().sharedMaterials.Length > 1)
        {
            EditorGUILayout.LabelField("Materials", EditorStyles.boldLabel);
            EditorGUILayout.IntSlider(materialIndex,
                                      0, customMaterialValues.gameObject.GetComponent<MeshRenderer>().sharedMaterials.Length - 1,
                                      new GUIContent("Index"));
        }

        EditorGUILayout.LabelField("Custom Main Maps", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(customColor, new GUIContent("Albedo"));

        EditorGUILayout.Slider(customMetallic,
                               0f, 1f,
                               new GUIContent("Metallic"));

        EditorGUILayout.Slider(customSmoothness,
                               0f, 1f,
                               new GUIContent("Smoothness"));

        Material material = customMaterialValues.gameObject.GetComponent<MeshRenderer>().sharedMaterials[0];

        if (material != null && material.HasProperty("_BumpMap") && material.GetTexture("_BumpMap") != null)
            EditorGUILayout.PropertyField(customNormalMap, new GUIContent("Normal Map"));

        if (material != null && material.HasProperty("_ParallaxMap") && material.GetTexture("_ParallaxMap") != null)
            EditorGUILayout.Slider(customHeightMap,
                                   0.005f, 0.08f,
                                   new GUIContent("Height Map"));

        if (material != null && material.HasProperty("_OcclusionMap") && material.GetTexture("_OcclusionMap") != null)
            EditorGUILayout.Slider(customOcclusion,
                                   0f, 1f,
                                   new GUIContent("Occlusion"));

        Vector2Layout(customTiling, "Tiling");
        Vector2Layout(customOffset, "Offset");

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            customMaterialValues.UpdateMaterial();
        }
    }

    void Vector2Layout(SerializedProperty vector2Property, string vector2Name)
    {
        Rect vector2Rect = EditorGUI.PrefixLabel(EditorGUILayout.GetControlRect(), new GUIContent(vector2Name));

        Vector2 vector2 = vector2Property.vector2Value;

        var prevLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 14;

        float fieldWidth = (vector2Rect.width - EditorGUIUtility.labelWidth) / 1.8f;

        vector2.x = EditorGUI.FloatField(new Rect(vector2Rect.x, vector2Rect.y,
                                                  fieldWidth - 2.5f, vector2Rect.height),
                                         "X", vector2.x);

        vector2.y = EditorGUI.FloatField(new Rect(vector2Rect.x + fieldWidth, vector2Rect.y,    
                                                  fieldWidth, vector2Rect.height),
                                         "Y", vector2.y);

        EditorGUIUtility.labelWidth = prevLabelWidth;

        vector2Property.vector2Value = vector2;
    }
}
