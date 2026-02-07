using UnityEngine;

public class ShaderScript : MonoBehaviour
{
    private Camera cam;
    public Shader toonShader;
    private bool shaderEnabled;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    void Update()
    {
     
    }

    [ContextMenu("SwitchShader")]
    void SwitchShader() { 
     shaderEnabled = !shaderEnabled;
        if (shaderEnabled)
        {
            cam.SetReplacementShader(toonShader, "");
        }
        else
        {
            cam.ResetReplacementShader();
        }
    }
}
