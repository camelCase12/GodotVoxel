using Godot;
using System.Collections.Generic;

namespace ChaseVoxel.Terrain
{
    /// <summary>
    /// Utils for working with terrain mesh
    /// </summary>
    public class MeshUtils
    {
        public static VoxelBuilder voxelBuilder;
        /// <summary>
        /// Gets the sides which are open to air in f,r,b,l,b,t order
        /// </summary>
        /// <param name="blockMap">BlockType 3D array of blocks</param>
        /// <param name="x">x value of block to get sides from</param>
        /// <param name="y">y value of block to get sides from<</param>
        /// <param name="z">z value of block to get sides from<</param>
        /// <returns>The bools for each of openness six sides</returns>
        public static bool[] GetOpenSides(BlockType[,,] blockMap, int x, int y, int z, int xOffset, int yOffset, int zOffset)
        {
            bool[] sides = new bool[6];
            BlockType block;

            if (ChunkManager.TryGetBlock(x + xOffset - 1, y + yOffset, z + zOffset, out block) && block == BlockType.Air) sides[0] = true;
            if (ChunkManager.TryGetBlock(x + xOffset, y + yOffset, z + zOffset + 1, out block) && block == BlockType.Air) sides[1] = true;
            if (ChunkManager.TryGetBlock(x + xOffset + 1, y + yOffset, z + zOffset, out block) && block == BlockType.Air) sides[2] = true;
            if (ChunkManager.TryGetBlock(x + xOffset, y + yOffset, z + zOffset - 1, out block) && block == BlockType.Air) sides[3] = true;
            if (ChunkManager.TryGetBlock(x + xOffset, y + yOffset - 1, z + zOffset, out block) && block == BlockType.Air) sides[4] = true;
            if (ChunkManager.TryGetBlock(x + xOffset, y + yOffset + 1, z + zOffset, out block) && block == BlockType.Air) sides[5] = true;

            /*if (x - 1 < 0 || blockMap[x - 1, y, z] == BlockType.Air) sides[0] = true;
            if (z + 1 >= blockMap.GetLength(2) || blockMap[x, y, z + 1] == BlockType.Air) sides[1] = true;
            if (x + 1 >= blockMap.GetLength(0) || blockMap[x + 1, y, z] == BlockType.Air) sides[2] = true;
            if (z - 1 < 0 || blockMap[x, y, z - 1] == BlockType.Air) sides[3] = true;
            if (y - 1 < 0 || blockMap[x, y - 1, z] == BlockType.Air) sides[4] = true;
            if (y + 1 >= blockMap.GetLength(1) || blockMap[x, y + 1, z] == BlockType.Air) sides[5] = true;*/
            return sides;
        }
        public static MeshInstance AddMeshInstanceFromBlockMap(BlockType[,,] blockMap, int xOffset, int yOffset, int zOffset)
        {
            ArrayMesh array_mesh = GetArrayMeshFromBlockMap(blockMap, xOffset, yOffset, zOffset);

            if (array_mesh == null) return null;

            StaticBody staticBody = new StaticBody();

            MeshInstance m = new MeshInstance { Name = "Hello", Mesh = array_mesh, Translation = new Vector3(xOffset, yOffset, zOffset) };

            m.CreateTrimeshCollision();

            staticBody.AddChild(m);
            voxelBuilder.AddChild(staticBody);

            return m;
        }
        public static ArrayMesh GetArrayMeshFromBlockMap(BlockType[,,] blockMap, int xOffset, int yOffset, int zOffset)
        {
            //Shallow method to begin: create every instance
            Vector3[] vertices = new Vector3[CubeUtils.vertexOffsets.Length];
            int[] indices = new int[CubeUtils.indexOffsets.Length];

            var array_mesh = new ArrayMesh();

            List<Vector3> reducedVertices = new List<Vector3>();
            List<Vector3> reducedNormals = new List<Vector3>();
            List<Vector2> reducedUvs = new List<Vector2>();
            List<int> reducedIndexOffsets = new List<int>();

            for (int i = 0; i < blockMap.GetLength(0); i++)
            {
                for (int j = 0; j < blockMap.GetLength(1); j++)
                {
                    for (int k = 0; k < blockMap.GetLength(2); k++)
                    {
                        //Skip mesh generation if air
                        if (blockMap[i, j, k] == BlockType.Air) continue;

                        //Get open sides for reduced mesh structure
                        bool[] openSides = GetOpenSides(blockMap, i, j, k, xOffset, yOffset, zOffset);

                        //Get reduced mesh structures for open sides
                        Vector3[] innerReducedVertices = CubeUtils.GetReducedVertices(openSides);

                        //Continue if reducedVertices is empty
                        if (innerReducedVertices.Length == 0) continue;

                        reducedNormals.AddRange(CubeUtils.GetReducedNormals(openSides));
                        reducedUvs.AddRange(CubeUtils.GetReducedUvs(openSides));
                        int[] innerReducedIndexOffsets = CubeUtils.GetReducedIndexOffsets(openSides);

                        Vector3 position = new Vector3(i, j, k);

                        //Update indices to offset
                        for(int l = 0; l < innerReducedIndexOffsets.Length; l++)
                        {
                            innerReducedIndexOffsets[l] += reducedIndexOffsets.Count;
                        }

                        //Update vertices to offset by in-mesh position
                        for (int l = 0; l < innerReducedVertices.Length; l++)
                        {
                            innerReducedVertices[l] = innerReducedVertices[l] + position;
                        }

                        reducedVertices.AddRange(innerReducedVertices);
                        reducedIndexOffsets.AddRange(innerReducedIndexOffsets);

                    }
                }
            }
            //Break out and return null if vertices has no length
            if (reducedVertices.Count == 0) return null;

            //Add surface
            var arrays = new Godot.Collections.Array();

            arrays.Resize((int)ArrayMesh.ArrayType.Max);
            arrays[(int)ArrayMesh.ArrayType.Vertex] = reducedVertices;
            arrays[(int)ArrayMesh.ArrayType.Index] = reducedIndexOffsets;
            arrays[(int)ArrayMesh.ArrayType.Normal] = reducedNormals;
            arrays[(int)ArrayMesh.ArrayType.TexUv] = reducedUvs;

            array_mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);

            //Change surface material
            array_mesh.SurfaceSetMaterial(0, BlockMaterials.dirtMaterial);

            return array_mesh;
        }
    }
}
