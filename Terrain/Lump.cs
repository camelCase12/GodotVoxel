using Godot;

namespace ChaseVoxel.Terrain
{
    public class Lump
    {
        public bool empty = true;
        private MeshInstance meshInstance;

        public MeshInstance getMeshInstance()
        {
            return meshInstance;
        }
        public void setMeshInstance(MeshInstance meshInstance)
        {
            this.meshInstance = meshInstance;
            empty = false;
        }

        public Lump(MeshInstance meshInstance)
        {
            this.meshInstance = meshInstance;
            empty = false;
        }
        public Lump() { }
    }
}
