using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Especialização do gatilho para armas de fogo.
/// </summary>
public class GunTrigger : Trigger
{
	/// <summary>
	/// Tempo que a arma passará se preparando a cada tiro.
	/// </summary>
	public float TimeBetweenShots;

	/// <summary>
	/// Tempo que a arma passa se preparando (segurando o Trigger) para dar o primeiro tiro.
	/// </summary>
	public float TimePrepareFirstShot;
}
