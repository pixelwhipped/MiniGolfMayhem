using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.GameElements
{
    public class Node
    {       
        public Vector2 WorldPosition;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public CollisionEvent OnCollision;
        private float _mass;
        public float Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                if (value <= 0) return;
                _mass = value;
            }
        }

        

        public float Radius;
        public float Angle;
        //If Less Than 0 assume all;
        public int CollisionGroup;
        public bool Fixed;
        public Color Color = Color.Red;
        public Node(Vector2 worldPosition, float radius,float mass, Vector2 velocity, Vector2 acceleration,int collisionGroup)
        {      
            WorldPosition = worldPosition;
            Mass = mass;
            Velocity = velocity;
            Acceleration = acceleration;
            Radius = radius;
            CollisionGroup = collisionGroup;
        }

        public void Update(GameTime gameTime)
        {
            if (Fixed) return;
            Velocity += Acceleration;
            WorldPosition += Velocity;
            Velocity *= 0.98f;
            Acceleration *= 0.5f;
         }

        internal void DetectCollisions(IEnumerable<Player> players)
        {
            foreach (var c in players)
            {                
                if(c.Par<0)continue;
                if (c.Node.CollisionGroup != CollisionGroup) continue;
                if (c.Node == this) continue;
                if (!(Vector2.Distance(WorldPosition, c.Node.WorldPosition) < Radius + c.Node.Radius)) continue;
                c.State = PlayerState.Bounced;
                //var collisionPoint = new Vector2(WorldPosition.X*c.Node.Radius + c.Node.WorldPosition.X*Radius);
                CalculateNewVelocities(this, c.Node);
                c.Game.Sounds.Bounce.Play();
                //CalculateNewVelocities(c.Node,this);
            }
        }

        internal void DetectCollisions(List<Node> nodes, List<Rail> rails, Point translationToWorld)
        {
            foreach (var cx in nodes)
            {
                var c = new Node(new Vector2(cx.WorldPosition.X + translationToWorld.X, cx.WorldPosition.Y + translationToWorld.Y), cx.Radius, cx.Mass,
                    cx.Velocity, cx.Acceleration, cx.CollisionGroup)
                {
                    Fixed = cx.Fixed
                };
                if (cx == this) continue;
                if (CollisionGroup > 0 && c.CollisionGroup != CollisionGroup) continue;
                if (!(WorldPosition.X + Radius + c.Radius > c.WorldPosition.X) ||
                    !(WorldPosition.X < c.WorldPosition.X + Radius + c.Radius) ||
                    !(WorldPosition.Y + Radius + c.Radius > c.WorldPosition.Y) ||
                    !(WorldPosition.Y < c.WorldPosition.Y + Radius + c.Radius)) continue;
                if (!(Vector2.Distance(WorldPosition, c.WorldPosition) < Radius + c.Radius)) continue;
                var collisionPoint = new Vector2((WorldPosition.X * c.Radius + c.WorldPosition.X * Radius) / (Radius + c.Radius), (WorldPosition.Y * c.Radius + c.WorldPosition.Y * Radius) / (Radius + c.Radius));
                OnCollision?.Invoke(this, c, collisionPoint);
                CalculateNewVelocities(this, c);
            }
            foreach (var l in rails)
            {
                Vector2 collisionPoint = Vector2.Zero;
                if (this.CollisionGroup > 0 && l.CollisionGroup != CollisionGroup) continue;
                var wp = WorldPosition;
                var loops = (float)Math.Ceiling(Velocity.Length());
                var vel = Velocity/loops;
                var collision = false;
                var ls = new Vector2(l.Start.X + translationToWorld.X, l.Start.Y + translationToWorld.Y);
                var le = new Vector2(l.End.X + translationToWorld.X, l.End.Y + translationToWorld.Y);
                //Do Pixel Per Pixel Test
                for (var i = 0; i < loops; i++)
                {                    
                   
                    if (BallLineCollisionTest(wp, Radius, ls, le, out collisionPoint))
                    {
                        BallLineCollisionTest(WorldPosition, Radius, ls, le, out collisionPoint);
                        collision = true;
                        break;                        
                    }
                    wp += vel;
                }
                if(!collision)continue;
                //Vnew = -2 * (V dot N)*N + V
                //where
                //V = Incoming Velocity Vector
                //N = The Normal Vector of the wall
                var normal = WorldPosition - collisionPoint; // normal of collision plane (not normalised).
                var nn = Vector2.Dot(normal, normal);// square length of normal of collision
                var vn = Vector2.Dot(normal, Velocity);// impact velocity (collision impulse).
                Velocity -= .8f * (2.0f * (vn / nn)) * normal;
                WorldPosition += Velocity;
                OnCollision?.Invoke(this, l, collisionPoint);
            }            
        }

        public void DetectCollisions()
        {
            /*
            foreach (var c in World.nodes)
            {
                if (c == this) continue;
                if (this.CollisionGroup > 0 && c.CollisionGroup != this.CollisionGroup) continue;
                if (!(Location.X + Radius + c.Radius > c.Location.X) ||
                    ! (Location.X < c.Location.X + Radius + c.Radius) ||
                    !(Location.Y + Radius + c.Radius > c.Location.Y) ||
                    !(Location.Y < c.Location.Y + Radius + c.Radius)) continue;
                if (!(Vector2.Distance(Location, c.Location) < Radius + c.Radius)) continue;                
                var collisionPoint = new Vector2((Location.X * c.Radius + c.Location.X * Radius) / (Radius + c.Radius), (Location.Y * c.Radius + c.Location.Y * Radius) / (Radius + c.Radius));
                OnCollision?.Invoke(this, c, collisionPoint);
                CalculateNewVelocities(this, c);
            }
            foreach (var l in World.rails)
            {
                Vector2 collisionPoint;
                if (this.CollisionGroup > 0 && l.CollisionGroup != this.CollisionGroup) continue;
                if (!BallLineCollisionTest(Location, Radius, l.Start , l.End, out collisionPoint)) continue;
                //Vnew = -2 * (V dot N)*N + V
                //where
                //V = Incoming Velocity Vector
                //N = The Normal Vector of the wall
                var normal = Location- collisionPoint; // normal of collision plane (not normalised).
                var nn = Vector2.Dot(normal, normal);// square length of normal of collision
                var vn = Vector2.Dot(normal, Velocity);// impact velocity (collision impulse).
                Velocity -= .8f* (2.0f * (vn / nn)) * normal;                  
                Location += Velocity;
            }*/
        }

        

        public bool BallLineCollisionTest(Vector2 location, float radius, Vector2 start, Vector2 end, out Vector2 collisionPoint)
        {
            var a = start;
            var b = end;
            var ac = new Vector2(location.X, location.Y);
            ac = ac - a;
            var ab = new Vector2(b.X, b.Y);
            ab = ab - a;
            var ab2 = Vector2.Dot(ab, ab);
            var acab = Vector2.Dot(ac, ab);
            var t = acab / ab2;

            if (t < 0.0)
                t = 0.0f;
            else if (t > 1.0)
                t = 1.0f;

            //P = A + t * AB; 
            collisionPoint = new Vector2(ab.X, ab.Y);
            collisionPoint = collisionPoint * t;
            collisionPoint = collisionPoint + a;

            var h = new Vector2(collisionPoint.X, collisionPoint.Y);
            h = h - location;
            double h2 = Vector2.Dot(h, h);
            double r2 = radius * radius;

            return !(h2 > r2);
        }

        private static void CalculateNewVelocities(Node a, Node b)
        {
            var mass1 = a.Mass;
            var mass2 = b.Mass;
            var velX1 = a.Velocity.X;
            var velX2 = b.Velocity.X;
            var velY1 = a.Velocity.Y;
            var velY2 = b.Velocity.Y;

            var newVelX1 = (velX1*(mass1 - mass2) + 2*mass2*velX2)/(mass1 + mass2);
            var newVelX2 = (velX2*(mass2 - mass1) + 2*mass1*velX1)/(mass1 + mass2);
            var newVelY1 = (velY1*(mass1 - mass2) + 2*mass2*velY2)/(mass1 + mass2);
            var newVelY2 = (velY2*(mass2 - mass1) + 2*mass1*velY1)/(mass1 + mass2);

            a.Velocity = a.Fixed ? Vector2.Zero : new Vector2(newVelX1, newVelY1);
            b.Velocity = b.Fixed ? Vector2.Zero : new Vector2(newVelX2, newVelY2);
            a.WorldPosition += a.Velocity;
            b.WorldPosition += b.Velocity;
        }

        public void Draw(SpriteBatch batch, Vector2 worldTranslation) => batch.DrawCircle(WorldPosition + worldTranslation, Radius, Color,1);
    }
}


