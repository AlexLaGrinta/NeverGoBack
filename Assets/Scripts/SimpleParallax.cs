using UnityEngine;
using UnityEngine.UI;

public class ParallaxLayers : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public RectTransform layer;        // Le layer UI à bouger
        public float strength = 10f;       // Force du déplacement pour cette couche
        [HideInInspector]
        public Vector2 basePos;            // Position de départ
    }

    public ParallaxLayer[] layers;

    void Start()
    {
        foreach (var l in layers)
            l.basePos = l.layer.anchoredPosition;
    }

    void Update()
    {
        Vector2 mouse = Input.mousePosition;
        float x = (mouse.x / Screen.width - 0.5f) * 2f;
        float y = (mouse.y / Screen.height - 0.5f) * 2f;

        foreach (var l in layers)
        {
            l.layer.anchoredPosition = l.basePos + new Vector2(x, y) * l.strength;
        }
    }
}
