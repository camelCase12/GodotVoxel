using ChaseVoxel.Terrain;
using Godot;
using System.Diagnostics;

public class VoxelBuilder : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Assign VoxelBuilder reference to MeshUtils
		MeshUtils.voxelBuilder = this;

		
		for(int i = -2; i < 2; i++)
		{
			for(int j = -2; j < 2; j++)
			{
				ChunkManager.GenerateNewChunk(i*32, j*32);
			}
		}
		Stopwatch sw = new Stopwatch();
		sw.Restart();
		ChunkManager.chunks[0].regenerateLump(2);
		sw.Stop();
		GD.Print("Lump regeneration time: " + sw.Elapsed.TotalSeconds + " seconds");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
