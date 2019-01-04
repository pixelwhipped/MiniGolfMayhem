using System;
using System.Linq;
using Windows.ApplicationModel.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Graphics
{
    public class PlayerSave
    {
        public string Name { get; set; }
        public int ColorIndex { get; set; }
        public int Par { get; set; }
        public int Total { get; set; }
        public Point LastTile { get; set; }
        public int Layer { get; set; }
        public PlayerState State { get; set; }
        public Point CurrentTile { get; set; }
        public Vector2 Position { get; set; }
    }
    public class Player
    {
        public Player(Golf game, PlayerSave saved):this(game,saved.ColorIndex,saved.Name)
        {
            Par = saved.Par;
            Total = saved.Total;
            PreviousTile = saved.LastTile;
            CurrentTile = saved.CurrentTile;
            //Layer = saved.Layer;            
           // State = saved.State;
          //  Node.WorldPosition = saved.Position;
        }
        public Player(Golf game, int color, string name)
        {
            ColorIndex = color;
            Game = game;
            Name = name;
            GolfBall = new Sprite(Game, 3, new TimeSpan(0, 0, 0, 0, 200), AnimationState.LoopPlay, Textures.GolfBall, 0,
                2);
            State = PlayerState.Finished;
            Power = new Tween(new TimeSpan(0, 0, 0, 1), 0f, 4f, true);            
            Game.UnifiedInput.TapListeners.Add(OnTap);            
            
        }

        public bool BouncePlayed { get; set; }

        public Golf Game { get; set; }

        public TimeSpan TeleportDelay { get; set; }

        public int ColorIndex { get; set; }

        public Color Color
        {
            get { return GameColors.Colors[ColorIndex].Color; }
            set
            {
                var color = GameColors.Colors.ToList().FindIndex(c => c.Color == value);
                if (color > 0) ColorIndex = color;
            }
        }

        internal Node Node { get; set; }

        public int Layer
        {
            get { return Node.CollisionGroup; }
            set { Node.CollisionGroup = value; }
        }

        public int LastImidiateLayer { get; set; }
        public int LastLayer { get; set; }
        public int PreviousLayer { get; set; }
        public Vector2 Position => Node.WorldPosition;

        public Vector2 Acceleration
        {
            get { return Node.Acceleration; }
            set { Node.Acceleration = value; }
        }

        public Vector2 Velocity
        {
            get { return Node.Velocity; }
            set { Node.Velocity = value; }
        }

        public Point CurrentTile { get; set; }
        public Point PreviousTile { get; set; }
        public Point LastTile { get; set; }

        public PlayerState State { get; set; }

        public int OnTile { get; set; }

        public int Par { get; set; }
        public int Total { get; set; }
        public string Name { get; set; }

        public Sprite GolfBall { get; set; }

        public TimeSpan IdleTime { get; set; } = TimeSpan.Zero;

        public Vector2? WaterPosition { get; set; }

        public Vector2 LastPosistion { get; set; }
        public Rectangle Teleport { get; set; }
        public bool Teleporting { get; set; }

        public float Rotation { get; set; }
        public Tween Power { get; set; }

        public void Start()
        {
            State = PlayerState.Ready;
            Total++;
            Par++;
        }

        private void OnTap(Vector2 value)
        {
            if (State != PlayerState.Ready || Game.Transitioning) return;
            State = PlayerState.Started;
            var r = Rotation;
            Acceleration = new Vector2((float) Math.Cos(r), (float) Math.Sin(r))*MathHelper.Clamp(Power, 0.1f, 4f);
            Game.Sounds.Swing.Play();
            Game.Sounds.Bounce.Play();
        }

        public Vector2 Location
        {
            get
            {
                return Game.UnifiedInput.Location;
            }
        }

        public void Update(GameTime gameTime, Map map)
        {
            try
            {
                if (State == PlayerState.Done) return;
                if (WaterPosition != null) return;
               
                LastPosistion = Node.WorldPosition - Node.Velocity*5;
                if (Teleporting && TeleportDelay > TimeSpan.Zero)
                {
                    TeleportDelay -= gameTime.ElapsedGameTime;
                    return;
                }
                if ((State == PlayerState.Ready || State == PlayerState.Bounced) && Velocity.Length() > 0.01f)
                {
                    State = State == PlayerState.Bounced ? PlayerState.Bounced : PlayerState.Started;
                    IdleTime = TimeSpan.Zero;
                }
                if ((State == PlayerState.Started || State == PlayerState.Bounced) && Velocity.Length() < 0.09f)
                {
                    IdleTime += gameTime.ElapsedGameTime;
                }
                else
                {
                    IdleTime = TimeSpan.Zero;
                }
                if (State == PlayerState.Started || State == PlayerState.Bounced) GolfBall.Update(gameTime);
                if (IdleTime > new TimeSpan(0, 0, 0, 3))
                {
                    State = PlayerState.Finished;
                    Velocity = Vector2.Zero;
                }
                if (State == PlayerState.Ready)
                {
                    Power.Update(gameTime.ElapsedGameTime);
                    return;
                }

                if (map.GetTile(this, 1) == 0 && map.GetTile(this, 2) == 0) //Out of map
                {
                    Node = new Node(map.StartWorldCenter, 2.5f, 1, Vector2.Zero, Vector2.Zero, map.StartLayer);
                    State = PlayerState.Finished;
                    PreviousTile = map.GetPlayerTile(this);
                    LastTile = PreviousTile;
                }

                
                OnTile = map.GetTile(this, Layer);


                CurrentTile = map.GetPlayerTile(this);
                var tileToWorldTranslation = new Point(CurrentTile.X*map.TileSet.TileWidth,CurrentTile.Y*map.TileSet.TileWidth);
                var pointOnTile = new Vector2(Node.WorldPosition.X - tileToWorldTranslation.X, Node.WorldPosition.Y - tileToWorldTranslation.Y);

                var walls = map.GetWalls(this);
                if (OnTile == 53 || OnTile == 54 || OnTile == 62 || OnTile == 63)
                {
                    walls =
                        walls.Union(
                            WorldHelpers.AddWallRails(
                                WorldHelpers.GetLineSpecialSegments(OnTile,
                                    new Point((int)(Node.WorldPosition.X - tileToWorldTranslation.X),
                                        (int)(Node.WorldPosition.Y - tileToWorldTranslation.Y))), Layer)).ToList();
                }
                Node.Update(gameTime);

                walls = map.GetWalls(this);
                if (OnTile == 53 || OnTile == 54 || OnTile == 62 || OnTile == 63)
                {
                    walls =
                        walls.Union(
                            WorldHelpers.AddWallRails(
                                WorldHelpers.GetLineSpecialSegments(OnTile,
                                    new Point((int)(Node.WorldPosition.X - tileToWorldTranslation.X),
                                        (int)(Node.WorldPosition.Y - tileToWorldTranslation.Y))), Layer)).ToList();
                }

                if (map.EndLayer == Layer && Vector2.Distance(map.EndWorldCenter, Node.WorldPosition) < 5 + Node.Radius)
                {
                    if (Node.Velocity.Length() < 3.5f)
                    {
                        Node = new Node(map.StartWorldCenter, 2.5f, 1, Vector2.Zero, Vector2.Zero, map.StartLayer);
                        State = PlayerState.Done;
                        Game.Sounds.Hole.Play();
                    }
                }

                #region Elevations

                LastImidiateLayer = Layer;
                // Layer = WorldHelpers.LayerMod()
                if (LastTile != CurrentTile)
                {
                    BouncePlayed =
                        !(OnTile == 53 && !(pointOnTile.Y > 23) || OnTile == 54 && !(pointOnTile.Y > 72) ||
                          OnTile == 62 && !(pointOnTile.X > 72) || OnTile == 63 && !(pointOnTile.X < 23));
                    PreviousLayer = LastLayer;
                    LastLayer = Layer;
                    //Need to test elevation vertical horizontal
                    if (Layer == 1 && map.GetElevation(LastTile) != Elevation.Flat)
                    {
                        if (WorldHelpers.IsTopLevel(map.Layer2.GetTile(CurrentTile.X, CurrentTile.Y)) &&
                            PreviousLayer != 2)
                        {
                            Layer = 2;
                        }
                    }
                    else if (Layer == 2 && map.GetElevation(CurrentTile) != Elevation.Flat)
                    {
                        Layer = 1;
                    }

                    PreviousTile = LastTile;
                    LastTile = CurrentTile;
                }
                if (!BouncePlayed && (OnTile == 53 || OnTile == 54 || OnTile == 62 || OnTile == 63))
                {
                    switch (OnTile)
                    {
                        case 53:
                        {
                            if (pointOnTile.Y < 23)
                            {
                                BouncePlayed = true;
                                Game.Sounds.DropBounce.Play();
                            }
                            break;
                        }
                        case 54:
                        {
                            if (pointOnTile.Y > 72)
                            {
                                BouncePlayed = true;
                                Game.Sounds.DropBounce.Play();
                            }
                            break;
                        }
                        case 62:
                        {
                            if (pointOnTile.X > 72)
                            {
                                BouncePlayed = true;
                                Game.Sounds.DropBounce.Play();
                            }
                            break;
                        }
                        case 63:
                        {
                            if (pointOnTile.X < 23)
                            {
                                BouncePlayed = true;
                                Game.Sounds.DropBounce.Play();
                            }
                            break;
                        }
                    }
                }
                if (Layer == 1)
                {
                    Node.Acceleration += WorldHelpers.GetForce(map.Layer1.GetTile(CurrentTile.X, CurrentTile.Y),
                        pointOnTile);
                }
                

                #endregion

                Node.DetectCollisions(map.GetBouncers(this), walls, tileToWorldTranslation);
                Node.DetectCollisions(map.Players.Where(p => p.State != PlayerState.Done));
                if (
                    map.GetWater(this)
                        .Any(
                            r =>
                                r.Contains(pointOnTile)))
                {
                    WaterPosition = Node.WorldPosition;
                    Sprites.Splash.OnFinish += SplashFinished;
                    Sprites.Splash.Animation = AnimationState.Play;
                    Game.Sounds.Splash.Play();
                }
                var ballTileRect = new Rectangle((int) (Node.WorldPosition.X - tileToWorldTranslation.X),
                    (int) (Node.WorldPosition.Y - tileToWorldTranslation.Y), (int) GolfBall.Width, (int) GolfBall.Height);
                var rects = map.GetTeleports(this);
                if (!Teleporting && rects.Any())
                {
                    var exits = rects.Where(r => r.Key.Intersects(ballTileRect)).SelectMany(r => r.Value).ToList();
                    if (exits.Any())
                    {
                        var v = Golf.Random.Next(exits.Count);
                        var r = exits[v];
                        //  r = new Rectangle(39, 46, 6, 10);
                        Teleport = new Rectangle(r.X + tileToWorldTranslation.X, r.Y + tileToWorldTranslation.Y,
                            r.Width,
                            r.Height);

                        Node.Velocity = WorldHelpers.GetTeleporterVelocity(68, r.Center); //will need to get tile type
                        Node.WorldPosition = new Vector2(Teleport.Center.X, Teleport.Center.Y);
                        TeleportDelay = new TimeSpan(0, 0, 0, 0, 500);
                        Teleporting = true;
                        Game.Sounds.Bouncing.Play();
                    }
                }
                else
                {
                    Teleporting =
                        Teleport.Intersects(new Rectangle(ballTileRect.X + tileToWorldTranslation.X,
                            ballTileRect.Y + tileToWorldTranslation.Y, (int) GolfBall.Width, (int) GolfBall.Height));
                }
            }
            catch (Exception)
            {
                //Somthing bad happened reset
                Node =
                    new Node(
                        new Vector2(Math.Max(map.Start.X, 0)*map.TileSet.TileWidth,
                            Math.Max(map.Start.Y, 0)*map.TileSet.TileHeight) +
                        WorldHelpers.GetStartOffset(map.StartSide), 2.5f, 1, Vector2.Zero, Vector2.Zero, map.StartLayer);
                State = PlayerState.Finished;
                PreviousTile = map.GetPlayerTile(this);
                LastTile = PreviousTile;
            }
        }

        private void SplashFinished()
        {
            if (WaterPosition == null) return;
            Node.Velocity = Vector2.Zero;
            State = PlayerState.Finished;
            Node.WorldPosition = LastPosistion;
            Sprites.Splash.Animation = AnimationState.Pause;
            Sprites.Splash.ToStart();
            WaterPosition = null;
        }

        public void Draw(SpriteBatch batch, Vector2 worldTranslation)
        {
            if (Game.Transitioning) return;
            if (Par < 0 || State == PlayerState.Done) return;
            if (WaterPosition != null)
            {
                Sprites.Splash.Draw(batch, (Vector2) WaterPosition + worldTranslation - new Vector2(10, 19), 0.9f);
            }
            else
            {
                var pos = Node.WorldPosition + worldTranslation - GolfBall.Center;
                GolfBall.Draw(batch, Node.WorldPosition + worldTranslation - GolfBall.Center/2f,
                    Layer == 1 ? 0.5f : 0.71f, Color.Black*0.25f);
                GolfBall.Draw(batch, pos, Layer == 1 ? 0.55f : 0.75f, Color);
                if (State != PlayerState.Ready) return;
                
                var direction = Location - pos;
                Rotation = (float) Math.Atan2(direction.Y, direction.X);
                batch.Draw(Textures.Starter, pos + GolfBall.Center, null, null, new Vector2(15, 5),
                    Rotation - (float) (Math.PI*0.5f),
                    new Vector2(1, 1f + Power/4f), Color.White*0.4f, SpriteEffects.None, Layer == 1 ? 0.49f : 0.7f);
                batch.Draw(Textures.StarterTop, pos + GolfBall.Center, null, null, new Vector2(15, 5),
                    Rotation - (float) (Math.PI*0.5f),
                    new Vector2(1, 1f + Power/4f), Color.White*0.4f, SpriteEffects.None, 1f);
            }
        }
    }
}