using Unity.Entities;
using UnityEngine;

public class AppleAuthoring : MonoBehaviour
{
    public float appleFallSpeed;
    public float appleBoundY;
    
    private class AppleBaker : Baker<AppleAuthoring>
    {
        public override void Bake(AppleAuthoring authoring)
        {
            var propertiesComponent = new AppleProperties
            {
                AppleFallSpeed = authoring.appleFallSpeed,
                AppleBoundY = authoring.appleBoundY
            };
            
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct AppleProperties : IComponentData
{
    public float AppleFallSpeed;
    public float AppleBoundY;
}
