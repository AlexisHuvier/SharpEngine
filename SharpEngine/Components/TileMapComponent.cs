using System.Xml.Linq;
using System;
using System.Collections.Generic;
using SharpEngine.Utils;

namespace SharpEngine.Components
{

    /// <summary>
    /// Composant qui affiche une tilemap de Tiled (.tmx)
    /// </summary>
    public class TileMapComponent: Component
    {
        protected string orientation;
        protected string renderorder;
        protected Vec2 size;
        protected Vec2 tileSize;
        protected bool infinite;
        protected List<TileUtils.Tile> tiles;
        protected List<TileUtils.Layer> layers;
        protected List<string> textures;

        /// <summary>
        /// Initialise le Composant.
        /// </summary>
        /// <param name="tilemap">Chemin vers la tilemap</param>
        /// <exception cref="Exception"></exception>
        public TileMapComponent(string tilemap): base()
        {
            tiles = new List<TileUtils.Tile>();
            layers = new List<TileUtils.Layer>();
            textures = new List<string>();

            XElement file = XElement.Load(tilemap);

            orientation = file.Attribute("orientation").Value;
            if (orientation != "orthogonal")
                throw new Exception("SharpEngine can only use orthogonal tilemap.");
            renderorder = file.Attribute("renderorder").Value;
            if (renderorder != "right-down")
                throw new Exception("SharpEngine can only use tilemap with right-down renderorder");
            size = new Vec2(Convert.ToInt32(file.Attribute("width").Value), Convert.ToInt32(file.Attribute("height").Value));
            tileSize = new Vec2(Convert.ToInt32(file.Attribute("tilewidth").Value), Convert.ToInt32(file.Attribute("tileheight").Value));
            infinite = Convert.ToBoolean(Convert.ToInt32(file.Attribute("infinite").Value));
            if(infinite)
                throw new Exception("SharpEngine can only use non-infinite tilemap.");

            foreach(XElement tiletype in file.Element("tileset").Elements("tile"))
            {
                TileUtils.Tile tile = new TileUtils.Tile()
                {
                    id = Convert.ToInt32(tiletype.Attribute("id").Value) + 1,
                    source = System.IO.Path.GetFileNameWithoutExtension(tiletype.Element("image").Attribute("source").Value)
                };
                textures.Add(System.IO.Path.GetDirectoryName(tilemap) + System.IO.Path.DirectorySeparatorChar + tiletype.Element("image").Attribute("source").Value);
                tiles.Add(tile);
            }

            foreach(XElement element in file.Elements("layer"))
            {
                List<int> tiles = new List<int>();
                foreach (string tile in element.Element("data").Value.Split(","))
                    tiles.Add(Convert.ToInt32(tile));
                TileUtils.Layer layer = new TileUtils.Layer
                {
                    tiles = tiles
                };
                layers.Add(layer);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for(int i = textures.Count - 1; i > -1; i--) {
                GetWindow().textureManager.AddTexture(System.IO.Path.GetFileNameWithoutExtension(textures[i]), textures[i]);
                textures.RemoveAt(i);
            }
        }

        public TileUtils.Tile GetTile(int id)
        {
            foreach(TileUtils.Tile tile in tiles)
            {
                if (tile.id == id)
                    return tile;
            }
            return new TileUtils.Tile() { };
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && layers.Count > 0)
            {
                foreach(TileUtils.Layer layer in layers)
                {
                    for(int i = 0; i < layer.tiles.Count; i++)
                    {
                        if (layer.tiles[i] != 0) {
                            Vec2 position = tc.position - tileSize * size / 2 - CameraManager.position + new Vec2(tileSize.x * Convert.ToInt32(i % Convert.ToInt32(size.x)), tileSize.y * Convert.ToInt32(i / Convert.ToInt32(size.y)));
                            GetSpriteBatch().Draw(GetWindow().textureManager.GetTexture(GetTile(layer.tiles[i]).source), new Rect(position, tileSize).ToMG(), Color.WHITE.ToMG());
                        }
                    }
                }
            }
        }

        public override string ToString() => $"TileMapComponent(orientation={orientation}, renderorder={renderorder}, size={size}, tileSize={tileSize}, infinite={infinite})";
    }
}
