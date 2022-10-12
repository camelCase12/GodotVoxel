using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaseVoxel.Terrain
{
    public class CubeUtils
    {
        /// <summary>
        /// Returns a set of vertices which the open sides have been eliminated from.
        /// </summary>
        /// <param name="openSides">Bool array representing which sides are open to air</param>
        /// <returns>Only vertices exposed to air</returns>
        public static Vector3[] GetReducedVertices(bool[] openSides)
        {
            int vertexLength = 0;
            foreach (var side in openSides)
            {
                if (side) vertexLength += 6;
            }
            Vector3[] reducedVertices = new Vector3[vertexLength];

            int currentIndex = 0;

            if (openSides[0])
            {
                Array.Copy(vertexOffsets, 0, reducedVertices, 0, 6);
                currentIndex += 6;
            }
            if (openSides[1])
            {
                Array.Copy(vertexOffsets, 6, reducedVertices, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[2])
            {
                Array.Copy(vertexOffsets, 12, reducedVertices, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[3])
            {
                Array.Copy(vertexOffsets, 18, reducedVertices, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[4])
            {
                Array.Copy(vertexOffsets, 24, reducedVertices, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[5])
            {
                Array.Copy(vertexOffsets, 30, reducedVertices, currentIndex, 6);
                currentIndex += 6;
            }

            return reducedVertices;
        }
        public static Vector3[] GetReducedNormals(bool[] openSides)
        {
            int normalLength = 0;
            foreach (var side in openSides)
            {
                if (side) normalLength += 6;
            }
            Vector3[] reducedNormals = new Vector3[normalLength];

            int currentIndex = 0;

            if (openSides[0])
            {
                Array.Copy(normals, 0, reducedNormals, 0, 6);
                currentIndex += 6;
            }
            if (openSides[1])
            {
                Array.Copy(normals, 6, reducedNormals, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[2])
            {
                Array.Copy(normals, 12, reducedNormals, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[3])
            {
                Array.Copy(normals, 18, reducedNormals, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[4])
            {
                Array.Copy(normals, 24, reducedNormals, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[5])
            {
                Array.Copy(normals, 30, reducedNormals, currentIndex, 6);
                currentIndex += 6;
            }

            return reducedNormals;
        }
        public static Vector2[] GetReducedUvs(bool[] openSides)
        {
            int uvLength = 0;
            foreach (var side in openSides)
            {
                if (side) uvLength += 6;
            }
            Vector2[] reducedUvs = new Vector2[uvLength];

            int currentIndex = 0;

            if (openSides[0])
            {
                Array.Copy(uvs, 0, reducedUvs, 0, 6);
                currentIndex += 6;
            }
            if (openSides[1])
            {
                Array.Copy(uvs, 6, reducedUvs, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[2])
            {
                Array.Copy(uvs, 12, reducedUvs, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[3])
            {
                Array.Copy(uvs, 18, reducedUvs, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[4])
            {
                Array.Copy(uvs, 24, reducedUvs, currentIndex, 6);
                currentIndex += 6;
            }
            if (openSides[5])
            {
                Array.Copy(uvs, 30, reducedUvs, currentIndex, 6);
                currentIndex += 6;
            }

            return reducedUvs;
        }
        public static int[] GetReducedIndexOffsets(bool[] openSides)
        {
            int indexOffsetLength = 0;
            foreach (var side in openSides)
            {
                if (side) indexOffsetLength += 6;
            }
            int[] reducedIndexOffsets = new int[indexOffsetLength];

            for (int i = 0; i < reducedIndexOffsets.Length; i++) reducedIndexOffsets[i] = i;

            return reducedIndexOffsets;
        }

        public static Vector3[] vertexOffsets = new Vector3[36]
            {
                new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 1),
                new Vector3(0, 0, 0), new Vector3(0, 1, 1), new Vector3(0, 0, 1),
                new Vector3(0, 0, 1), new Vector3(0, 1, 1), new Vector3(1, 1, 1),
                new Vector3(0, 0, 1), new Vector3(1, 1, 1), new Vector3(1, 0, 1),
                new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 0),
                new Vector3(1, 0, 1), new Vector3(1, 1, 0), new Vector3(1, 0, 0),
                new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),
                new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 0),
                new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0),
                new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, 0),
                new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1),
                new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 1, 1)
            };
        public static Vector3[] normals = new Vector3[36]
            {
                new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0),
                new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0),
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0),
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
                new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0),
                new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0),
                new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0),
            };
        public static Vector2[] uvs = new Vector2[36]
        {
            new Vector2(0, 2f/3), new Vector2(0, 1f/3), new Vector2(0.5f, 1f/3),
            new Vector2(0, 2f/3), new Vector2(0.5f, 1f/3), new Vector2(0.5f, 2f/3),
            new Vector2(0, 1), new Vector2(0, 2f/3), new Vector2(0.5f, 2f/3),
            new Vector2(0, 1), new Vector2(0.5f, 2f/3), new Vector2(0.5f, 1),
            new Vector2(0.5f, 1f/3), new Vector2(0.5f, 0), new Vector2(1, 0),
            new Vector2(0.5f, 1f/3), new Vector2(1, 0), new Vector2(1, 1f/3),
            new Vector2(0.5f, 2f/3), new Vector2(0.5f, 1f/3), new Vector2(1, 1f/3),
            new Vector2(0.5f, 2f/3), new Vector2(1, 1f/3), new Vector2(1, 2f/3),
            new Vector2(0.5f, 1), new Vector2(0.5f, 2f/3), new Vector2(1, 2f/3),
            new Vector2(0.5f, 1), new Vector2(1, 2f/3), new Vector2(1, 1),
            new Vector2(0, 1f/3), new Vector2(0, 0), new Vector2(0.5f, 0),
            new Vector2(0, 1f/3), new Vector2(0.5f, 0), new Vector2(0.5f, 1f/3)
        };
        public static int[] indexOffsets = new int[36]
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
                18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35
            };
    }
}
