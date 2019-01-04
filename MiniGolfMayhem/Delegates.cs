using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MiniGolfMayhem
{
    public delegate void Setter<in TValue>(TValue value);

    public delegate TValue Getter<out TValue>();

    public delegate void Procedure<in TValue>(TValue value);

    public delegate void Operation<in TValue>(TValue a, TValue b);

    public delegate void CollisionEvent(object a, object b, Vector2 c);

    public delegate void Routine();    
}
