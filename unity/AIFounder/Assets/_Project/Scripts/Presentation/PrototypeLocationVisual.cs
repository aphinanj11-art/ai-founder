using UnityEngine;

namespace AIFounder.Presentation
{
    public sealed class PrototypeLocationVisual : MonoBehaviour
    {
        [SerializeField] private Color markerColor = Color.white;

        public Color MarkerColor => markerColor;

        private void Awake()
        {
            Apply();
        }

        public void Configure(Color color)
        {
            markerColor = color;
            Apply();
        }

        private void Apply()
        {
            Renderer markerRenderer = GetComponent<Renderer>();
            if (markerRenderer != null)
            {
                markerRenderer.material.color = markerColor;
            }
        }
    }
}
