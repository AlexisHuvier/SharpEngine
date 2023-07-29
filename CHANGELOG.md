== Changelog

[+] Add - [~] Modification - [-] Suppression - [#] Bug Fixes - [.] Others

V 1.4.8 - 28/07/2023
[~] FontManager : Check if font file exists
[.] Tests : Improve tests coverage

V 1.4.7 - 15/07/2023
[~] Physics : 1 meter is 50 pixels
[#] Scene : Fix world step

V 1.4.6 - 14/07/2023
[~] Physics : Fixed World Step
[-] Window : Fixed Update

V 1.4.5 - 14/07/2023
[#] ImGui : Render only on Update

V 1.4.4 - 14/07/2023
[+] Window : FPS Parameter
[~] DebugManager : Update SharpEngine ImGui Window

V 1.4.3 - 14/07/2023
[#] FontManager : Loading Error
[.] Update : ImGui.NET, Raylib-cs

V 1.4.2 - 14/07/2023
[-] Widgets : Display Rect Functions
[.] Refactor : Use Properties

V 1.4.1 - 14/07/2023
[+] FontManager : Fonts
[+] MusicManager : Musics
[+] SoundManager : Sounds
[+] TextureManager : Textures
[+] Window : SoundManager, MusicManager
[~] Window : Make scenes property public
[~] DebugManager : Upgrade CreateSeImGuiWindow
[#] Physics : Remove entity doesn't remove Body

V 1.4.0 - 08/07/2023
[+] ScrollFrame
[.] Refactor : Use Linq

V 1.3.2 - 02/07/2023
[+] Color : Darker, Lighter
[+] Widget : GetDisplayRect, GetTotalDisplayedRect
[~] Logs

V 1.3.1 - 01/07/2023
[~] Frame : Use DrawRectangleLinesEx
[~] LineInput : Use Scissor
[~] MultiLineInput : Use Scissor
[-] LineInput : Remove Cursor
[-] MultiLineInput : Remove Cursor

V 1.3.0 - 28/06/2023
[+] Label Style
[+] MultiLineInput
[~] LineInput : Unlimited Characters
[#] Label : Render

V 1.2.2 - 26/06/2023
[#] LineInput : Use RealPostion in Focused

V 1.2.1 - 26/06/2023
[#] Widget : Children display

V 1.2.0 - 25/06/2023
[+] ValueEventArgs
[+] LineInput
[#] Window : Update and Draw only current scene
[#] Input : Result of PressedChers and PressedKeys

V 1.1.0 - 24/06/2023
[+] ColorRect
[+] Label
[+] Button
[+] Checkbox
[+] Frame
[+] Image
[+] TextureButton
[+] Rect : Contains
[+] InputManager : IsMouseInRectangle
[~] Widget : Position is required
[~] SharpEngine : Use C# Events
[#] InputManager : Values of MouseButton
[#] TextComponent : Color

V 1.0.0 - 22/06/2023
[.] RECREATION WITH RAYLIB-CS

V 0.19.7 (Not Released) - 31/05/2023
[+] SoundManager : Volume
[+] MusicManager : Repeating, Muted, Volume
[+] InputManager : GetDownedKeys
[+] Window : Resizable
[+] Renderer : AllowOutsizeRender
[#] TileMapComponent : LayerDepth

V 0.19.6 - 26/05/2023
[~] InputManager : Optimize
[-] MonoGameVersion

V 0.19.5 - 26/05/2023
[#] PhysicsComponent : RemoveBody

V 0.19.4 - 25/05/2023
[#] PhysicsComponent

V 0.19.3 - 24/05/2023
[#] Widget : Children drawing when parent isn't displayed

V 0.19.2 - 23/05/2023
[+] Color : Implicit Cast between SharpEngine and MonoGame
[+] Pause System
[.] Refactor

V 0.19.1 - 22/05/2023
[#] AutoMouvementComponent
[.] Refactor
[.] Optimization

V 0.19.0 - 22/05/2023
*No changelog*

V 0.18.3 - 21/05/2023
[+] Particle : ZLayer
[+] ParticleEmitter : ZLayer
[~] Rect : Optimize

V 0.18.2 - 21/05/2023
[+] Widget : RemoveAllChildren

V 0.18.1 - 20/05/2023
[+] Window : GetScene<T>
[+] Entity : GetScene<T>
[+] Widget : GetScene<T>

V 0.18.0 - 19/05/2023
[+] RectComponent

V 0.17.5 - 17/05/2023
[#] Widget : Chidren rendering

V 0.17.4 - 16/05/2023
[#] Widgets : LayerDepth
[.] Optimization

V 0.17.3 - 16/05/2023
[~] Internal Layer System
[.] Optimization

V 0.17.2 - 15/05/2023
[~] Scene : Optimize Entity Sorting

V 0.17.1 - 13/05/2023
[~] Save : Define default value

V 0.17.0 - 13/05/2023
[+] Animation
[+] Rand
[+] ControlComponent : Direction
[+] Window : ShowPhysicDebugView
[+] DebugManager : CreateSharpEngineImGuiWindow
[+] Scene : RemoveAllWidgets, RemoveAllEntities
[~] DebugManager : Optimize
[~] TileMap : Optimize
[~] Vec2 : Transform to Struct
[~] Rect : Transform to Struct
[~] FixtureInfo : Transform to Struct
[~] TileType : Transform to Struct
[#] TileMap : Spacing
[.] Update to Net 7
[.] Refactor

V 0.16.11 - 27/09/2022
[#] Widget : GetRealPosition

V 0.16.10 - 27/09/2022
[~] Widget : Don't update when not displayed

V 0.16.9 - 22/09/2022
[#] Scene : RemoveEntity, RemoveWidget

V 0.16.8 - 22/09/2022
[+] Scene : Delay Parameter in RemoveEntity and RemoveWidget

V 0.16.7 - 21/09/2022
[#] Widgets : Fix removing
[#] Entity : Fix removing

V 0.16.6 - 21/09/2022
[~] Scene : Optimize

V 0.16.5 - 16/09/2022
[~] Widgets : Propagate ZLayer to Children
[#] Scene : Fix Order of Widgets

V 0.16.4 - 16/09/2022
[+] Widgets : ZLayer
[+] Scene : GetDisplayedWidgetsSortByZ

V 0.16.3 - 15/09/2022
[#] Image : Rotation

V 0.16.2 - 15/09/2022
[+] Image : Rotation
[#] Image : Size

V 0.16.1 - 14/09/2022
[+] Frame : BackgroundColor
[~] Widget : Don't draw children if not displayed

V 0.16.0 - 11/09/2022
[+] FixtureTag
[+] Fixture : OnCollision

V 0.15.6 - 08/09/2022
*No changelog*

V 0.15.5 - 08/09/2022
[~] PhysicsComponent : Remove Body when Removed

V 0.15.4 - 07/09/2022
*No changelog*

V 0.15.3 - 07/09/2022
[#] Scene : AddEntity

V 0.15.2 - 07/09/2022
[~] Scene : Return Entity when added 

V 0.15.1 - 07/09/2022
[~] Selector : Optimize
[#] Widgets : Real Position
[#] Widget : Deleting Child in Update

V 0.15.0 - 06/09/2022
[+] ColorRect
[+] Frame
[+] Widget : GetRealPosition
[+] Image : SourceRect, FlipX, FlipY, Scale
[~] Widgets : Optimize
[~] Selector : Rework with Child system
[#] Slider
[#] Button
[#] Checkbox

V 0.14.0 - 06/09/2022
[+] PhysicsComponent : FixedRotation, IgnoreGravity

V 0.13.0 - 06/09/2022
[+] DebugView
[~] Aether.Physics2D : Use Monogame Version

V 0.12.0 - 06/09/2022
[+] AnimSpriteSheetComponent : FlipX, FlipY, Offset
[+] SpriteComponent : FlipX, FlipY

V 0.11.4 - 05/09/2022
[~] SamplerState set to PointClamp
[~] Renderer : Optimize

V 0.11.3 - 04/09/2022
[~] TileMapComponent : Optimize

V 0.11.2 - 03/09/2022
[#] Renderer

V 0.11.1 - 03/09/2022
[#] AnimSpriteSheetComponent

V 0.11.0 - 03/09/2022
[+] Scene : CloseScene, OpenScene
[~] SpriteSheetComponent : Rename to AnimSpriteSheetComponent

V 0.10.2 - 02/09/2022
[#] TileMapComponent : Scale

V 0.10.1 - 02/09/2022
[#] Lang

V 0.10.0 - 30/80/2022
[+] Window : SetCurrentScene, VSync, Debug
[+] Widgets : Child System
[+] PhysicsComponent : GetLinearVelocity, SetLinearVelocity, ApplyLinearImpulse, Collision and Separation Callbacks
[+] DistanceJoint : Frequency, DampingRatio, ToAetherPhysics
[+] Tile : SourceRect
[+] Entity : Tag
[+] Lang
[+] RevoluteJoint
[+] RopeJoint
[+] LangManager
[+] Renderer
[+] Slider
[+] Gamepad Support
[+] ImGui
[~] ControlComponent : Improve Physics
[#] Window : ExitWithEscape
[#] ControlComponent : MouseFollow ControlType
[#] TileMapComponent
[#] LineEdit : Focused
[.] Big Refactor

V 0.9.0 - 23/05/2022
[+] Math : RandomBetween
[+] ParticleComponent
[+] CircleCollisionComponent
[+] RectCollisionComponent : Circle Collision
[+] AutoMovementComponent
[+] SpriteComponent : Offset
[+] TextComponent : Offset
[+] Window : StartCallback, StopCallback, GetCurrentScene, Fullscreen Management
[+] SpriteSheetComponent : ToString 
[+] Color : Operators, Equals, GetHashCode, GetColorBetween
[+] Vec2 : ToAetherPhysics
[+] Physics Engine
[+] Docs
[~] PhysicsComponent : Allow Multiple fixtures
[~] Change components creation system
[~] Change widget creation system
[~] Make Math public
[#] TransformComponent : Fix comment
[.] Window : Refactor

V 0.8.0 - 28/10/2021
[+] Entity : TextInput Event
[+] Component : TextInput Event
[+] DebugManager
[+] Vec2 : Normalized, LengthSquared
[+] Math : E, LOG10E, LOG2E, PI, PIOVER2, PIOVER4, TAU, TWOPI, Distance, ToDegrees, ToRandians
[+] TileMapComponent
[#] PhysicsComponent
[#] RectCollisionComponent

V 0.7.1 - 16/10/2021
[+] Widget : GetScene, GetSpriteBatch, GetWindow

V 0.7.0 - 12/10/2021
[+] TexturedButton
[~] FontManager : Throw error on getting unknown font
[~] TextureManager : Throw error on getting unknown texture
[#] Rect
[#] Vec2

V 0.6.1 - 08/10/2021
[#] Scene : Update

V 0.6.0 - 07/10/2021
[+] Window : GetScene
[#] Scene : Cannot remove Entity and Widget in Update

V 0.5.2 - 05/10/2021
[#] Rect
[#] Vec2

V 0.5.1 - 05/10/2021
[#] Rect
[#] Vec2

V 0.5.0 - 05/10/2021
[+] Vec2 : Operators, Equals, GetHashCode
[+] Rect : Operators, Equals, GetHashCode
[+] ControlComponent : IsMoving

V 0.4.1 - 05/10/2021
[#] SpriteSheetComponent

V 0.4.0 - 04/10/2021
[+] SpriteSheetComponent

V 0.3.0 - 03/10/2021
[+] Save
[+] Window : TakeScreenshot, Stop
[+] TransformComponent : zLayer

V 0.2.0 - 01/10/2021
[+] Camera

V 0.1.4 - 01/10/2021
*No changelog*

V 0.1.3 - 01/10/2021
*No changelog*

V 0.1.2 - 01/10/2021
*No changelog*

V 0.1.0 - ??/09/2021
First version