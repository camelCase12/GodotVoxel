using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaseVoxel.Terrain
{
    public class BlockMaterials
    {
        public static SpatialMaterial dirtMaterial = new SpatialMaterial()
        {
            AlbedoColor = Color.Color8(120, 78, 55),
            AlbedoTexture = ((Texture)ResourceLoader.Load("res://textures/TestBlock.png"))
        };
        public static SpatialMaterial stoneMaterial = new SpatialMaterial()
        {
            AlbedoColor = Color.Color8(150, 150, 150)
        };
        public static ShaderMaterial grassPlusDirtMaterial = (ShaderMaterial)ResourceLoader.Load("res://textures/GrassPlusDirt.tres");
    }
}
