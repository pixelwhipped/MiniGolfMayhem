using Microsoft.Xna.Framework;

namespace MiniGolfMayhem.GameElements
{
    public class Rail
    {
        public Vector2 Start;
        public Vector2 End;
       // public World World;
        public int CollisionGroup;
        public Rail(Vector2 start, Vector2 end,int collisionGroup)            
        {
            //World = world;           
            Start = start;
            End = end;
            CollisionGroup = collisionGroup;
        }        
    }
}
