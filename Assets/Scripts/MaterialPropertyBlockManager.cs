using UnityEngine;

[ExecuteAlways]
[AddComponentMenu("Material Property Block")]
public class MaterialPropertyBlockManager : MonoBehaviour
{
    [SerializeField] int materialIndex;
             private int lastIndex;


    [SerializeField] Color customColor;

    [SerializeField] float customMetallic, customSmoothness,
                           customNormalMap, customHeightMap, customOcclusion;

    [SerializeField] Vector2 customTiling,
                             customOffset;


    void Awake()
    {
        if (Application.isPlaying)
        {
            UpdateMaterial();
        }
    }

    void Reset()
    {
        StartMaterial();
    }

    void OnValidate()
    {
        if (materialIndex != lastIndex)
        {
            ChangeMaterial();
            lastIndex = materialIndex;
        }

        UpdateMaterial();
    }

    void StartMaterial()
    {
        materialIndex = 0;

        ChangeMaterial();
        UpdateMaterial();
    }

    void ChangeMaterial()
    {
        Material material = Application.isPlaying
            ? gameObject.GetComponent<MeshRenderer>().materials[materialIndex]
            : gameObject.GetComponent<MeshRenderer>().sharedMaterials[materialIndex];


        customColor = material.color;

        customMetallic = material.GetFloat("_Metallic");
        customSmoothness = material.GetFloat("_Glossiness");

        customNormalMap = material.GetTexture("_BumpMap") != null
            ? material.GetFloat("_BumpScale")
            : 1f;
        customHeightMap = material.GetTexture("_ParallaxMap") != null
            ? material.GetFloat("_Parallax")
            : 0.02f;
        customOcclusion = material.GetTexture("_OcclusionMap") != null
            ? material.GetFloat("_OcclusionStrength")
            : 1f;

        customTiling = material.mainTextureScale;
        customOffset = material.mainTextureOffset;
    }

    public void UpdateMaterial()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();


        materialPropertyBlock.SetColor("_Color", customColor);

        materialPropertyBlock.SetFloat("_Metallic", customMetallic);
        materialPropertyBlock.SetFloat("_Glossiness", customSmoothness);

        materialPropertyBlock.SetFloat("_BumpScale", customNormalMap);
        materialPropertyBlock.SetFloat("_Parallax", customHeightMap);
        materialPropertyBlock.SetFloat("_OcclusionStrength", customOcclusion);

        materialPropertyBlock.SetVector("_MainTex_ST", new Vector4(customTiling.x, customTiling.y,
                                                                   customOffset.x, customOffset.y));


        gameObject.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, materialIndex);
    }
}