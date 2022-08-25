using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameTime = SharpEngine.Utils.Math.GameTime;
using Vector2 = System.Numerics.Vector2;

namespace SharpEngine.Utils.ImGui;

public class ImGuiRenderer
{
    private InternalGame _game;
    
    private GraphicsDevice _graphicsDevice;
    
    private BasicEffect _effect;
    private RasterizerState _rasterizerState;

    private byte[] _vertexData;
    private VertexBuffer _vertexBuffer;
    private int _vertexBufferSize;

    private byte[] _indexData;
    private IndexBuffer _indexBuffer;
    private int _indexBufferSize;

    private Dictionary<IntPtr, Texture2D> _loadedTextures;

    private int _textureId;
    private IntPtr? _fontTextureId;

    private int _scrollWheelValue;
    private List<int> _keys = new();

    public ImGuiRenderer(InternalGame game)
    {
        var context = ImGuiNET.ImGui.CreateContext();
        ImGuiNET.ImGui.SetCurrentContext(context);

        _game = game;
        _graphicsDevice = game.GraphicsDevice;

        _loadedTextures = new Dictionary<IntPtr, Texture2D>();

        _rasterizerState = new RasterizerState()
        {
            CullMode = CullMode.None,
            DepthBias = 0,
            FillMode = FillMode.Solid,
            MultiSampleAntiAlias = false,
            ScissorTestEnable = true,
            SlopeScaleDepthBias = 0
        };

        SetupInput();
    }

    public virtual void BeforeLayout(GameTime gameTime)
    {
        ImGuiNET.ImGui.GetIO().DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        UpdateInput();
        ImGuiNET.ImGui.NewFrame();
    }

    public virtual void AfterLayout()
    {
        ImGuiNET.ImGui.Render();
        unsafe
        {
            RenderDrawData(ImGuiNET.ImGui.GetDrawData());
        }
    }

