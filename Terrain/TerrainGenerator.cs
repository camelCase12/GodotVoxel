using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaseVoxel.Terrain
{
    public class TerrainGenerator
    {
        public static OpenSimplexNoise noise = new OpenSimplexNoise()
        {
            Seed = ChunkManager.seed,
            Octaves = 4,
            Period = 20,
            Persistence = 0.8f,
        };
        public static int[,] generateHeightmap(int xOffset, int zOffset)
        {
            int[,] heightMap = new int[32, 32];

            for(int i = 0; i < heightMap.GetLength(0); i++)
            {
                for(int j = 0; j < heightMap.GetLength(1); j++)
                {
                    heightMap[i, j] = (int)(noise.GetNoise2d(xOffset + i, zOffset + j) * 5 + 50);
                }
            }

            return heightMap;
        }
        public static BlockType[,,] generateTerrain(int xOffset, int zOffset)
        {
            BlockType[,,] blockTypes = new BlockType[32, 480, 32];

            int[,] heightMap = generateHeightmap(xOffset, zOffset);

            for(int x = 0; x < heightMap.GetLength(0); x++)
            {
                for(int z = 0; z < heightMap.GetLength(1); z++)
                {
                    for(int y = 0; y < heightMap[x, z]; y++)
                    {
                        blockTypes[x, y, z] = BlockType.Dirt;
                    }
                }
            }

            return blockTypes;
        }
    }
}
