using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaseVoxel.Terrain
{
    public class Chunk
    {
        public int xOffset;
        public int zOffset;
        public BlockType[,,] blocks = new BlockType[32,480,32];
        public Lump[] lumps = new Lump[15];

        public bool lumpAreaContainsBlock(int yHeight)
        {
            for(int x = 0; x < blocks.GetLength(0); x++)
            {
                for(int y = yHeight; y < yHeight + 32; y++)
                {
                    for(int z = 0; z < blocks.GetLength(2); z++)
                    {
                        if (blocks[x, y, z] != BlockType.Air) return true;
                    }
                }
            }
            return false;
        }
        public BlockType[,,] getLumpArea(int yHeight)
        {
            BlockType[,,] lumpArea = new BlockType[32, 32, 32];

            for(int x = 0; x < blocks.GetLength(0); x++)
            {
                for(int y = yHeight; y < yHeight + 32; y++)
                {
                    for(int z = 0; z < blocks.GetLength(2); z++)
                    {
                        lumpArea[x, y-yHeight, z] = blocks[x, y, z];
                    }
                }
            }

            return lumpArea;
        }

        public void generateMesh()
        {
            for(int i = 0; i < lumps.Length; i++)
            {
                if (lumpAreaContainsBlock(i * 32))
                {
                    MeshInstance m = MeshUtils.AddMeshInstanceFromBlockMap(getLumpArea(i * 32), xOffset, i*32, zOffset);

                    if (m == null) lumps[i] = new Lump();
                    else lumps[i] = new Lump(m);
                }
                else
                {
                    lumps[i] = new Lump();
                }
            }
        }

        public void regenerateMesh()
        {
            for(int i = 0; i < lumps.Length; i++)
            {
                if(lumps[i].empty == false)
                {
                    lumps[i].getMeshInstance().QueueFree();
                    MeshInstance m = MeshUtils.AddMeshInstanceFromBlockMap(getLumpArea(i * 32), xOffset, i * 32, zOffset);

                    if (m == null) lumps[i] = new Lump();
                    else lumps[i] = new Lump(m);
                }
            }
        }

        public void regenerateLump(int lumpIndex)
        {
            if(lumps[lumpIndex].empty == false) lumps[lumpIndex].getMeshInstance().QueueFree();
            MeshInstance m = MeshUtils.AddMeshInstanceFromBlockMap(getLumpArea(lumpIndex * 32), xOffset, lumpIndex * 32, zOffset);

            if (m == null) lumps[lumpIndex] = new Lump();
            else lumps[lumpIndex] = new Lump(m);
        }

        public BlockType getBlockAt(int worldX, int worldY, int worldZ)
        {
            return blocks[worldX - xOffset, worldY, worldZ - zOffset];
        }

        public void setBlockAt(int worldX, int worldY, int worldZ, BlockType block)
        {
            blocks[worldX - xOffset, worldY, worldZ - zOffset] = block;
        }

        public Chunk(int xOffset, int zOffset)
        {
            this.xOffset = xOffset;
            this.zOffset = zOffset;
        }
    }
}
