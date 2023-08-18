using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTheme.Runtime.Components
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UI/UnityTheme/Effect/Gradient")]
    public class GradientImage : BaseMeshEffect
    {
        public Gradient gradient;

        [SerializeField]
        private GradientDirection _direction;

        public override void ModifyMesh(VertexHelper vh)
        {
            var rect = graphic.rectTransform.rect;
            UIVertex vertex = default;
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);                
                
                var normalized = new Vector2(Math.Abs(vertex.position.x - rect.min.x) < float.Epsilon ? 0 : 1,
                    Math.Abs(vertex.position.y - rect.min.y) < float.Epsilon ? 0 : 1);

                if (_direction == GradientDirection.Horizontal)
                {
                    if (normalized.x == 0)
                    {
                        var minTime = gradient.colorKeys.Min(c => c.time);
                        vertex.color = gradient.colorKeys.First(c => Math.Abs(c.time - minTime) < float.Epsilon).color;
                    } else if (Math.Abs(normalized.x - 1) < float.Epsilon)
                    {
                        var maxTime = gradient.colorKeys.Max(c => c.time);
                        vertex.color = gradient.colorKeys.First(c => Math.Abs(c.time - maxTime) < float.Epsilon).color;
                    }    
                } else if (_direction == GradientDirection.Vertical)
                {
                    if (normalized.y == 0)
                    {
                        var minTime = gradient.colorKeys.Min(c => c.time);
                        vertex.color = gradient.colorKeys.First(c => Math.Abs(c.time - minTime) < float.Epsilon).color;
                    } else if (Math.Abs(normalized.y - 1) < float.Epsilon)
                    {
                        var maxTime = gradient.colorKeys.Max(c => c.time);
                        vertex.color = gradient.colorKeys.First(c => Math.Abs(c.time - maxTime) < float.Epsilon).color;
                    }
                }
                
                vh.SetUIVertex(vertex, i);
            }

            StartCoroutine(MarkAsDirty());
        }
        
        private IEnumerator  MarkAsDirty()
        {
            yield return null;
            graphic.SetVerticesDirty();
        }
    }    

    public enum GradientDirection
    {
        Horizontal,
        Vertical
    }
}