    protected virtual void SetupInput()
    {
        var io = ImGuiNET.ImGui.GetIO();
        
        _keys.Add(io.KeyMap[(int)ImGuiKey.Tab] = (int)Keys.Tab);
        _keys.Add(io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Keys.Left);
        _keys.Add(io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Keys.Right);
        _keys.Add(io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Keys.Up);
        _keys.Add(io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Keys.Down);
        _keys.Add(io.KeyMap[(int)ImGuiKey.PageUp] = (int)Keys.PageUp);
        _keys.Add(io.KeyMap[(int)ImGuiKey.PageDown] = (int)Keys.PageDown);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Home] = (int)Keys.Home);
        _keys.Add(io.KeyMap[(int)ImGuiKey.End] = (int)Keys.End);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Delete] = (int)Keys.Delete);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Backspace] = (int)Keys.Back);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Enter] = (int)Keys.Enter);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Escape] = (int)Keys.Escape);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Space] = (int)Keys.Space);
        _keys.Add(io.KeyMap[(int)ImGuiKey.A] = (int)Keys.A);
        _keys.Add(io.KeyMap[(int)ImGuiKey.C] = (int)Keys.C);
        _keys.Add(io.KeyMap[(int)ImGuiKey.V] = (int)Keys.V);
        _keys.Add(io.KeyMap[(int)ImGuiKey.X] = (int)Keys.X);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Y] = (int)Keys.Y);
        _keys.Add(io.KeyMap[(int)ImGuiKey.Z] = (int)Keys.Z);

        _game.Window.TextInput += (s, a) =>
        {
            if (a.Character == '\t') return;
            io.AddInputCharacter(a.Character);
        };

        ImGuiNET.ImGui.GetIO().Fonts.AddFontDefault();
    }

    protected virtual Effect UpdateEffect(Texture2D tex)
    {
        _effect ??= new BasicEffect(_graphicsDevice);
        var io = ImGuiNET.ImGui.GetIO();

        _effect.World = Matrix.Identity;
        _effect.View = Matrix.Identity;
        _effect.Projection = Matrix.CreateOrthographicOffCenter(0, io.DisplaySize.X, io.DisplaySize.Y, 0, -1, 1);
        _effect.TextureEnabled = true;
        _effect.Texture = tex;
        _effect.VertexColorEnabled = true;

        return _effect;
    }

    protected virtual void UpdateInput()
    {
        var io = ImGuiNET.ImGui.GetIO();

        var mouse = Mouse.GetState();
        var keyboard = Keyboard.GetState();

        foreach (var t in _keys)
            io.KeysDown[t] = keyboard.IsKeyDown((Keys)t);

        io.KeyShift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);
        io.KeyCtrl = keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl);
        io.KeyAlt = keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt);
        io.KeySuper = keyboard.IsKeyDown(Keys.LeftWindows) || keyboard.IsKeyDown(Keys.RightWindows);

        io.DisplaySize = new Vector2(_graphicsDevice.PresentationParameters.BackBufferWidth,
            _graphicsDevice.PresentationParameters.BackBufferHeight);
        io.DisplayFramebufferScale = new Vector2(1, 1);

        io.MousePos = new Vector2(mouse.X, mouse.Y);
        io.MouseDown[0] = mouse.LeftButton == ButtonState.Pressed;
        io.MouseDown[1] = mouse.RightButton == ButtonState.Pressed;
        io.MouseDown[2] = mouse.MiddleButton == ButtonState.Pressed;

        var scrollDelta = mouse.ScrollWheelValue - _scrollWheelValue;
        io.MouseWheel = scrollDelta > 0 ? 1 : scrollDelta < 0 ? -1 : 0;
        _scrollWheelValue = mouse.ScrollWheelValue;
    }

    public virtual unsafe void RebuildFontAlias()
    {
        var io = ImGuiNET.ImGui.GetIO();
        io.Fonts.GetTexDataAsRGBA32(out byte* pixelData, out var width, out var height, out var bytesPerPixel);

        var pixels = new byte[width * height * bytesPerPixel];
        Marshal.Copy(new IntPtr(pixelData), pixels, 0, pixels.Length);

        var tex2d = new Texture2D(_graphicsDevice, width, height, false, SurfaceFormat.Color);
        tex2d.SetData(pixels);

        if (_fontTextureId.HasValue) UnbindTexture(_fontTextureId.Value);

        _fontTextureId = BindTexture(tex2d);
        
        io.Fonts.SetTexID(_fontTextureId!.Value);
        io.Fonts.ClearTexData();
    }

    protected virtual IntPtr BindTexture(Texture2D tex)
    {
        var id = new IntPtr(_textureId++);
        _loadedTextures.Add(id, tex);
        return id;
    }

    protected virtual void UnbindTexture(IntPtr texId)
    {
        _loadedTextures.Remove(texId);
    }

    private void RenderDrawData(ImDrawDataPtr drawDataPtr)
    {
        var lastViewport = _graphicsDevice.Viewport;
        var lastScissorBox = _graphicsDevice.ScissorRectangle;

        _graphicsDevice.BlendFactor = Microsoft.Xna.Framework.Color.White;
        _graphicsDevice.BlendState = BlendState.NonPremultiplied;
        _graphicsDevice.RasterizerState = _rasterizerState;
        _graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
        
        drawDataPtr.ScaleClipRects(ImGuiNET.ImGui.GetIO().DisplayFramebufferScale);

        _graphicsDevice.Viewport = new Viewport(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
            _graphicsDevice.PresentationParameters.BackBufferHeight);

        UpdateBuffers(drawDataPtr);
        RenderCommandLists(drawDataPtr);

        _graphicsDevice.Viewport = lastViewport;
        _graphicsDevice.ScissorRectangle = lastScissorBox;
    }

    private unsafe void UpdateBuffers(ImDrawDataPtr drawDataPtr)
    {
        if (drawDataPtr.TotalVtxCount == 0) return;

        if (drawDataPtr.TotalVtxCount > _vertexBufferSize)
        {
            _vertexBuffer?.Dispose();

            _vertexBufferSize = (int)(drawDataPtr.TotalVtxCount * 1.5f);
            _vertexBuffer = new VertexBuffer(_graphicsDevice, DrawVertDeclaration.Declaration, _vertexBufferSize,
                BufferUsage.None);
            _vertexData = new byte[_vertexBufferSize * DrawVertDeclaration.Size];
        }

        if (drawDataPtr.TotalIdxCount > _indexBufferSize)
        {
            _indexBuffer?.Dispose();

            _indexBufferSize = (int)(drawDataPtr.TotalIdxCount * 1.5f);
            _indexBuffer = new IndexBuffer(_graphicsDevice, IndexElementSize.SixteenBits, _indexBufferSize,
                BufferUsage.None);
            _indexData = new byte[_indexBufferSize * sizeof(ushort)];
        }

        var vtxOffset = 0;
        var idxOffset = 0;

        for (var n = 0; n < drawDataPtr.CmdListsCount; n++)
        {
            var cmdList = drawDataPtr.CmdListsRange[n];

            fixed (void* vtxDstPtr = &_vertexData[vtxOffset * DrawVertDeclaration.Size])
            fixed (void* idxDstPtr = &_indexData[idxOffset * sizeof(ushort)])
            {
                Buffer.MemoryCopy((void*)cmdList.VtxBuffer.Data, vtxDstPtr, _vertexData.Length,
                    cmdList.VtxBuffer.Size * DrawVertDeclaration.Size);
                Buffer.MemoryCopy((void*)cmdList.IdxBuffer.Data, idxDstPtr, _indexData.Length,
                    cmdList.IdxBuffer.Size * sizeof(ushort));
            }

            vtxOffset += cmdList.VtxBuffer.Size;
            idxOffset += cmdList.IdxBuffer.Size;
        }
        
        _vertexBuffer.SetData(_vertexData, 0, drawDataPtr.TotalVtxCount * DrawVertDeclaration.Size);
        _indexBuffer.SetData(_indexData, 0, drawDataPtr.TotalIdxCount * sizeof(ushort));
    }

    private unsafe void RenderCommandLists(ImDrawDataPtr drawDataPtr)
    {
        _graphicsDevice.SetVertexBuffer(_vertexBuffer);
        _graphicsDevice.Indices = _indexBuffer;

        var vtxOffset = 0;
        var idxOffset = 0;

        for (var n = 0; n < drawDataPtr.CmdListsCount; n++)
        {
            var cmdList = drawDataPtr.CmdListsRange[n];
            for (var cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++)
            {
                var drawCmd = cmdList.CmdBuffer[cmdi];

                if (drawCmd.ElemCount == 0) continue;
                if (!_loadedTextures.ContainsKey(drawCmd.TextureId))
                    throw new InvalidOperationException($"Could not find a texture : {drawCmd.TextureId}");

                _graphicsDevice.ScissorRectangle = new Rectangle(
                    (int)drawCmd.ClipRect.X, (int)drawCmd.ClipRect.Y, (int)(drawCmd.ClipRect.Z - drawCmd.ClipRect.X),
                    (int)(drawCmd.ClipRect.W - drawCmd.ClipRect.Y));

                var effect = UpdateEffect(_loadedTextures[drawCmd.TextureId]);

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

#pragma warning disable CS0618
                    _graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                        (int)drawCmd.VtxOffset + vtxOffset, 0, cmdList.VtxBuffer.Size,
                        (int)drawCmd.IdxOffset + idxOffset, (int)drawCmd.ElemCount / 3);
#pragma warning restore CS0618
                }
            }

            vtxOffset += cmdList.VtxBuffer.Size;
            idxOffset += cmdList.IdxBuffer.Size;
        }
    }
}