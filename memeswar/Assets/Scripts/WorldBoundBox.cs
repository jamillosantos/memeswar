using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// Script extensão do unity que cria uma borda no mundo para limitar a movimentação dos jogadores, e projéteis.
/// </summary>
public class WorldBoundBox : MonoBehaviour
{
	private const int TOP = 0;
	private const int RIGHT = 1;
	private const int BOTTOM = 2;
	private const int LEFT = 3;

	private const int COUNT = 4;

	private BoxCollider[] _walls;

	public float Width = 1f;

	public float Height = 1f;

	public float Depth = 1f;

	void Start()
	{
		this._walls = new BoxCollider[COUNT];
		for (int i = 0; i < COUNT; i++)
			this._walls[i] = this.gameObject.AddComponent<BoxCollider>();
		this.applyTrans();
	}

	/// <summary>
	/// Altera a posição das "paredes".
	/// </summary>
	private void applyTrans()
	{
		if (this._walls != null)
		{
			this._walls[TOP].center = new Vector3(0f, -this.Height / 2f, 0f);
			this._walls[TOP].size = new Vector3(this.Width, 1f, this.Depth);
		
			this._walls[RIGHT].center = new Vector3(this.Width / 2f, 0f, 0f);
			this._walls[RIGHT].size = new Vector3(1f, this.Height, this.Depth);
		
			this._walls[BOTTOM].center = new Vector3(0f, this.Height / 2f, 0f);
			this._walls[BOTTOM].size = new Vector3(this.Width, 1f, this.Depth);
		
			this._walls[LEFT].center = new Vector3(-this.Width / 2f, 0f, 0f);
			this._walls[LEFT].size = new Vector3(1f, this.Height, this.Depth);
		}
	}

	/// <summary>
	/// Evento chamado apenas enquanto no editor para exibir visualmente as paredes enquanto desenvolvendo.
	/// </summary>
	void OnDrawGizmos()
	{
		Gizmos.color = Color.gray;
		this.drawGizmos();
	}

	/// <summary>
	/// Evento chamado apenas enquanto no editor para exibir visualmente as paredes selecionadas enquanto desenvolvendo.
	/// </summary>
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		this.drawGizmos();
	}

	/// <summary>
	/// Desenha as paredes.
	/// </summary>
	private void drawGizmos()
	{
		Gizmos.DrawCube(this.transform.position + new Vector3(0f, -this.Height / 2f, 0f), new Vector3(this.Width, 1f, this.Depth));
		Gizmos.DrawCube(this.transform.position + new Vector3(this.Width / 2f, 0f, 0f), new Vector3(1f, this.Height, this.Depth));
		Gizmos.DrawCube(this.transform.position + new Vector3(0f, this.Height / 2f, 0f), new Vector3(this.Width, 1f, this.Depth));
		Gizmos.DrawCube(this.transform.position + new Vector3(-this.Width / 2f, 0f, 0f), new Vector3(1f, this.Height, this.Depth));
	}
}
