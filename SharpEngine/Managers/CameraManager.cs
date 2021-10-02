using SharpEngine.Components;

namespace SharpEngine
{
    public class CameraManager
    {
        public static Vec2 position = new Vec2(0);
        public static Entity followEntity = null;

        public static void Update(Vec2 windowSize)
        {
            if(followEntity != null && followEntity.GetComponent<TransformComponent>() is TransformComponent tc)
                position = tc.position - windowSize / 2;
        }
    }
}
