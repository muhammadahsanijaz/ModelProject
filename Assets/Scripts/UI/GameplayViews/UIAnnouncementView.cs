using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MoonKart.UI
{
	public class UIAnnouncementView : UIView 
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private Announcement[] _announcements;

		private Announcement _activeAnnouncement;

		// UIView INTERFACE

		protected override void OnGameSet()
		{
			base.OnGameSet();
		}

		protected override void OnGameCleared()
		{
			base.OnGameCleared();
		}

		protected override void OnOpen()
		{
			base.OnOpen();

			for (int i = 0; i < _announcements.Length; i++)
			{
				HideAnnouncement(_announcements[i]);
			}
		}

		protected override void OnTick()
		{
			base.OnTick();

			if (_activeAnnouncement == null)
				return;

			_activeAnnouncement.Time += Time.deltaTime;

			if (_activeAnnouncement.Time >= _activeAnnouncement.Duration)
			{
				HideAnnouncement(_activeAnnouncement);
				_activeAnnouncement = null;
				return;
			}

			if (_activeAnnouncement.FadeGroup == null)
				return;

			if (_activeAnnouncement.FadeIn == true)
			{
				float progress = _activeAnnouncement.Time / _activeAnnouncement.FadeInTime;
				_activeAnnouncement.FadeGroup.alpha = DOVirtual.EasedValue(0f, 1f, progress, Ease.OutQuad);
			}
			else if (_activeAnnouncement.FadeOut == true)
			{
				float progress = (_activeAnnouncement.Time - (_activeAnnouncement.Duration - _activeAnnouncement.FadeOutTime)) / _activeAnnouncement.FadeOutTime;
				_activeAnnouncement.FadeGroup.alpha = DOVirtual.EasedValue(1f, 0f, progress, Ease.OutQuad);
			}
			else
			{
				_activeAnnouncement.FadeGroup.alpha = 1f;
			}
		}

		// PRIVATE METHODS

		private void OnRaceStarted()
		{
			ShowAnnouncement(EType.RaceStarted);
		}

		private void OnWrongWay()
		{
			ShowAnnouncement(EType.WrongWay);
		}

		private void OnCheckpointPassed()
		{
			ShowAnnouncement(EType.CheckpointPassed);
		}

		private void OnLapFinished()
		{
			//ShowAnnouncement(EType.LapFinished, lapFinishedEvent.Time.AsFloat.ToString("f2"));
		}

		private void OnPlayerFinished()
		{
			//ShowAnnouncement(EType.PlayerFinished, playerFinishedEvent.Time.ToString("f2"));
		}

		private void ShowAnnouncement(EType type, string messageParameter = null)
		{
			//ShowAnnouncement(_announcements.Find(t => t.Type == type), messageParameter);
		}

		private void ShowAnnouncement(Announcement announcement, string messageParameter = null)
		{
			if (announcement == null)
				return;

			HideAnnouncement(_activeAnnouncement);
			HideAnnouncement(announcement);

			if (announcement.Text != null)
			{
				if (announcement.Message.HasValue() == true)
				{
					announcement.Text.text = messageParameter.HasValue() == true ? string.Format(announcement.Message, messageParameter) : announcement.Message;
				}
				else if (messageParameter.HasValue() == true)
				{
					announcement.Text.text = messageParameter;
				}
			}


			announcement.Time = 0f;

			_activeAnnouncement = announcement;
		}

		private void HideAnnouncement(Announcement announcement)
		{
			if (announcement == null)
				return;

			if (announcement.Text != null)
			{
				announcement.Text.text = string.Empty;
			}

			if (announcement.Group != null)
			{
				announcement.Group.SetActive(false);
			}

			announcement.FadeGroup.SetVisibility(false);
		}

		// CLASSES

		[Serializable]
		private class Announcement
		{
			public EType Type;
			[TextArea]
			public string Message;
			public TextMeshProUGUI Text;
			public GameObject Group;
			public CanvasGroup FadeGroup;
			public float Duration;
			public float FadeInTime = 0.25f;
			public float FadeOutTime = 0.25f;

			public float Time { get; set; }
			public bool FadeIn => FadeInTime > 0f && Time < FadeInTime;
			public bool FadeOut => FadeOutTime > 0f && Time > Duration - FadeOutTime;
		}

		public enum EType
		{
			None,
			RaceStarted,
			CheckpointPassed,
			WrongWay,
			LapFinished,
			PlayerFinished,
		}
	}
}
