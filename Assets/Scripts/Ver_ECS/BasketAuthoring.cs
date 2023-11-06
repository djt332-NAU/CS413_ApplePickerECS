using Unity.Entities;
using UnityEngine;

public class BasketAuthoring : MonoBehaviour
{   
    private class BasketBaker : Baker<BasketAuthoring>
    {
        public override void Bake(BasketAuthoring authoring)
        {
            var propertiesComponent = new BasketProperties
            {
            };
            
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct BasketProperties : IComponentData
{
}
