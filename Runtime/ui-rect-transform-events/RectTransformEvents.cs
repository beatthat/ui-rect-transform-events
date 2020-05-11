using BeatThat.Rects;
using UnityEngine.EventSystems;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace BeatThat.UIRectTransformEvents
{
	/// <summary>
	/// Trigger component that attaches to a RectTransform and fires an event when that RectTransform's screen rect changes.
	/// Unity's UIBehaviour has a related message 'OnRectTransformDimensionsChange';
	/// unfortunately, that message is only sent when the RectTransform's dimensions change,
	/// NOT when the RectTransform's position changes.
	/// </summary>
	[ExecuteInEditMode]
	public class RectTransformEvents : UIBehaviour 
	{
		public enum ChangeDetection { ANCHORED_POSITION = 0, SCREEN_RECT = 1 }
		public ChangeDetection m_changeDetection;

		// Analysis disable ConvertToAutoProperty
		public ChangeDetection changeDetection { get { return m_changeDetection; } set { m_changeDetection = value; } }
		// Analysis restore ConvertToAutoProperty

		[SerializeField] private UnityEvent m_onScreenRectChanged = new UnityEvent();
		public UnityEvent onScreenRectChanged { get { return m_onScreenRectChanged; } set { m_onScreenRectChanged = value; } }

		override protected void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			m_onScreenRectChangedEventPending = true;
		}

		private RectTransform rectTransform { get { return m_rectTransform?? (m_rectTransform = GetComponent<RectTransform>()); } }
		[NonSerialized]private RectTransform m_rectTransform;

		override protected void OnEnable()
		{
			m_lastPos = this.rectTransform.anchoredPosition;
			m_onScreenRectChangedEventPending = false;
		}

		/// <summary>
		/// Call before changing RectTransform if you don't want events fired
		/// </summary>
		public void DisableEvents()
		{
			this.eventsDisabled = true;
		}

		/// <summary>
		/// Call after changing RectTransform if you DisableEvents for a change
		/// </summary>
		public void SyncAndRenableEvents()
		{
			m_lastPos = this.rectTransform.anchoredPosition;
			m_onScreenRectChangedEventPending = false;
			this.eventsDisabled = false;
		}

		private bool eventsDisabled { get; set; }

		void Update()
		{
			switch(m_changeDetection) {
			case ChangeDetection.ANCHORED_POSITION:
				var curPos = this.rectTransform.anchoredPosition;
				if(curPos != m_lastPos) {
					m_lastPos = curPos;
					m_onScreenRectChangedEventPending = true;
				}
				break;
			case ChangeDetection.SCREEN_RECT:
				var curRect = this.rectTransform.GetScreenRect();
				if(curRect != m_lastRect) {
					m_lastRect = curRect;
					m_onScreenRectChangedEventPending = true;
				}
				break;
			}

			if(m_onScreenRectChangedEventPending && !this.eventsDisabled) {
				this.onScreenRectChanged.Invoke();
			}

			m_onScreenRectChangedEventPending = false;
		}

		private bool m_onScreenRectChangedEventPending;
		private Vector2 m_lastPos;
		private Rect m_lastRect;
	}
}



