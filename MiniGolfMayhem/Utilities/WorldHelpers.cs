using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Devices;
using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Utilities
{
    public static class WorldHelpers
    {

        public static int[] TopTiles = {82, 83, 84, 85, 91, 100, 109};

        public static int[] BottomTiles =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            12, 13, 14, 15, 16, 17, 18,
            19, 20, 21, 22, 23, 24, 25, 26, 27,
            28, 29, 30, 31, 32, 33, 34, 35,
            37, 38, 39, 40, 41, 42, 43, 44,
            46, 47, 48, 49, 50, 51, 52,53,54,
            55, 56, 57, 58, 59, 60, 61,62,63,
            64, 65, 66, 67, 68, 69, 70,
            73, 74, 75, 76, 77, 78,92,93,101,102
        };

        public static int[] Ramps =
        {
            4, 5, 6, 7, 13, 14, 15, 16, 22, 23, 31, 32, 35, 40, 41, 44
        };

        public static int[] ConnectionsRightNormalLow = 
        {
            2, 3,20, 21, 22, 24, 26, 38, 39, 42, 46, 47, 56, 59, 60, 61, 68, 73, 75, 76,92,62,63,102
        };

        public static int[] ConnectionsLeftNormalLow = 
        {
            1, 2, 19, 20, 23, 25, 26, 37, 39, 43, 46, 47, 55, 59, 60, 61, 68, 73, 75, 76,92,62,63,102
        };

        public static int[] ConnectionsTopNormalLow = 
        {
            1, 3, 10, 12, 13, 27, 33, 37, 38, 47, 48, 51, 59, 65, 68, 69, 70, 74, 77, 78,93,53,54,101
        };

        public static int[] ConnectionsBottomNormalLow = 
        {
            4, 10, 12, 19, 21, 27, 34, 37, 38, 46, 48, 52, 59, 64, 68, 69, 70, 74, 77, 78,93,101,53,54
        };

        public static int StartBottom = 86;
        public static int StartTop = 87;
        public static int StartRight = 88;
        public static int StartLeft = 89;

        public static Vector2 GetStartOffset(Side side)
        {
            switch (side)
            {
                case Side.Top:
                    return new Vector2(48,39);                    
                case Side.Bottom:
                    return new Vector2(48, 57);
                case Side.Right:
                    return new Vector2(57,48);
                case Side.Left:
                    return new Vector2(39, 48);
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public static List<Side> GetOpenSides(int tile,int layer)
        {
            switch (tile)
            {
                case 1: return new List<Side>{Side.Bottom,Side.Right};
                case 2: return new List<Side> { Side.Left, Side.Right };
                case 3: return new List<Side> { Side.Bottom, Side.Left };
                case 4:
                    return new List<Side> {layer == 1 ? Side.Top : Side.Bottom};
                case 5: return new List<Side> { layer == 1 ? Side.Top : Side.Bottom, Side.Right };
                case 6: return new List<Side> { layer == 1 ? Side.Top : Side.Bottom, Side.Right, Side.Left };
                case 7: return new List<Side> { layer == 1 ? Side.Top : Side.Bottom, Side.Left };
                case 8: return new List<Side> { Side.Bottom, Side.Right };
                case 9: return new List<Side> { Side.Bottom, Side.Left };
                case 10: return new List<Side> { Side.Bottom, Side.Top };
                case 12: return new List<Side> { Side.Bottom, Side.Top };

                case 13: return new List<Side> { layer == 1 ? Side.Bottom : Side.Top };
                case 14: return new List<Side> { layer == 1 ? Side.Bottom : Side.Top,  Side.Right };
                case 15: return new List<Side> { layer == 1 ? Side.Bottom : Side.Top,  Side.Right, Side.Left };
                case 16: return new List<Side> { layer == 1 ? Side.Bottom : Side.Top, Side.Left };
                case 17: return new List<Side> { Side.Top, Side.Right };
                case 18: return new List<Side> { Side.Top, Side.Left };
                case 19: return new List<Side> { Side.Top, Side.Right };
                case 20: return new List<Side> { Side.Left, Side.Right };
                case 21: return new List<Side> { Side.Top, Side.Left };
                case 22: return new List<Side> { layer == 1 ? Side.Left : Side.Right };
                case 23: return new List<Side> { layer == 1 ? Side.Right : Side.Left };
                case 24: return new List<Side> { Side.Left};
                case 25: return new List<Side> { Side.Right };
                case 26: return new List<Side> { Side.Left, Side.Right };
                case 27: return new List<Side> { Side.Top, Side.Bottom };

                case 28: return new List<Side> { Side.Bottom, Side.Top, Side.Right };
                case 29: return new List<Side> { Side.Bottom, Side.Top, Side.Right, Side.Left };
                case 30: return new List<Side> { Side.Bottom, Side.Top, Side.Left };
                case 31: return new List<Side> { Side.Bottom, layer == 1 ? Side.Left : Side.Right };
                case 32: return new List<Side> { Side.Bottom, layer == 1 ? Side.Right : Side.Left };
                case 33: return new List<Side> { Side.Bottom};
                case 34: return new List<Side> { Side.Top };
                case 35: return new List<Side> { Side.Bottom, Side.Top, layer == 1 ? Side.Right : Side.Left };
                case 37: return new List<Side> { Side.Bottom, Side.Top, Side.Right };
                case 38: return new List<Side> { Side.Bottom, Side.Top, Side.Left };
                case 39: return new List<Side> { Side.Left, Side.Right };
                case 40: return new List<Side> { Side.Top, layer == 1 ? Side.Left : Side.Right };
                case 41: return new List<Side> { Side.Top, layer == 1 ? Side.Right : Side.Left };
                case 42: return new List<Side> { Side.Left };
                case 43: return new List<Side> { Side.Right };
                case 44: return new List<Side> { Side.Bottom, Side.Top, layer == 1 ? Side.Left : Side.Right };
                case 46: return new List<Side> { Side.Top, Side.Right, Side.Left };
                case 47: return new List<Side> { Side.Bottom, Side.Right, Side.Left };
                case 48: return new List<Side> { Side.Top, Side.Bottom };
                case 49: return new List<Side> { Side.Top, Side.Right, Side.Left };
                case 50: return new List<Side> { Side.Bottom, Side.Right, Side.Left };
                case 51: return new List<Side> { Side.Bottom};
                case 52: return new List<Side> { Side.Top};
                case 53: return new List<Side> { Side.Bottom, Side.Top };
                case 54: return new List<Side> { Side.Bottom, Side.Top };
                case 55: return new List<Side> { Side.Top , Side.Bottom,Side.Right ,Side.Left};
                case 56: return new List<Side> { Side.Top, Side.Bottom,Side.Right, Side.Left };
                case 57: return new List<Side> { Side.Bottom, Side.Right };
                case 58: return new List<Side> { Side.Bottom, Side.Left };
                case 59: return new List<Side> { Side.Bottom, Side.Top, Side.Right, Side.Left };
                case 60: return new List<Side> { Side.Right, Side.Left };
                case 61: return new List<Side> { Side.Right, Side.Left };
                case 62: return new List<Side> { Side.Right, Side.Left };
                case 63: return new List<Side> { Side.Right, Side.Left };
                case 64: return new List<Side> { Side.Right, Side.Left, Side.Top, Side.Bottom };
                case 65: return new List<Side> { Side.Right, Side.Left, Side.Top, Side.Bottom };
                case 66: return new List<Side> { Side.Top, Side.Right };
                case 67: return new List<Side> { Side.Top, Side.Left };
                case 68: return new List<Side> { Side.Bottom, Side.Top, Side.Right, Side.Left };
                case 69: return new List<Side> { Side.Bottom, Side.Top };
                case 70: return new List<Side> { Side.Bottom, Side.Top };

                case 73: return new List<Side> { Side.Right, Side.Left };
                case 74: return new List<Side> { Side.Bottom, Side.Top };
                case 75: return new List<Side> { Side.Right, Side.Left };
                case 76: return new List<Side> { Side.Right, Side.Left };
                case 77: return new List<Side> { Side.Bottom, Side.Top };
                case 78: return new List<Side> { Side.Bottom, Side.Top };

                case 82: return new List<Side> { Side.Bottom, Side.Top };
                case 83: return layer == 1?new List<Side> { Side.Bottom, Side.Top }:new List<Side> { Side.Bottom, Side.Top, Side.Right };
                case 84: return new List<Side> { Side.Bottom, Side.Top, Side.Right, Side.Left };
                case 85: return layer == 1?new List<Side> { Side.Bottom, Side.Top }:new List<Side> { Side.Bottom, Side.Top, Side.Left }; ;
                case 91: return new List<Side> { Side.Right, Side.Left };
                case 92: return new List<Side> { Side.Right, Side.Left };
                case 93: return new List<Side> { Side.Bottom, Side.Top };

                case 100: return new List<Side> { Side.Right, Side.Left, Side.Bottom };
                case 101: return new List<Side> { Side.Bottom, Side.Top };
                case 102: return new List<Side> { Side.Left, Side.Right };
                case 109: return new List<Side> { Side.Right, Side.Left, Side.Top};

            }
            return new List<Side>();
        } 
        public static int GetStartTile(Side side)
        {
            switch (side)
            {
                case Side.Top:
                    return StartTop;
                case Side.Bottom:
                    return StartBottom;
                case Side.Left:
                    return StartLeft;
                case Side.Right:
                    return StartRight;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public static int EndBottom = 95;
        public static int EndTop = 96;
        public static int EndRight = 97;
        public static int EndLeft = 98;

        public static Vector2 GetEndOffset(Side side)
        {
            switch (side)
            {
                case Side.Top:
                    return new Vector2(48, 39);
                case Side.Bottom:
                    return new Vector2(48, 57);
                case Side.Right:
                    return new Vector2(57, 48);
                case Side.Left:
                    return new Vector2(39, 48);
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public static int GetEndTile(Side side)
        {
            switch (side)
            {
                case Side.Top:
                    return EndTop;
                case Side.Bottom:
                    return EndBottom;
                case Side.Left:
                    return EndLeft;
                case Side.Right:
                    return EndRight;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public static int LayerMod(int tile, Vector2 pos)
        {
            switch (tile)
            {
                case 4:
                case 5:
                case 6:
                case 7:
                {
                    if (pos.Y > 48)
                    {
                        return 2;
                    }
                    break;
                }
                case 13:
                case 14:
                case 15:
                case 16:
                    {
                        if (pos.Y < 48)
                        {
                            return 2;
                        }
                        break;
                    }
                case 23:
                {
                        if (pos.X > 48)
                        {
                            return 2;
                        }
                    break;
                }
                case 24:
                    {
                        if (pos.X < 48)
                        {
                            return 2;
                        }
                        break;
                    }
                case 31:
                    {
                        if (pos.X > 48)
                        {
                            return 2;
                        }
                        break;
                    }
                case 32:
                    {
                        if (pos.X < 48)
                        {
                            return 2;
                        }
                        break;
                    }
                case 35:
                    {
                        if (pos.X < 48)
                        {
                            return 2;
                        }
                        break;
                    }
                case 40:
                    {
                        if (pos.X > 48)
                        {
                            return 2;
                        }
                        break;
                    }
                case 41:
                    {
                        if (pos.X < 48)
                        {
                            return 2;
                        }
                        break;
                    }
                case 44:
                    {
                        if (pos.X > 48)
                        {
                            return 2;
                        }
                        break;
                    }
                default:
                    return 1;
            }
            return 1;
        }

        public static List<KeyValuePair<Vector2, Vector2>> GetLineSpecialSegments(int tile, Point pos)
        {
            var ret = new List<KeyValuePair<Vector2, Vector2>>();
            switch (tile)
            {
                case 53:
                {
                        if(pos.Y<23)
                        ret.AddRange(CreateSegments(new[] { 0,24,96,24}));
                        break;
                }
                case 54:
                    {
                        if (pos.Y > 72)
                            ret.AddRange(CreateSegments(new[] { 0, 71, 96, 71 }));
                        break;
                    }
                case 62:
                    {
                        if (pos.X > 72)
                            ret.AddRange(CreateSegments(new[] { 71, 0, 71, 96 }));
                        break;
                    }
                case 63:
                    {
                        if (pos.X < 23)
                            ret.AddRange(CreateSegments(new[] { 24, 0, 24, 96 }));
                        break;
                    }
            }
            return ret;
        }
        public static List<KeyValuePair<Vector2, Vector2>> GetLineSegments(int tile)
        {
            var ret = new List<KeyValuePair<Vector2, Vector2>>();
            switch (tile)
            {
                case 1: //Top Left Round Corner
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 42, 25, 27, 42, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {78, 96, 78, 90, 82, 82, 90, 78, 96, 78}));
                    break;
                }
                case 2: //Horizontal 2 half walls
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 66, 17, 66, 47, 71, 47, 71, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {0, 78, 15, 78, 15, 48, 20, 48, 20, 78, 96, 78}));
                    break;
                }
                case 3: //Top Right Round Corner
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(1), 96));
                    break;
                }
                case 4: //Center Vertical Rise
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 0}));
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 96}));
                    break;
                }
                case 5: //Left Vertical Rise Low Top
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 0}));
                    break;
                }
                case 6: // Vertical Rise Low Top
                {
                    break;
                }
                case 7: // Right Vertical Rise Low Top
                {
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 96}));
                    break;
                }
                case 8: //Round Top Left
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 79, 25, 59, 34, 45, 48, 32, 70, 21, 82, 17, 96, 17}));
                    break;
                }
                case 9: //Round Top Right
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(8), 96));
                    break;
                }
                case 10: //Verticle 2 half walls
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 29, 47, 29, 47, 24, 17, 24, 17, 0}));
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 75, 48, 75, 48, 81, 78, 81, 78, 96}));
                    break;
                }
                case 11: //Background
                {
                    break;
                }
                case 12: //Vertical
                {
                    ret.AddRange(GetLineSegments(4));
                    break;
                }
                case 13: //Center Vertical Rise Low Bottom
                {
                    ret.AddRange(GetLineSegments(4));
                    break;
                }
                case 14: //Left Vertical Rise Low Bottom
                {
                    ret.AddRange(GetLineSegments(5));
                    break;
                }
                case 15: //Vertical Rise Low Bottom
                {
                    break;
                }
                case 16: //Right Vertical Rise Low Bottom
                {
                    ret.AddRange(GetLineSegments(7));
                    break;
                }
                case 17: //Round Bottom Left
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(8), 96));
                    break;
                }
                case 18: //Round Bottom Right
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(9), 96));
                    break;
                }
                case 19: //Bottom Left Round Corner
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(1), 96));
                    break;
                }
                case 20: //Horizontal
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {0, 78, 96, 78}));
                    break;
                }
                case 21: //Bottom Right Round Corner
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(19), 96));
                    break;
                }
                case 22: //Horizontal Rise Left to Right
                {
                    ret.AddRange(GetLineSegments(20));
                    break;
                }
                case 23: //Horizontal Rise Right to Left
                {
                    ret.AddRange(GetLineSegments(20));
                    break;
                }
                case 24: //Horizontal End Right Cap
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 69, 17, 69, 78, 0, 78}));
                    break;
                }
                case 25: //Horizontal End Left Cap
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(24), 96));
                    break;
                }
                case 26: //Bouncers Horizontal
                {
                    ret.AddRange(GetLineSegments(20));
                    break;
                }
                case 27: //Bouncers Vertical
                {
                    ret.AddRange(GetLineSegments(4));
                    break;
                }
                case 28: //Vertical Left
                {
                    ret.AddRange(GetLineSegments(5));
                    break;
                }
                case 29:
                {
                    break;
                }
                case 30: //Vertical Right
                {
                    ret.AddRange(GetLineSegments(7));
                    break;
                }
                case 31: //Horizontal Top Rise Left to Right
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 96, 17}));
                    break;
                }
                case 32: //Horizontal Top Rise Right to Left
                {
                    ret.AddRange(GetLineSegments(31));
                    break;
                }
                case 33: //Vertical Top End Cap
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 26, 78, 26, 78, 96}));
                    break;
                }
                case 34: //Vertical Botton End Cap
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(33), 96));
                    break;
                }
                case 35: //Horizontal Rise Right to Left
                {
                    break;
                }
                case 36: //Empty
                {
                    break;
                }
                case 37: //Vertical T Section Exit Right
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 0}));
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {78, 96, 78, 78, 96, 78}));
                    break;
                }
                case 38: //Vertical T Section Exit Left
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(37), 96));
                    break;
                }
                case 39: //Horizontal mid walls
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 46, 17, 46, 42, 50, 42, 50, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {0, 78, 46, 78, 46, 56, 50, 56, 50, 78, 96, 78}));
                    break;
                }
                case 40: //Horizontal Bottom Rise Left to Right
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(31), 96));
                    break;
                }
                case 41: //Horizontal Bottom Rise Right to Left
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(31), 96));
                    break;
                }
                case 42: //Horizontal Rise Right End Cap
                {
                    ret.AddRange(GetLineSegments(24));
                    break;
                }
                case 43: //Horizontal Rise Left End Cap
                {
                    ret.AddRange(GetLineSegments(25));
                    break;
                }
                case 44: //Horizontal Rise Left to Right
                {
                    break;
                }
                case 45: //Empty
                {
                    break;
                }
                case 46: //Horizontal T Section exit Top
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 17, 17, 17, 0}));
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {0, 78, 96, 78}));
                    break;
                }
                case 47: //Horizontal T Section Exit Bottom
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(46), 96));
                    break;
                }
                case 48: //Vertical Mid Wall
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 50, 42, 50, 42, 47, 17, 47, 17, 0}));
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 47, 56, 47, 56, 50, 78, 50, 78, 96}));
                    break;
                }
                case 49: //Horizontal Bottom
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(31), 96));
                    break;
                }
                case 50: //Horizontal Top
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(49), 96));
                    break;
                }

                case 51: //Vertical Rise Top End Cap
                {
                    ret.AddRange(GetLineSegments(33));
                    break;
                }
                case 52: //Vertical Rise Bottom End Cap
                {
                    ret.AddRange(GetLineSegments(34));
                    break;
                }
                case 53: //Ramp Passable Up
                {
                        ret.AddRange(GetLineSegments(4));
                        break;
                    }
                case 54: //Ramp Passable Down
                    {
                        ret.AddRange(GetLineSegments(4));
                        break;
                    }
                case 55: //Vertical Full Exit Right
                {
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {78, 96, 78, 78, 96, 78}));
                    break;
                }
                case 56: //Vertical Full Exit Left
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(55), 96));
                    break;
                }
                case 57: //Box Top Left
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 17, 96, 17}));
                    break;
                }
                case 58: //Box Top Right
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(57), 96));
                    break;
                }
                case 59: // + Junction
                {
                    ret.AddRange(CreateSegments(new[] {0, 78, 17, 78, 17, 96}));
                    ret.AddRange(CreateSegments(new[] {0, 17, 17, 17, 17, 0}));
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {78, 96, 78, 78, 96, 78}));
                    break;
                }
                case 60: //Horizontal Mid Wall Top
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 46, 17, 46, 24, 49, 24, 49, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {0, 78, 46, 78, 46, 38, 49, 38, 49, 78, 96, 78}));
                    break;
                }
                case 61: //Horizontal Mid Wall Bottom
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(60), 96));
                    break;
                }
                case 62: //Empty
                {
                        ret.AddRange(GetLineSegments(20));
                        break;
                }
                case 63: //Empty
                {
                        ret.AddRange(GetLineSegments(20));
                        break;
                }
                case 64: //Vertical Full Exit Top
                {
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {0, 17, 17, 17, 17, 0}));
                    break;
                }
                case 65: //Vertical Full Exit Bottom
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(64), 96));
                    break;
                }
                case 66: //Box Bottom Left
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(57), 96));
                    break;
                }
                case 67: //Box Bottom Right
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(58), 96));
                    break;
                }
                case 68: //x Junction
                {
                    ret.AddRange(CreateSegments(new[] {0, 17, 10, 17, 35, 42, 35, 46, 43, 46, 43, 55, 35, 55, 35, 59, 16, 78, 0, 78}));
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 85, 39, 63, 44, 63, 44, 54, 51, 54, 51, 63, 56, 63, 78, 85, 78, 96}));
                    ret.AddRange(CreateSegments(new[] {96, 78, 79, 78, 60, 59, 60, 55, 52, 55, 52, 46, 60, 46, 60, 42, 85, 17, 96, 17}));
                    ret.AddRange(CreateSegments(new[] {78, 0, 78, 16, 57, 37, 53, 37, 53, 43, 42, 43, 42, 37, 38, 37, 17, 16, 17, 0}));
                    break;
                }
                case 69: //Vertical Mid Left Wall
                {
                    ret.AddRange(CreateSegments(new[] {17, 96, 17, 50, 24, 50, 24, 47, 17, 47, 17, 0}));
                    ret.AddRange(CreateSegments(new[] {78, 96, 78, 50, 38, 50, 38, 47, 78, 47, 78, 0}));
                    break;
                }
                case 70: //Vertical Mid Right Wall
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(69), 96));
                    break;
                }
                case 71: //Empty
                {
                    break;
                }
                case 72: //Empty
                {
                    break;
                }
                case 73: //Horizontal Water Mid
                {
                    ret.AddRange(GetLineSegments(20));
                    break;
                }
                case 74: //Vertical Water Mid
                {
                    ret.AddRange(GetLineSegments(4));
                    break;
                }
                case 75: //Horizontal Water Top
                {
                    ret.AddRange(GetLineSegments(20));
                    break;
                }
                case 76: //Horizontal Water Bottom
                {
                    ret.AddRange(GetLineSegments(20));
                    break;
                }
                case 77: //Vertical Water Right
                {
                    ret.AddRange(GetLineSegments(4));
                    break;
                }
                case 78: //Vertical Water Left
                {
                    ret.AddRange(GetLineSegments(4));
                    break;
                }
                case 79: //Empty
                {
                    break;
                }
                case 80: //Empty
                {
                    break;
                }
                case 81: //Empty
                {
                    break;
                }
                case 82: //High Vertical
                {
                    ret.AddRange(GetLineSegments(4));
                    break;
                }
                case 83: //High Left
                {
                    ret.AddRange(GetLineSegments(5));
                    break;
                }
                case 84: //High
                {
                    break;
                }
                case 85: //High Right
                {
                    ret.AddRange(GetLineSegments(7));
                    break;
                }
                case 86: //Empty
                {
                    break;
                }
                case 87: //Empty
                {
                    break;
                }
                case 88: //Empty
                {
                    break;
                }
                case 89: //Empty
                {
                    break;
                }
                case 90: //Empty
                {
                    break;
                }
                case 91: //High Horizontal
                {
                    ret.AddRange(GetLineSegments(20));
                    break;
                }
                case 92: //Zig Zag Horizontal
                {
                    ret.AddRange(FlipHorizontalSegments(GetLineSegments(2), 96));
                    break;
                }
                case 93: //Zig Zag Vertical
                {
                    ret.AddRange(FlipVerticalSegments(GetLineSegments(10), 96));
                    break;
                    }
                case 94: //Empty
                {
                    break;
                }
                case 95: //Empty
                {
                    break;
                }
                case 96: //Empty
                {
                    break;
                }
                case 97: //Empty
                {
                    break;
                }
                case 98: //Empty
                {
                    break;
                }
                case 99: //Empty
                {
                    break;
                }
                case 100: //High Top
                {
                    ret.AddRange(GetLineSegments(31));
                    break;
                }
                case 101: //Empty
                {
                        ret.AddRange(GetLineSegments(12));
                        break;
                }
                case 102: //Empty
                {
                        ret.AddRange(GetLineSegments(20));
                        break;
                }
                case 103: //Empty
                {
                    break;
                }
                case 104: //Empty
                {
                    break;
                }
                case 105: //Empty
                {
                    break;
                }
                case 106: //Empty
                {
                    break;
                }
                case 107: //Empty
                {
                    break;
                }
                case 108: //Empty
                {
                    break;
                }
                case 109: //Bottom
                {
                    ret.AddRange(GetLineSegments(40));
                    break;
                }
            }
            return ret;
        }

        
        public static List<Side> GetStarts(int tile)
        {
            var ret = new List<Side>();
            switch (tile)
            {
                case 1:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    break;
                }
                case 3:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Right);
                    break;
                }
                case 12:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 19:
                {
                    ret.Add(Side.Bottom);
                    ret.Add(Side.Left);
                    break;
                }
                case 20:
                {
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    break;
                }
                case 21:
                {
                    ret.Add(Side.Bottom);
                    ret.Add(Side.Right);
                    break;
                }
                case 24:
                {
                    ret.Add(Side.Right);
                    break;
                }
                case 25:
                {
                    ret.Add(Side.Left);
                    break;
                }
                case 28:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 29:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 30:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 33:
                {
                    ret.Add(Side.Top);
                    break;
                }
                case 34:
                {
                    ret.Add(Side.Bottom);
                    break;
                }
                case 37:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 38:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 46:
                {
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 47:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    break;
                }
                case 49:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 50:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 55:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 56:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 57:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 58:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 59:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 64:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 65:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 66:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
                case 67:
                {
                    ret.Add(Side.Top);
                    ret.Add(Side.Left);
                    ret.Add(Side.Right);
                    ret.Add(Side.Bottom);
                    break;
                }
            }
            return ret;
        }

        public static List<KeyValuePair<Side, int[]>> GetSideConnections(int tile)
        {
            var ret = new List<KeyValuePair<Side, int[]>>();
            var top = new List<int>();
            var bottom = new List<int>();
            var left = new List<int>();
            var right = new List<int>();
            switch (tile)
            {
                case 1: //Top Left Round Corner
                {
                    right.AddRange(ConnectionsRightNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 2: //Horizontal 2 half walls
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 3: //Top Right Round Corner
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 4: //Center Vertical Rise
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(new[] {82, 13});
                    break;
                }
                case 5: //Left Vertical Rise Low Top
                {
                    top.AddRange(new[] {14, 8, 28, 56, 57});
                    right.AddRange(new[] {6, 7});
                    bottom.AddRange(new[] {14, 83});
                    break;
                }
                case 6: // Vertical Rise Low Top
                {
                    top.AddRange(new[] {15, 29, 50, 64});
                    right.AddRange(new[] {6, 7});
                    left.AddRange(new[] {6, 5});
                    bottom.AddRange(new[] {15, 84});
                    break;
                }
                case 7: // Right Vertical Rise Low Top
                {
                    top.AddRange(new[] {16, 9, 30, 55, 58});
                    left.AddRange(new[] {6, 5});
                    bottom.AddRange(new[] {16, 85, 109});
                    break;
                }
                case 8: //Round Top Left
                {
                    right.AddRange(new[] {9, 31, 50, 58, 64});
                    bottom.AddRange(new[] {17, 5, 28, 56, 66});
                    break;
                }
                case 9: //Round Top Right
                {
                    left.AddRange(new[] {8, 32, 50, 57, 64});
                    bottom.AddRange(new[] {18, 7, 30, 55, 67});
                    break;
                }
                case 10: //Verticle 2 half walls
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 11: //Background
                {
                    break;
                }
                case 12: //Vertical
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 13: //Center Vertical Rise Low Bottom
                {
                    top.AddRange(new[] {82, 4});
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 14: //Left Vertical Rise Low Bottom
                {
                    top.AddRange(new[] {5, 83});
                    right.AddRange(new[] {15, 16});
                    bottom.AddRange(new[] {5, 17, 28, 56, 66});
                    break;
                }
                case 15: //Vertical Rise Low Bottom
                {
                    top.AddRange(new[] {6, 84, 100});
                    left.AddRange(new[] {14, 15});
                    right.AddRange(new[] {15, 16});
                    bottom.AddRange(new[] {6, 29, 49, 65});

                    break;
                }
                case 16: //Right Vertical Rise Low Bottom
                {
                    top.AddRange(new[] {7, 85});
                    left.AddRange(new[] {15, 14});
                    bottom.AddRange(new[] {7, 18, 30, 55, 67});
                    break;
                }
                case 17: //Round Bottom Left
                {
                    top.AddRange(new[] {8, 14, 28, 56, 57});
                    right.AddRange(new[] {18, 40, 49, 67, 65});
                    break;
                }
                case 18: //Round Bottom Right
                {
                    top.AddRange(new[] {9, 16, 30, 55, 58});
                    left.AddRange(new[] {17, 41, 49, 66, 65});
                    break;
                }
                case 19: //Bottom Left Round Corner
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 20: //Horizontal
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 21: //Bottom Right Round Corner
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    left.AddRange(ConnectionsLeftNormalLow);
                    break;
                }
                case 22: //Horizontal Rise Left to Right
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(new[] {23, 91});
                    break;
                }
                case 23: //Horizontal Rise Right to Left
                {
                    right.AddRange(ConnectionsRightNormalLow);
                    left.AddRange(new[] {22, 91});
                    break;
                }
                case 24: //Horizontal End Right Cap
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    break;
                }
                case 25: //Horizontal End Left Cap
                {
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 26: //Bouncers Horizontal
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 27: //Bouncers Vertical
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 28: //Vertical Left
                {
                    top.AddRange(new[] {8, 14, 28, 56, 57});
                    right.AddRange(new[] {29, 30, 44, 55});
                    bottom.AddRange(new[] {17, 5, 28, 56, 66});
                    break;
                }
                case 29:
                {
                    top.AddRange(new[] {29, 15, 50, 64});
                    left.AddRange(new[] {29, 28, 35, 56});
                    right.AddRange(new[] {29, 30, 44, 55});
                    bottom.AddRange(new[] {29, 6, 49, 65});
                    break;
                }
                case 30: //Vertical Right
                {
                    top.AddRange(new[] {9, 16, 30, 35, 57});
                    left.AddRange(new[] {29, 28, 35, 56});
                    bottom.AddRange(new[] {18, 7, 30, 55, 67});
                    break;
                }
                case 31: //Horizontal Top Rise Left to Right
                {
                    left.AddRange(new[] {8, 32, 50, 64, 57});
                    right.AddRange(new[] {32, 100});
                    bottom.AddRange(new[] {40, 44});
                    break;
                }
                case 32: //Horizontal Top Rise Right to Left
                {
                    left.AddRange(new[] {31, 100});
                    right.AddRange(new[] {9, 31, 50, 64, 58});
                    bottom.AddRange(new[] {35, 41});
                    break;
                }
                case 33: //Vertical Top End Cap
                {
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 34: //Vertical Botton End Cap
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    break;
                }
                case 35: //Horizontal Rise Right to Left
                {
                    top.AddRange(new[] {35, 32});
                    left.AddRange(new[] {44, 84, 83});
                    right.AddRange(new[] {29, 30, 44, 55});
                    bottom.AddRange(new[] {35, 41});

                    break;
                }
                case 36: //Empty
                {
                    break;
                }
                case 37: //Vertical T Section Exit Right
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 38: //Vertical T Section Exit Left
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    left.AddRange(ConnectionsLeftNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 39: //Horizontal mid walls
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 40: //Horizontal Bottom Rise Left to Right
                {
                    top.AddRange(new[] {31, 44});
                    left.AddRange(new[] {41, 17, 49, 65, 66});
                    right.AddRange(new[] {41, 109});
                    break;
                }
                case 41: //Horizontal Bottom Rise Right to Left
                {
                    top.AddRange(new[] {32, 35});
                    left.AddRange(new[] {40, 109});
                    right.AddRange(new[] {18, 40, 49, 65, 67});
                    break;
                }
                case 42: //Horizontal Rise Right End Cap
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    break;
                }
                case 43: //Horizontal Rise Left End Cap
                {
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 44: //Horizontal Rise Left to Right
                {
                    top.AddRange(new[] {44, 31});
                    left.AddRange(new[] {29, 28, 35, 59});
                    right.AddRange(new[] {35, 84, 85});
                    bottom.AddRange(new[] {44, 40});
                    break;
                }
                case 45: //Empty
                {
                    break;
                }
                case 46: //Horizontal T Section exit Top
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 47: //Horizontal T Section Exit Bottom
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 48: //Vertical Mid Wall
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 49: //Horizontal Bottom
                {
                    top.AddRange(new[] {29, 50, 64, 15});
                    left.AddRange(new[] {17, 41, 65, 66});
                    right.AddRange(new[] {41, 109});
                    break;
                }
                case 50: //Horizontal Top
                {
                    left.AddRange(new[] {40, 109});
                    right.AddRange(new[] {9, 40, 64, 58});
                    bottom.AddRange(new[] {29, 49, 65, 6});
                    break;
                }

                case 51: //Vertical Rise Top End Cap
                {
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 52: //Vertical Rise Bottom End Cap
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    break;
                }
                case 53: //Empty
                {
                        top.AddRange(ConnectionsTopNormalLow);
                        bottom.AddRange(ConnectionsBottomNormalLow);
                        break;
                }
                case 54: //Empty
                {
                        top.AddRange(ConnectionsTopNormalLow);
                        bottom.AddRange(ConnectionsBottomNormalLow);
                        break;
                }
                case 55: //Vertical Full Exit Right
                {
                    top.AddRange(new[] {9, 16, 30, 55, 58});
                    left.AddRange(new[] {28, 29, 35, 56});
                    right.AddRange(ConnectionsRightNormalLow);
                    bottom.AddRange(new[] {7, 18, 30, 55, 67});
                    break;
                }
                case 56: //Vertical Full Exit Left
                {
                    top.AddRange(new[] {8, 14, 28, 56, 57});
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(new[] {29, 30, 44, 55});
                    bottom.AddRange(new[] {5, 17, 28, 56, 66});
                    break;
                }
                case 57: //Box Top Left
                {
                    right.AddRange(new[] {9, 31, 50, 58, 64});
                    bottom.AddRange(new[] {17, 5, 28, 56, 66});
                    break;
                }
                case 58: //Box Top Right
                {
                    left.AddRange(new[] {8, 32, 50, 57, 64});
                    bottom.AddRange(new[] {18, 7, 30, 55, 67});
                    break;
                }
                case 59: // + Junction
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 60: //Horizontal Mid Wall Top
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 61: //Horizontal Mid Wall Bottom
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 62: 
                {
                        left.AddRange(ConnectionsLeftNormalLow);
                        right.AddRange(ConnectionsRightNormalLow);
                        break;
                }
                case 63: 
                {
                        left.AddRange(ConnectionsLeftNormalLow);
                        right.AddRange(ConnectionsRightNormalLow);
                        break;
                }
                case 64: //Vertical Full Exit Top
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    left.AddRange(new[] {8, 32, 50, 57, 64});
                    right.AddRange(new[] {9, 31, 50, 58, 64});
                    break;
                }
                case 65: //Vertical Full Exit Bottom
                {
                    left.AddRange(new[] {17, 41, 49, 66, 65});
                    right.AddRange(new[] {18, 40, 49, 67, 65});
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 66: //Box Bottom Left
                {
                    top.AddRange(new[] {8, 14, 28, 56, 57});
                    right.AddRange(new[] {18, 40, 49, 67, 65});
                    break;
                }
                case 67: //Box Bottom Right
                {
                    top.AddRange(new[] {9, 16, 30, 55, 58});
                    left.AddRange(new[] {17, 41, 49, 66, 65});
                    break;
                }
                case 68: //x Junction
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 69: //Vertical Mid Left Wall
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 70: //Vertical Mid Right Wall
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 71: //Empty
                {
                    break;
                }
                case 72: //Empty
                {
                    break;
                }
                case 73: //Horizontal Water Mid
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 74: //Vertical Water Mid
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 75: //Horizontal Water Top
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 76: //Horizontal Water Bottom
                {
                    left.AddRange(ConnectionsLeftNormalLow);
                    right.AddRange(ConnectionsRightNormalLow);
                    break;
                }
                case 77: //Vertical Water Right
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 78: //Vertical Water Left
                {
                    top.AddRange(ConnectionsTopNormalLow);
                    bottom.AddRange(ConnectionsBottomNormalLow);
                    break;
                }
                case 79: //Empty
                {
                    break;
                }
                case 80: //Empty
                {
                    break;
                }
                case 81: //Empty
                {
                    break;
                }
                case 82: //High Vertical
                {
                    top.AddRange(new[] {82, 4});
                    bottom.AddRange(new[] {82, 13});
                    break;
                }
                case 83: //High Left
                {
                    top.AddRange(new[] {83, 5});
                    right.AddRange(new[] {84, 85});
                    bottom.AddRange(new[] {83, 14});
                    break;
                }
                case 84: //High
                {
                    top.AddRange(new[] {84, 100, 6});
                    left.AddRange(new[] {83, 84, 44});
                    right.AddRange(new[] {84, 85, 35});
                    bottom.AddRange(new[] {84, 109, 15});
                    break;
                }
                case 85: //High Right
                {
                    top.AddRange(new[] {85, 7});
                    left.AddRange(new[] {83, 84});
                    bottom.AddRange(new[] {85, 16});
                    break;
                }
                case 86: //Empty
                {
                    break;
                }
                case 87: //Empty
                {
                    break;
                }
                case 88: //Empty
                {
                    break;
                }
                case 89: //Empty
                {
                    break;
                }
                case 90: //Empty
                {
                    break;
                }
                case 91: //High Horizontal
                {
                    left.AddRange(new[] {22, 91});
                    right.AddRange(new[] {91, 23});
                    break;
                }
                case 92: //Horizontal Water Mid
                    {
                        left.AddRange(ConnectionsLeftNormalLow);
                        right.AddRange(ConnectionsRightNormalLow);
                        break;
                    }
                case 93: //Vertical Water Mid
                    {
                        top.AddRange(ConnectionsTopNormalLow);
                        bottom.AddRange(ConnectionsBottomNormalLow);
                        break;
                    }
                case 94: //Empty
                {
                    break;
                }
                case 95: //Empty
                {
                    break;
                }
                case 96: //Empty
                {
                    break;
                }
                case 97: //Empty
                {
                    break;
                }
                case 98: //Empty
                {
                    break;
                }
                case 99: //Empty
                {
                    break;
                }
                case 100: //High Top
                {
                    left.AddRange(new[] {31, 100});
                    right.AddRange(new[] {100, 32});
                    bottom.AddRange(new[] {84, 109});
                    break;
                }
                case 101: //Empty
                {
                        top.AddRange(ConnectionsTopNormalLow);
                        bottom.AddRange(ConnectionsBottomNormalLow);
                        break;
                }
                case 102: //Empty
                {
                        left.AddRange(ConnectionsLeftNormalLow);
                        right.AddRange(ConnectionsRightNormalLow);
                        break;
                }
                case 103: //Empty
                {
                    break;
                }
                case 104: //Empty
                {
                    break;
                }
                case 105: //Empty
                {
                    break;
                }
                case 106: //Empty
                {
                    break;
                }
                case 107: //Empty
                {
                    break;
                }
                case 108: //Empty
                {
                    break;
                }
                case 109: //Bottom
                {
                    top.AddRange(new[] {84, 100});
                    left.AddRange(new[] {40, 109});
                    right.AddRange(new[] {109, 41});
                    break;
                }
            }
            ret.Add(new KeyValuePair<Side, int[]>(Side.Top, top.ToArray()));
            ret.Add(new KeyValuePair<Side, int[]>(Side.Bottom, bottom.ToArray()));
            ret.Add(new KeyValuePair<Side, int[]>(Side.Left, left.ToArray()));
            ret.Add(new KeyValuePair<Side, int[]>(Side.Right, right.ToArray()));
            return ret;
        }

        public static List<Rectangle> GetTeleporters(int tile)
        {
            var ret = new List<Rectangle>();
            switch (tile)
            {
                case 68: //x Junction
                {
                    ret.Add(new Rectangle(42, 37, 12, 6));
                    ret.Add(new Rectangle(52, 46, 6, 10));
                    ret.Add(new Rectangle(44, 53, 8, 6));
                    ret.Add(new Rectangle(39, 46, 6, 10));
                    break;
                }
            }
            return ret;
        }

        public static Vector2 GetTeleporterVelocity(int tile, Point r)
        {
            var ret = Vector2.Zero;
            switch (tile)
            {
                case 68: //x Junction
                    {
                        if (new Rectangle(42, 37, 12, 6).Contains(r)) ret = new Vector2(0, -1.75f);
                        else if (new Rectangle(52, 46, 6, 10).Contains(r)) ret = new Vector2(1.75f, 0);
                        else if (new Rectangle(39, 46, 6, 10).Contains(r)) ret = new Vector2(-1.75f, 0);
                        else if (new Rectangle(44, 53, 8, 6).Contains(r)) ret = new Vector2(0, 1.75f);
                        // ret.Add(new Rectangle(40, 46, 3, 10));
                        break;
                    }
            }
            return ret;
        }

        public static Vector2 GetTeleporterVelocity(int tile, Rectangle r)
        {
            var ret = Vector2.Zero;
            switch (tile)
            {
                case 68: //x Junction
                    {
                        if(r.Intersects(new Rectangle(42, 37, 12, 6))) ret = new Vector2(0,-2);
                        else if (r.Intersects(new Rectangle(52, 46, 6, 10))) ret = new Vector2(2, 0);
                        else if (r.Intersects(new Rectangle(39, 46, 6, 10))) ret = new Vector2(-2, 0);
                        else if (r.Intersects(new Rectangle(44, 53, 8, 6))) ret = new Vector2(0, 2);
                        // ret.Add(new Rectangle(40, 46, 3, 10));
                        break;
                    }
            }
            return ret;
        }

        //todo have to add randomness and direction
        public static List<KeyValuePair<Rectangle, List<Rectangle>>> GetTeleports(int i)
        {
            var ret = new List<KeyValuePair<Rectangle, List<Rectangle>>>();
            var t = GetTeleporters(i);
            if (t.Count == 1)//only 1 so has to come out same way
            {
                ret.Add(new KeyValuePair<Rectangle, List<Rectangle>>(t[0],new List<Rectangle> {t[0]}));
            }
            foreach (var c in t.ToArray())
            {
                ret.Add(new KeyValuePair<Rectangle, List<Rectangle>>(c, t.Where(p => p != c).ToList()));
            }
            return ret;
        }

        public static List<Rectangle> GetWater(int tile)
        {
            var ret = new List<Rectangle>();
            switch (tile)
            {
                case 73: //Horizontal Water Mid
                {
                    ret.Add(new Rectangle(33, 18, 30, 20));
                    ret.Add(new Rectangle(33, 58, 30, 20));
                    break;
                }
                case 74: //Horizontal Water Mid
                {
                    ret.Add(new Rectangle(18, 33, 20, 30));
                    ret.Add(new Rectangle(58, 33, 20, 30));
                    break;
                }
                case 75: //Horizontal Water Top
                {
                    ret.Add(new Rectangle(12, 18, 71, 28));
                    break;
                }
                case 76: //Horizontal Water Bottom
                {
                    ret.Add(new Rectangle(12, 50, 71, 28));
                    break;
                }
                case 77: //Vertical Water Right
                {
                    ret.Add(new Rectangle(50, 12, 28, 71));
                    break;
                }
                case 78: //Vertical Water Left
                {
                    ret.Add(new Rectangle(18, 12, 28, 71));
                    break;
                }
            }
            return ret;
        }

      /*  public static List<Rectangle> GetTeleports(int tile)
        {
            var ret = new List<Rectangle>();
            switch (tile)
            {
                case 63:
                {
                    ret.Add(new Rectangle(44, 55, 8, 3));
                    ret.Add(new Rectangle(40, 46, 3, 10));
                    ret.Add(new Rectangle(42, 38, 12, 4));
                    ret.Add(new Rectangle(53, 46, 3, 10));
                    break;
                }
            }
            return ret;
        }*/

        public static List<Vector3> GetBouncers(int tile)
        {
            var ret = new List<Vector3>();
            switch (tile)
            {
                case 26: //Horizontal
                {
                    ret.Add(new Vector3(16, 31, 4));
                    ret.Add(new Vector3(43, 31, 4));
                    ret.Add(new Vector3(73, 31, 4));
                    ret.Add(new Vector3(28, 48, 4));
                    ret.Add(new Vector3(61, 47, 4));
                    ret.Add(new Vector3(16, 64, 4));
                    ret.Add(new Vector3(43, 64, 4));
                    ret.Add(new Vector3(73, 64, 4));
                    break;
                }
                case 27: //Vertical
                {
                    ret.Add(new Vector3(31, 16, 4));
                    ret.Add(new Vector3(64, 16, 4));
                    ret.Add(new Vector3(47, 28, 4));
                    ret.Add(new Vector3(31, 43, 4));
                    ret.Add(new Vector3(64, 43, 4));
                    ret.Add(new Vector3(48, 61, 4));
                    ret.Add(new Vector3(31, 73, 4));
                    ret.Add(new Vector3(64, 73, 4));
                    break;
                }
                default:
                {
                    break;
                }
            }
            return ret;
        }

        public static Elevation GetElevation(int tile)
        {
            switch (tile)
            {
                case 4:
                    return Elevation.Vertical;
                case 5:
                    return Elevation.Vertical;
                case 6:
                    return Elevation.Vertical;
                case 7:
                    return Elevation.Vertical;
                case 13:
                    return Elevation.Vertical;
                case 14:
                    return Elevation.Vertical;
                case 15:
                    return Elevation.Vertical;
                case 16:
                    return Elevation.Vertical;
                case 22:
                    return Elevation.Horizontal;
                case 23:
                    return Elevation.Horizontal;
                case 31:
                    return Elevation.Horizontal;
                case 32:
                    return Elevation.Horizontal;
                case 35:
                    return Elevation.Horizontal;
                case 40:
                    return Elevation.Horizontal;
                case 41:
                    return Elevation.Horizontal;
                case 42:
                    return Elevation.Horizontal;
                case 43:
                    return Elevation.Horizontal;
                case 44:
                    return Elevation.Horizontal;
                case 51:
                    return Elevation.Vertical;
                case 52:
                    return Elevation.Vertical;
                case 62:
                    return Elevation.Horizontal;
                case 63:
                    return Elevation.Horizontal;
                default:
                    return Elevation.Flat;
            }
        }

        public static bool IsRamp(int tile)
        {
            return GetForce(tile, Vector2.Zero) != Vector2.Zero && tile != 42 && tile != 43 && tile != 51 && tile != 51;
        }

        public static float HillForce = 0.008f;
        public static Vector2 GetForce(int tile, Vector2 pos)
        {
            switch (tile)
            {
                case 4:
                case 5:                    
                case 6:                    
                case 7:
                    return new Vector2(0,-HillForce);
                case 13:
                case 14:
                case 15:
                case 16:
                    return new Vector2(0, HillForce);
                case 22:
                    return new Vector2(-HillForce,0);
                case 23:
                    return new Vector2(HillForce, 0);
                case 31:
                    return new Vector2(-HillForce, 0);
                case 32:
                    return new Vector2(HillForce, 0);
                case 35:
                    return new Vector2(HillForce, 0);
                case 40:
                    return new Vector2(-HillForce, 0);
                case 41:
                    return new Vector2(HillForce, 0);
                case 42:
                    return new Vector2(-HillForce, 0);
                case 43:
                    return new Vector2(HillForce, 0);
                case 44:
                    return new Vector2(-HillForce, 0);
                case 51:
                    return new Vector2(0,HillForce);
                case 52:
                    return new Vector2(0, -HillForce);
                case 53:
                {
                    return pos.Y > 23 ? new Vector2(0, HillForce) : Vector2.Zero;
                }
                case 54:
                    {
                        return pos.Y < 72 ? new Vector2(0, -HillForce) : Vector2.Zero;
                    }
                case 62:
                    {
                        return pos.X < 72 ? new Vector2(-HillForce,0) : Vector2.Zero;
                    }
                case 63:
                    {
                        return pos.X > 23 ? new Vector2(HillForce,0) : Vector2.Zero;
                    }
                case 101: //Empty
                    {
                        if(pos.Y > 25 && pos.Y < 48) return new Vector2(0,-HillForce*2.5f);
                        if (pos.Y > 48 && pos.Y < 71) return new Vector2(0,HillForce * 2.5f);
                        return Vector2.Zero;
                    }
                case 102: //Empty
                    {
                        if (pos.X > 25 && pos.X < 48) return new Vector2(-HillForce * 2.5f,0);
                        if (pos.X > 48 && pos.X < 71) return new Vector2(HillForce * 2.5f,0);
                        return Vector2.Zero;
                    }
                default:
                    return Vector2.Zero;
            }
        }

        public static bool IsTopLevel(int tile) => TopTiles.Contains(tile);

        public static List<KeyValuePair<Vector2, Vector2>> CreateSegments(int[] points)
        {
            var ret = new List<KeyValuePair<Vector2, Vector2>>();
            for (var i = 0; i < points.Length - 2; i += 2)
            {
                ret.Add(new KeyValuePair<Vector2, Vector2>(new Vector2(points[i], points[i + 1]), new Vector2(points[i + 2], points[i + 3])));
            }
            return ret;
        }

        public static List<KeyValuePair<Vector2, Vector2>> FlipHorizontalSegments(List<KeyValuePair<Vector2, Vector2>> segments, int width)
        {
            var ret = new List<KeyValuePair<Vector2, Vector2>>();
            foreach (var s in segments)
            {
                ret.Add(new KeyValuePair<Vector2, Vector2>(new Vector2(width - s.Key.X, s.Key.Y), new Vector2(width - s.Value.X, s.Value.Y)));
            }
            return ret;
        }

        public static List<KeyValuePair<Vector2, Vector2>> FlipVerticalSegments(List<KeyValuePair<Vector2, Vector2>> segments, int height)
        {
            var ret = new List<KeyValuePair<Vector2, Vector2>>();
            foreach (var s in segments)
            {
                ret.Add(new KeyValuePair<Vector2, Vector2>(new Vector2(s.Key.X, height - s.Key.Y), new Vector2(s.Value.X, height - s.Value.Y)));
            }
            return ret;
        }

        public static List<Rail> AddWallRails(List<KeyValuePair<Vector2, Vector2>> rails, int layer)
        {
            var ret = new List<Rail>();
            foreach (var r in rails)
            {
                ret.Add(new Rail(r.Key, r.Value, layer));
            }
            return ret;
        }

        public static List<Node> AddBouncerNodes(List<Vector3> bouncers, int layer)
        {
            var ret = new List<Node>();
            foreach (var b in bouncers)
            {
                ret.Add(new Node(new Vector2(b.X, b.Y), b.Z, float.MaxValue, Vector2.Zero, Vector2.Zero, layer) {Fixed = true});
            }
            return ret;
        }



    }
}
