using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.GameElements
{
    public class World
    {
        public Rectangle Bounds;
        public List<Node> Nodes = new List<Node>();        
        public List<Rail> Rails = new List<Rail>();
        public List<Rectangle> Water = new List<Rectangle>();

        public List<KeyValuePair<Rectangle, List<Rectangle>>> Teleports =
            new List<KeyValuePair<Rectangle, List<Rectangle>>>();

        public List<KeyValuePair<Rectangle, Vector2>> Forces = new List<KeyValuePair<Rectangle, Vector2>>();

        public void Update(GameTime gameTime)
        {
            foreach (var b in Nodes)
                b.Update(gameTime);
          //  foreach (var b in Nodes)
          //      b.DetectCollisions();
        }

        public Node PickNode(Vector2 mousePos, float maxDist = 20) => Nodes.FirstOrDefault(node => Vector2.Distance(node.WorldPosition, mousePos) < maxDist);

        public void Draw(SpriteBatch batch, Vector2 posistion)
        {
            foreach (var l in Rails)
                batch.DrawLine(l.Start+posistion, l.End+posistion, Color.Pink, 1);
            foreach (var b in Nodes)
                b.Draw(batch,posistion);
        }
    }
}
