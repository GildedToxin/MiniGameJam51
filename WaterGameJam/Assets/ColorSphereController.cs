using UnityEngine;

[ExecuteAlways]
public class ColorSphereController : MonoBehaviour
{
    public Material targetMaterial;
    public float radius = 5f;
    public Color color = Color.white;

    void Update()
    {
        if (!targetMaterial) return;

        targetMaterial.SetVector("_SphereCenter", transform.position);
        targetMaterial.SetFloat("_SphereRadius", radius);
        targetMaterial.SetColor("_BaseColor", color);
    }
}
