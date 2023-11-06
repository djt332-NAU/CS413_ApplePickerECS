using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class AppleTreeAuthoring : MonoBehaviour
{
    public float speed;
    public float leftAndRightEdge;
    public float changeDirectionChance;
    public float appleDropDelay;
    
    public int numBaskets;
    public float basketBottomY;
    public float basketSpacingY;
    
    public GameObject applePrefab;
    public GameObject basketPrefab;
    
    private class AppleTreeBaker : Baker<AppleTreeAuthoring>
    {
        public override void Bake(AppleTreeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new AppleTreeProperties
            {
                Speed = authoring.speed,
                LeftAndRightEdge = authoring.leftAndRightEdge,
                ChangeDirectionChance = authoring.changeDirectionChance,
                AppleDropDelay = authoring.appleDropDelay,
                NumBaskets = authoring.numBaskets,
                BasketBottomY = authoring.basketBottomY,
                BasketSpacingY = authoring.basketSpacingY,
                ApplePrefab = GetEntity(authoring.applePrefab, 
                                        TransformUsageFlags.Dynamic),
                BasketPrefab = GetEntity(authoring.basketPrefab, 
                                         TransformUsageFlags.Dynamic),
                Random = Random.CreateFromIndex((uint) entity.Index)
            };
            
            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct AppleTreeProperties : IComponentData
{
    public float Speed;
    public float LeftAndRightEdge;
    public float ChangeDirectionChance;
    public float AppleDropDelay;
    
    public int NumBaskets;
    public float BasketBottomY;
    public float BasketSpacingY;
    
    public Entity ApplePrefab;
    public Entity BasketPrefab;

    public Random Random;
}
