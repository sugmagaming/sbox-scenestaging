using Sandbox;

public sealed class FractalComponent : Component
{
    public void SpawnFractal( Vector3 position, float size, int depth )
    {
        // Create a new cube at the given position
        var cube = new ModelRenderer();


        cube.Model = Model.Load( "models/sbox_props/watermelon/watermelon.vmdl" );
        cube.Transform.Position = position;
        cube.Transform.Scale = size / 100.0f;

        // If we've reached the maximum depth, stop recursing
        if ( depth <= 0 ) return;

        // Calculate the size and position for the smaller cubes
        float newSize = size / 3;
        float offset = newSize;

        // Spawn smaller cubes in each corner of the current cube
        for ( int x = -1; x <= 1; x += 2 )
        {
            for ( int y = -1; y <= 1; y += 2 )
            {
                for ( int z = -1; z <= 1; z += 2 )
                {
                    Vector3 newPosition = position + new Vector3( x, y, z ) * offset;
                    SpawnFractal( newPosition, newSize, depth - 1 );
                }
            }
        }
    }

    public void SpawnFractal()
    {
        // Spawn a fractal cube at the center of the map
        SpawnFractal( Vector3.Zero, 100, 3 );
    }
}