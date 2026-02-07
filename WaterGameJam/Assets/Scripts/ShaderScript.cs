using UnityEngine;

public class ShaderScript : MonoBehaviour
{
    private Camera cam;
    public Shader toonShader;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    void Update()
    {
        if (this.enabled)
            cam.RenderWithShader(toonShader, "");
        cam.SetReplacementShader(toonShader, "");

    }
}
