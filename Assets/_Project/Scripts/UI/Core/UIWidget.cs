using System.Collections.Generic;
using UnityEngine;

namespace MoonKart.UI
{
	public abstract class UIWidget : UIBehaviour
	{
		// PUBLIC MEMBERS

		public bool IsVisible { get; private set; }

		// PROTECTED MEMBERS

		protected bool IsInitalized { get; private set; }
		protected GameUI GameUI { get; private set; }
		protected GameContext Context { get { return GameUI.Context; } }
		protected UIWidget Owner { get; private set; }

		private List<UIWidget> _childs = new List<UIWidget>(32);

// PUBLIC METHODS

		public void PlayClickSound()
		{
			if (GameUI != null)
			{
				GameUI.PlayClickSound();
			}
		}

		public void PlaySound(AudioSetup sound)
		{
			if (sound == null)
			{
				Debug.LogWarning($"Missing click sound, parent {name}");
				return;
			}

			if (GameUI != null)
			{
				GameUI.PlaySound(sound);
			}
		}

		// INTERNAL METHODS

		internal void Initialize(GameUI gameUI, UIWidget owner)
		{
			if (IsInitalized == true)
				return;

			GameUI = gameUI;
			Owner = owner;

			_childs.Clear();

			// TODO: Do not include widgets that are under different widget (widget hierarchy)
			GetComponentsInChildren(true, _childs);

			_childs.Remove(this);

			for (int i = 0; i < _childs.Count; i++)
			{
				_childs[i].Initialize(gameUI, this);
			}

			OnInitialize();

			IsInitalized = true;

			if (gameObject.activeInHierarchy == true)
			{
				Visible();
			}
		}

		internal void Deinitialize()
		{
			if (IsInitalized == false)
				return;

			Hidden();

			OnDeinitialize();

			for (int i = 0; i < _childs.Count; i++)
			{
				_childs[i].Deinitialize();
			}

			_childs.Clear();

			IsInitalized = false;

			GameUI = null;
			Owner = null;
		}

		internal void GameSet()
		{
			if (IsInitalized == false)
				return;

			OnGameSet();

			for (int i = 0; i < _childs.Count; i++)
			{
				_childs[i].GameSet();
			}
		}

		internal void GameCleared()
		{
			if (IsInitalized == false)
				return;

			OnGameCleared();

			for (int i = 0; i < _childs.Count; i++)
			{
				_childs[i].GameCleared();
			}
		}

		internal void Visible(object obj = null)
		{
			if (IsInitalized == false)
				return;

			if (IsVisible == true)
				return;

			if (gameObject.activeSelf == false)
				return;

			IsVisible = true;

			for (int i = 0; i < _childs.Count; i++)
			{
				_childs[i].Visible();
			}
			if (obj == null)
			{
				OnVisible();
            }
            else
            {
				OnVisible(obj);
            }
		}

		internal void Hidden()
		{
			if (IsVisible == false)
				return;

			IsVisible = false;

			OnHidden();

			for (int i = 0; i < _childs.Count; i++)
			{
				_childs[i].Hidden();
			}
		}

		internal void Tick()
		{
			if (IsInitalized == false)
				return;

			OnTick();

			for (int i = 0; i < _childs.Count; i++)
			{
				_childs[i].Tick();
			}
		}

		internal void AddChild(UIWidget widget)
		{
			if (widget == null || widget == this)
				return;

			if (_childs.Contains(widget) == true)
			{
				Debug.LogError($"Widget {widget.name} is already added as child of {name}");
				return;
			}

			_childs.Add(widget);

			widget.Initialize(GameUI, this);

			if (GameUI.QuantumGameSet == true)
			{
				widget.GameSet();
			}
		}

		internal void RemoveChild(UIWidget widget)
		{
			int childIndex = _childs.IndexOf(widget);

			if (childIndex < 0)
			{
				Debug.LogError($"Widget {widget.name} is not child of {name} and cannot be removed");
				return;
			}

			if (GameUI.QuantumGameSet == true)
			{
				widget.GameCleared();
			}

			widget.Deinitialize();

			_childs.RemoveAt(childIndex);
		}

		// MONOBEHAVIOR

		protected void OnEnable()
		{
			Visible();
		}

		protected void OnDisable()
		{
			Hidden();
		}

	#if UNITY_EDITOR
		protected void OnDestroy()
		{
			if (IsInitalized == true)
			{
				string ownerName = Owner != null ? Owner.name : "None";
				Debug.LogError($"Widget {name} with owner {ownerName} was not correctly deinitialized");
			}
		}
	#endif

		// UIWidget INTERFACE

		public virtual bool IsActive() { return true; }

		protected virtual void OnInitialize() { }
		protected virtual void OnInitialize(object obj) { }
		protected virtual void OnDeinitialize() { }
		protected virtual void OnGameSet() { }
		protected virtual void OnGameCleared() { }
		protected virtual void OnVisible() { }
		protected virtual void OnVisible(object obj) { }
		protected virtual void OnHidden() { }
		protected virtual void OnTick() { }
	}
}
