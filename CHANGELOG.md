# Changelog

### Légende 
[+] Add<br>
[\~] Modification<br>
[-] Suppression<br>
[#] Bug Fixes<br>
[.] Others

### V 1.6.2 - 02/08/2023
[#] ControlComponent : Velocity has problems

### V 1.6.1 - 30/07/2023
[+] SpriteComponent : FlipX, FlipY
[+] SpriteSheetComponent : FlipX, FlipY

### V 1.6.0 - 29/07/2023
[+] Selector

### V 1.5.0 - 29/07/2023
[+] Docs<br>
[+] Changelog<br>
[+] PhysicsComponent : IsOnGround<br>
[+] ControlComponent : CanJump, JumpForce, Implement Jump<br>
[#] Scene : Wrong Gravity

### V 1.4.8 - 28/07/2023
[\~] FontManager : Check if font file exists<br>
[.] Tests : Improve tests coverage

### V 1.4.7 - 15/07/2023
[\~] Physics : 1 meter is 50 pixels<br>
[#] Scene : Fix world step

### V 1.4.6 - 14/07/2023
[\~] Physics : Fixed World Step<br>
[-] Window : Fixed Update

### V 1.4.5 - 14/07/2023
[#] ImGui : Render only on Update

### V 1.4.4 - 14/07/2023
[+] Window : FPS Parameter<br>
[\~] DebugManager : Update SharpEngine ImGui Window

### V 1.4.3 - 14/07/2023
[#] FontManager : Loading Error<br>
[.] Update : ImGui.NET, Raylib-cs

### V 1.4.2 - 14/07/2023
[-] Widgets : Display Rect Functions<br>
[.] Refactor : Use Properties

### V 1.4.1 - 14/07/2023
[+] FontManager : Fonts<br>
[+] MusicManager : Musics<br>
[+] SoundManager : Sounds<br>
[+] TextureManager : Textures<br>
[+] Window : SoundManager, MusicManager<br>
[\~] Window : Make scenes property public<br>
[\~] DebugManager : Upgrade CreateSeImGuiWindow<br>
[#] Physics : Remove entity doesn't remove Body

### V 1.4.0 - 08/07/2023
[+] ScrollFrame<br>
[.] Refactor : Use Linq

### V 1.3.2 - 02/07/2023
[+] Color : Darker, Lighter<br>
[+] Widget : GetDisplayRect, GetTotalDisplayedRect<br>
[\~] Logs

### V 1.3.1 - 01/07/2023
[\~] Frame : Use DrawRectangleLinesEx<br>
[\~] LineInput : Use Scissor<br>
[\~] MultiLineInput : Use Scissor<br>
[-] LineInput : Remove Cursor<br>
[-] MultiLineInput : Remove Cursor

### V 1.3.0 - 28/06/2023
[+] Label Style<br>
[+] MultiLineInput<br>
[\~] LineInput : Unlimited Characters<br>
[#] Label : Render

### V 1.2.2 - 26/06/2023
[#] LineInput : Use RealPostion in Focused

### V 1.2.1 - 26/06/2023
[#] Widget : Children display

### V 1.2.0 - 25/06/2023
[+] ValueEventArgs<br>
[+] LineInput<br>
[#] Window : Update and Draw only current scene<br>
[#] Input : Result of PressedChers and PressedKeys

### V 1.1.0 - 24/06/2023
[+] ColorRect<br>
[+] Label<br>
[+] Button<br>
[+] Checkbox<br>
[+] Frame<br>
[+] Image<br>
[+] TextureButton<br>
[+] Rect : Contains<br>
[+] InputManager : IsMouseInRectangle<br>
[\~] Widget : Position is required<br>
[\~] SharpEngine : Use C# Events<br>
[#] InputManager : Values of MouseButton<br>
[#] TextComponent : Color

### V 1.0.0 - 22/06/2023
[.] RECREATION WITH RAYLIB-CS

### V 0.19.7 (Not Released) - 31/05/2023
[+] SoundManager : Volume<br>
[+] MusicManager : Repeating, Muted, Volume<br>
[+] InputManager : GetDownedKeys<br>
[+] Window : Resizable<br>
[+] Renderer : AllowOutsizeRender<br>
[#] TileMapComponent : LayerDepth

### V 0.19.6 - 26/05/2023
[\~] InputManager : Optimize<br>
[-] MonoGameVersion

### V 0.19.5 - 26/05/2023
[#] PhysicsComponent : RemoveBody

### V 0.19.4 - 25/05/2023
[#] PhysicsComponent

### V 0.19.3 - 24/05/2023
[#] Widget : Children drawing when parent isn't displayed

### V 0.19.2 - 23/05/2023
[+] Color : Implicit Cast between SharpEngine and MonoGame<br>
[+] Pause System<br>
[.] Refactor

### V 0.19.1 - 22/05/2023
[#] AutoMouvementComponent<br>
[.] Refactor<br>
[.] Optimization

### V 0.19.0 - 22/05/2023
*No changelog*

### V 0.18.3 - 21/05/2023
[+] Particle : ZLayer<br>
[+] ParticleEmitter : ZLayer<br>
[\~] Rect : Optimize

### V 0.18.2 - 21/05/2023
[+] Widget : RemoveAllChildren<br>

### V 0.18.1 - 20/05/2023
[+] Window : GetScene\<T><br>
[+] Entity : GetScene\<T><br>
[+] Widget : GetScene\<T>

### V 0.18.0 - 19/05/2023
[+] RectComponent

### V 0.17.5 - 17/05/2023
[#] Widget : Chidren rendering

### V 0.17.4 - 16/05/2023
[#] Widgets : LayerDepth<br>
[.] Optimization

### V 0.17.3 - 16/05/2023
[\~] Internal Layer System<br>
[.] Optimization

### V 0.17.2 - 15/05/2023
[\~] Scene : Optimize Entity Sorting

### V 0.17.1 - 13/05/2023
[\~] Save : Define default value

### V 0.17.0 - 13/05/2023
[+] Animation<br>
[+] Rand<br>
[+] ControlComponent : Direction<br>
[+] Window : ShowPhysicDebugView<br>
[+] DebugManager : CreateSharpEngineImGuiWindow<br>
[+] Scene : RemoveAllWidgets, RemoveAllEntities<br>
[\~] DebugManager : Optimize<br>
[\~] TileMap : Optimize<br>
[\~] Vec2 : Transform to Struct<br>
[\~] Rect : Transform to Struct<br>
[\~] FixtureInfo : Transform to Struct<br>
[\~] TileType : Transform to Struct<br>
[#] TileMap : Spacing<br>
[.] Update to Net 7<br>
[.] Refactor

### V 0.16.11 - 27/09/2022
[#] Widget : GetRealPosition

### V 0.16.10 - 27/09/2022
[\~] Widget : Don't update when not displayed

### V 0.16.9 - 22/09/2022
[#] Scene : RemoveEntity, RemoveWidget

### V 0.16.8 - 22/09/2022
[+] Scene : Delay Parameter in RemoveEntity and RemoveWidget

### V 0.16.7 - 21/09/2022
[#] Widgets : Fix removing<br>
[#] Entity : Fix removing

### V 0.16.6 - 21/09/2022
[\~] Scene : Optimize

### V 0.16.5 - 16/09/2022
[\~] Widgets : Propagate ZLayer to Children<br>
[#] Scene : Fix Order of Widgets

### V 0.16.4 - 16/09/2022
[+] Widgets : ZLayer<br>
[+] Scene : GetDisplayedWidgetsSortByZ

### V 0.16.3 - 15/09/2022
[#] Image : Rotation

### V 0.16.2 - 15/09/2022
[+] Image : Rotation<br>
[#] Image : Size

### V 0.16.1 - 14/09/2022
[+] Frame : BackgroundColor<br>
[\~] Widget : Don't draw children if not displayed

### V 0.16.0 - 11/09/2022
[+] FixtureTag<br>
[+] Fixture : OnCollision

### V 0.15.6 - 08/09/2022
*No changelog*

### V 0.15.5 - 08/09/2022
[\~] PhysicsComponent : Remove Body when Removed

### V 0.15.4 - 07/09/2022
*No changelog*

### V 0.15.3 - 07/09/2022
[#] Scene : AddEntity

### V 0.15.2 - 07/09/2022
[\~] Scene : Return Entity when added 

### V 0.15.1 - 07/09/2022
[\~] Selector : Optimize<br>
[#] Widgets : Real Position<br>
[#] Widget : Deleting Child in Update

### V 0.15.0 - 06/09/2022
[+] ColorRect<br>
[+] Frame<br>
[+] Widget : GetRealPosition<br>
[+] Image : SourceRect, FlipX, FlipY, Scale<br>
[\~] Widgets : Optimize<br>
[\~] Selector : Rework with Child system<br>
[#] Slider<br>
[#] Button<br>
[#] Checkbox

### V 0.14.0 - 06/09/2022
[+] PhysicsComponent : FixedRotation, IgnoreGravity

### V 0.13.0 - 06/09/2022
[+] DebugView<br>
[\~] Aether.Physics2D : Use Monogame Version

### V 0.12.0 - 06/09/2022
[+] AnimSpriteSheetComponent : FlipX, FlipY, Offset<br>
[+] SpriteComponent : FlipX, FlipY

### V 0.11.4 - 05/09/2022
[\~] SamplerState set to PointClamp<br>
[\~] Renderer : Optimize

### V 0.11.3 - 04/09/2022
[\~] TileMapComponent : Optimize

### V 0.11.2 - 03/09/2022
[#] Renderer

### V 0.11.1 - 03/09/2022
[#] AnimSpriteSheetComponent

### V 0.11.0 - 03/09/2022
[+] Scene : CloseScene, OpenScene<br>
[\~] SpriteSheetComponent : Rename to AnimSpriteSheetComponent

### V 0.10.2 - 02/09/2022
[#] TileMapComponent : Scale

### V 0.10.1 - 02/09/2022
[#] Lang

### V 0.10.0 - 30/80/2022
[+] Window : SetCurrentScene, VSync, Debug<br>
[+] Widgets : Child System<br>
[+] PhysicsComponent : GetLinearVelocity, SetLinearVelocity, ApplyLinearImpulse, Collision and Separation Callbacks<br>
[+] DistanceJoint : Frequency, DampingRatio, ToAetherPhysics<br>
[+] Tile : SourceRect<br>
[+] Entity : Tag<br>
[+] Lang<br>
[+] RevoluteJoint<br>
[+] RopeJoint<br>
[+] LangManager<br>
[+] Renderer<br>
[+] Slider<br>
[+] Gamepad Support<br>
[+] ImGui<br>
[\~] ControlComponent : Improve Physics<br>
[#] Window : ExitWithEscape<br>
[#] ControlComponent : MouseFollow ControlType<br>
[#] TileMapComponent<br>
[#] LineEdit : Focused<br>
[.] Big Refactor

### V 0.9.0 - 23/05/2022
[+] Math : RandomBetween<br>
[+] ParticleComponent<br>
[+] CircleCollisionComponent<br>
[+] RectCollisionComponent : Circle Collision<br>
[+] AutoMovementComponent<br>
[+] SpriteComponent : Offset<br>
[+] TextComponent : Offset<br>
[+] Window : StartCallback, StopCallback, GetCurrentScene, Fullscreen Management<br>
[+] SpriteSheetComponent : ToString <br>
[+] Color : Operators, Equals, GetHashCode, GetColorBetween<br>
[+] Vec2 : ToAetherPhysics<br>
[+] Physics Engine<br>
[+] Docs<br>
[\~] PhysicsComponent : Allow Multiple fixtures<br>
[\~] Change components creation system<br>
[\~] Change widget creation system<br>
[\~] Make Math public<br>
[#] TransformComponent : Fix comment<br>
[.] Window : Refactor

### V 0.8.0 - 28/10/2021
[+] Entity : TextInput Event<br>
[+] Component : TextInput Event<br>
[+] DebugManager<br>
[+] Vec2 : Normalized, LengthSquared<br>
[+] Math : E, LOG10E, LOG2E, PI, PIOVER2, PIOVER4, TAU, TWOPI, Distance, ToDegrees, ToRandians<br>
[+] TileMapComponent<br>
[#] PhysicsComponent<br>
[#] RectCollisionComponent

### V 0.7.1 - 16/10/2021
[+] Widget : GetScene, GetSpriteBatch, GetWindow

### V 0.7.0 - 12/10/2021
[+] TexturedButton<br>
[\~] FontManager : Throw error on getting unknown font<br>
[\~] TextureManager : Throw error on getting unknown texture<br>
[#] Rect<br>
[#] Vec2

### V 0.6.1 - 08/10/2021
[#] Scene : Update

### V 0.6.0 - 07/10/2021
[+] Window : GetScene<br>
[#] Scene : Cannot remove Entity and Widget in Update

### V 0.5.2 - 05/10/2021
[#] Rect<br>
[#] Vec2

### V 0.5.1 - 05/10/2021
[#] Rect<br>
[#] Vec2

### V 0.5.0 - 05/10/2021
[+] Vec2 : Operators, Equals, GetHashCode<br>
[+] Rect : Operators, Equals, GetHashCode<br>
[+] ControlComponent : IsMoving

### V 0.4.1 - 05/10/2021
[#] SpriteSheetComponent

### V 0.4.0 - 04/10/2021
[+] SpriteSheetComponent

### V 0.3.0 - 03/10/2021
[+] Save<br>
[+] Window : TakeScreenshot, Stop<br>
[+] TransformComponent : zLayer

### V 0.2.0 - 01/10/2021
[+] Camera

### V 0.1.4 - 01/10/2021
*No changelog*

### V 0.1.3 - 01/10/2021
*No changelog*

### V 0.1.2 - 01/10/2021
*No changelog*

### V 0.1.0 - ??/09/2021
First version