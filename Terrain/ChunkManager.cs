using System;
using System.Collections.Generic;

namespace ChaseVoxel.Terrain
{
    public class ChunkManager
    {
        public static int seed = 0;

        public static List<Chunk> chunks = new List<Chunk>();

        public static string GetMeshName(int xOffset, int yOffset, int zOffset)
        {
            return $"{xOffset},{yOffset},{zOffset}";
        }
        public static void GenerateNewChunk(int xOffset, int zOffset)
        {
            //Generate the new chunk itself
            Chunk newChunk = new Chunk(xOffset, zOffset);

            newChunk.blocks = TerrainGenerator.generateTerrain(xOffset, zOffset);

            chunks.Add(newChunk);

            newChunk.generateMesh();

            //Regenerate any nearby chunks' meshes in an efficient manner
            foreach (Chunk chunk in chunks)
            {
                if(Math.Abs(chunk.xOffset - xOffset) <= 32 || Math.Abs(chunk.zOffset - zOffset) <= 32 && !(chunk.xOffset == xOffset && chunk.zOffset == zOffset))
                {
                    chunk.regenerateMesh();
                }
            }
        }
        public static bool TryAddBlock(int worldX, int worldY, int worldZ, BlockType block)
        {
            foreach (Chunk chunk in chunks)
            {
                if (worldX >= chunk.xOffset && worldX < chunk.xOffset + 32 &&
                    worldZ >= chunk.zOffset && worldZ < chunk.zOffset + 32 &&
                    worldY >= 0 && worldY < 480)
                {
                    chunk.setBlockAt(worldX, worldY, worldZ, block);
                    chunk.regenerateLump(worldY / 32); //TODO: Make this check for all lumps bordering a block
                    return true;
                }
            }

            return false;
        }

        public static bool TryGetBlock(int worldX, int worldY, int worldZ, out BlockType block)
        {
            block = BlockType.Air;
            foreach (Chunk chunk in chunks)
            {
                if (worldX >= chunk.xOffset && worldX < chunk.xOffset + 32 &&
                    worldZ >= chunk.zOffset && worldZ < chunk.zOffset + 32 &&
                    worldY >= 0 && worldY < 480)
                {
                    block = chunk.getBlockAt(worldX, worldY, worldZ);
                    return true;
                }
            }
            return false;
        }
    }
}
