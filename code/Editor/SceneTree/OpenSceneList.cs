﻿
using System;

public partial class OpenSceneList : Widget
{
	public OpenSceneList( Widget parent ) : base( parent )
	{
		MinimumHeight = Theme.RowHeight;
		Layout = Layout.Row();
		Layout.Margin = new Sandbox.UI.Margin( 2, 2, 2, 2 );
		Layout.Spacing = 2;
	}

	public void BuildUI()
	{
		Layout.Clear( true );

		if ( GameManager.IsPlaying )
		{
			AddSceneButton( Scene.Active );
		}

		foreach ( var scene in EditorScene.OpenScenes )
		{
			AddSceneButton( scene );
		}

		Layout.AddStretchCell();
	}

	protected override void OnPaint()
	{
		Paint.ClearPen();
		Paint.SetBrush( Theme.ControlBackground );
		Paint.DrawRect( LocalRect, 5 );
	}

	void AddSceneButton( Scene scene )
	{
		Layout.Add( new SceneTabButton( scene ) );
	}

	int rebuildHash;

	[EditorEvent.Frame]
	public void CheckForChanges()
	{
		HashCode hash = new();

		foreach ( var scene in EditorScene.OpenScenes )
		{
			hash.Add( scene );
		}

		hash.Add( Scene.Active );

		if ( rebuildHash == hash.ToHashCode() ) return;
		rebuildHash = hash.ToHashCode();

		BuildUI();
	}
}
