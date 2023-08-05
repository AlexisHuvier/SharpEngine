using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Renderer;

public static class DMRender
{
    public static List<Instruction> Instructions = new();

    private static void DrawInstructions(ICollection instructions)
    {
        foreach (var instruction in Instructions)
        {
            switch (instruction.Type)
            {
                case InstructionType.ScissorMode:
                    Raylib.BeginScissorMode((int)instruction.Parameters[0], (int)instruction.Parameters[1],
                        (int)instruction.Parameters[2], (int)instruction.Parameters[3]);
                    DrawInstructions(instruction.Parameters.GetRange(4, instructions.Count - 4)
                        .Select(x => (Instruction)x).ToList());
                    Raylib.EndScissorMode();
                    break;
                case InstructionType.DrawRectangle:
                    Raylib.DrawRectangle((int)instruction.Parameters[0], (int)instruction.Parameters[1],
                        (int)instruction.Parameters[2], (int)instruction.Parameters[3],
                        (Color)instruction.Parameters[4]);
                    break;
                case InstructionType.DrawRectanglePro:
                    Raylib.DrawRectanglePro((Rect)instruction.Parameters[0], (Vec2)instruction.Parameters[1],
                        (float)instruction.Parameters[2], (Color)instruction.Parameters[3]);
                    break;
                case InstructionType.DrawTexturePro:
                    Raylib.DrawTexturePro((Texture2D)instruction.Parameters[0], (Rect)instruction.Parameters[1],
                        (Rect)instruction.Parameters[2], (Vec2)instruction.Parameters[3],
                        (float)instruction.Parameters[4], (Color)instruction.Parameters[5]);
                    break;
                case InstructionType.DrawTextPro:
                    Raylib.DrawTextPro((Font)instruction.Parameters[0], (string)instruction.Parameters[1],
                        (Vec2)instruction.Parameters[2], (Vec2)instruction.Parameters[3],
                        (float)instruction.Parameters[4], (float)instruction.Parameters[5],
                        (float)instruction.Parameters[6], (Color)instruction.Parameters[7]);
                    break;
                case InstructionType.DrawRectangleLinesEx:
                    Raylib.DrawRectangleLinesEx((Rect)instruction.Parameters[0], (float)instruction.Parameters[1],
                        (Color)instruction.Parameters[2]);
                    break;
                case InstructionType.DrawTextEx:
                    Raylib.DrawTextEx((Font)instruction.Parameters[0], (string)instruction.Parameters[1],
                        (Vec2)instruction.Parameters[2], (float)instruction.Parameters[3],
                        (float)instruction.Parameters[4], (Color)instruction.Parameters[5]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public static void Draw()
    {
        Instructions.Sort((i1, i2) => i1.ZLayer.CompareTo(i2.ZLayer));
        DrawInstructions(Instructions);
    }

    public static void ScissorMode(int posX, int posY, int width, int height, InstructionDestination destination, int zLayer, Action scissorAction)
    {
        var instructions = new List<Instruction>(Instructions);
        Instructions.Clear();
        scissorAction();
        var instruction = new Instruction
        {
            Type = InstructionType.ScissorMode,
            Destination = destination,
            ZLayer = zLayer,
            Parameters = new List<object> { posX, posY, width, height }
        };
        instruction.Parameters.AddRange(Instructions.Select(x => (object)x));
        Instructions = instructions;
        Instructions.Add(instruction);
    }

    public static void DrawRectangle(Rect rectangle, Vec2 origin, float rotation, Color color, InstructionDestination destination, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawRectanglePro,
            Destination = destination,
            ZLayer = zLayer,
            Parameters = new List<object> { rectangle, origin, rotation, color }
        });
    }

    public static void DrawRectangle(int posX, int posY, int width, int height, Color color,
        InstructionDestination destination, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawRectangle,
            Destination = destination,
            ZLayer = zLayer,
            Parameters = new List<object> { posX, posY, width, height, color }
        });
    }

    public static void DrawRectanglesLines(Rectangle rect, int borderSize, Color borderColor,
        InstructionDestination destination, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawRectangleLinesEx,
            Destination = destination,
            ZLayer = zLayer,
            Parameters = new List<object> { rect, borderSize, borderColor }
        });
    }

    public static void DrawTexture(Texture2D texture, Rect source, Rect dest, Vec2 origin, float rotation, Color tint,
        InstructionDestination destination, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawTexturePro,
            Destination = destination,
            ZLayer = zLayer,
            Parameters = new List<object> { texture, source, dest, origin, rotation, tint }
        });
    }

    public static void DrawText(Font font, string text, Vec2 position, Vec2 origin, float rotation, int fontSize,
        int spacing, Color color, InstructionDestination destination, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawTextPro,
            Destination = destination,
            ZLayer = zLayer,
            Parameters = new List<object> { font, text, position, origin, rotation, fontSize, spacing, color }
        });
    }

    public static void DrawText(Font font, string text, Vec2 position, int fontSize, int spacing, Color color,
        InstructionDestination destination, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawTextEx,
            Destination = destination,
            ZLayer = zLayer,
            Parameters = new List<object> { font, text, position, fontSize, spacing, color }
        });
    }
}