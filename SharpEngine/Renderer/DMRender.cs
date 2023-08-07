using System;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Renderer;

/// <summary>
/// Static class which used to render textures, texts or rectangles
/// </summary>
public static class DMRender
{
    /// <summary>
    /// Current Instructions to be rendered
    /// </summary>
    public static List<Instruction> Instructions = new();

    private static void DrawInstructions(List<Instruction> instructions)
    {
        foreach (var instruction in instructions)
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
                        (float)instruction.Parameters[4], (int)instruction.Parameters[5],
                        (int)instruction.Parameters[6], (Color)instruction.Parameters[7]);
                    break;
                case InstructionType.DrawRectangleLinesEx:
                    Raylib.DrawRectangleLinesEx((Rect)instruction.Parameters[0], (int)instruction.Parameters[1],
                        (Color)instruction.Parameters[2]);
                    break;
                case InstructionType.DrawTextEx:
                    Raylib.DrawTextEx((Font)instruction.Parameters[0], (string)instruction.Parameters[1],
                        (Vec2)instruction.Parameters[2], (int)instruction.Parameters[3],
                        (int)instruction.Parameters[4], (Color)instruction.Parameters[5]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Draw all instructions in Window
    /// </summary>
    /// <param name="window">Window</param>
    public static void Draw(Window window)
    {
        var entityInstructions = Instructions.Where(x => x.Source == InstructionSource.Entity).ToList();
        var uiInstructions = Instructions.Where(x => x.Source == InstructionSource.UI).ToList();
        
        entityInstructions.Sort((i1, i2) => i1.ZLayer.CompareTo(i2.ZLayer));
        uiInstructions.Sort((i1, i2) => i1.ZLayer.CompareTo(i2.ZLayer));
        
        Raylib.BeginMode2D(window.CameraManager.Camera2D);
        DrawInstructions(entityInstructions);
        Raylib.EndMode2D();
        
        DrawInstructions(uiInstructions);
        
        Instructions.Clear();
    }

    /// <summary>
    /// Add Scissor Mode Instructions
    /// </summary>
    /// <param name="posX">Position X</param>
    /// <param name="posY">Position Y</param>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    /// <param name="scissorAction">Function which render in scissor mode</param>
    public static void ScissorMode(int posX, int posY, int width, int height, InstructionSource source, int zLayer, Action scissorAction)
    {
        var instructions = new List<Instruction>(Instructions);
        Instructions.Clear();
        scissorAction();
        var instruction = new Instruction
        {
            Type = InstructionType.ScissorMode,
            Source = source,
            ZLayer = zLayer,
            Parameters = new List<object> { posX, posY, width, height }
        };
        instruction.Parameters.AddRange(Instructions.Select(x => (object)x));
        Instructions = instructions;
        Instructions.Add(instruction);
    }

    /// <summary>
    /// Add Draw Rectangle Pro Instruction
    /// </summary>
    /// <param name="rectangle">Rectangle</param>
    /// <param name="origin">Origin</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawRectangle(Rect rectangle, Vec2 origin, float rotation, Color color, InstructionSource source, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawRectanglePro,
            Source = source,
            ZLayer = zLayer,
            Parameters = new List<object> { rectangle, origin, rotation, color }
        });
    }

    /// <summary>
    /// Add Draw Rectangle Instruction
    /// </summary>
    /// <param name="posX">Position X</param>
    /// <param name="posY">Position Y</param>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawRectangle(int posX, int posY, int width, int height, Color color,
        InstructionSource source, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawRectangle,
            Source = source,
            ZLayer = zLayer,
            Parameters = new List<object> { posX, posY, width, height, color }
        });
    }

    /// <summary>
    /// Add Draw Rectangle Lines Ex Instruction
    /// </summary>
    /// <param name="rect">Rectangle</param>
    /// <param name="borderSize">Border Size</param>
    /// <param name="borderColor">Border Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawRectangleLines(Rect rect, int borderSize, Color borderColor,
        InstructionSource source, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawRectangleLinesEx,
            Source = source,
            ZLayer = zLayer,
            Parameters = new List<object> { rect, borderSize, borderColor }
        });
    }

    /// <summary>
    /// Add Draw Texture Pro Instruction
    /// </summary>
    /// <param name="texture">Texture</param>
    /// <param name="src">Rectangle Source</param>
    /// <param name="dest">Rectangle Destination</param>
    /// <param name="origin">Origin</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="tint">Color Tint</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawTexture(Texture2D texture, Rect src, Rect dest, Vec2 origin, float rotation, Color tint,
        InstructionSource source, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawTexturePro,
            Source = source,
            ZLayer = zLayer,
            Parameters = new List<object> { texture, src, dest, origin, rotation, tint }
        });
    }

    /// <summary>
    /// Add Draw Text Pro Instruction
    /// </summary>
    /// <param name="font">Font</param>
    /// <param name="text">Text</param>
    /// <param name="position">Position</param>
    /// <param name="origin">Origin</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="fontSize">Font Size</param>
    /// <param name="spacing">Spacing</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawText(Font font, string text, Vec2 position, Vec2 origin, float rotation, int fontSize,
        int spacing, Color color, InstructionSource source, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawTextPro,
            Source = source,
            ZLayer = zLayer,
            Parameters = new List<object> { font, text, position, origin, rotation, fontSize, spacing, color }
        });
    }

    /// <summary>
    /// Add Draw Text Ex Instruction
    /// </summary>
    /// <param name="font">Font</param>
    /// <param name="text">Text</param>
    /// <param name="position">Position</param>
    /// <param name="fontSize">Font Size</param>
    /// <param name="spacing">Spacing</param>
    /// <param name="color">Color</param>
    /// <param name="source">Instruction Source</param>
    /// <param name="zLayer">Z Layer</param>
    public static void DrawText(Font font, string text, Vec2 position, int fontSize, int spacing, Color color,
        InstructionSource source, int zLayer)
    {
        Instructions.Add(new Instruction
        {
            Type = InstructionType.DrawTextEx,
            Source = source,
            ZLayer = zLayer,
            Parameters = new List<object> { font, text, position, fontSize, spacing, color }
        });
    }
}