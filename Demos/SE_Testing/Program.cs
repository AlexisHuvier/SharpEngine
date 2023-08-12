
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SE_Testing;

public static class Program
{
    private static void Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "Test");
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.RAYWHITE);

            DrawText("éèàùè&", 250, 20, 20, Color.DARKGRAY);

            EndDrawing();
        }

        CloseWindow();
    }
}