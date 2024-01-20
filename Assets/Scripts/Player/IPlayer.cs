using System.Collections.Generic;
using UnityEngine;

namespace MoonKart
{
	/// <summary>
	/// Player Interface  That have player Properties
	/// </summary>
	public interface IPlayer  
	{
		string UserID   { get; }
		string Nickname { get; }
		int Map      { get; }
		bool isReady      { get; }
		string CarPresetIndex      { get; }
		
	}
}
