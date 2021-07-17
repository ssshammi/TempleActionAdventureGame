﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

namespace PixLi
{
	public class DialogueUserInterfaceView : View
	{
		[System.Serializable]
		public class DialogueUserInterfaceViewDisplayData
		{
			public DialogueData DialogueData;

			public DialogueUserInterfaceViewDisplayData(DialogueData dialogueData)
			{
				this.DialogueData = dialogueData;
			}
		}

		[System.Serializable]
		public class DialogueUserInterfaceViewOutput
		{
			[SerializeField] private TextMeshProUGUI _messageTextField;
			public TextMeshProUGUI _MessageTextField => this._messageTextField;

			[SerializeField] private TextMeshProUGUI _collocutorNameTextField;
			public TextMeshProUGUI _CollocutorNameTextField => this._collocutorNameTextField;

			[SerializeField] private Image _collocutorIconImageField;
			public Image _CollocutorIconImageField => this._collocutorIconImageField;
		}

		[SerializeField] protected DialogueUserInterfaceViewOutput viewOutput;
		public DialogueUserInterfaceViewOutput _ViewOutput => this.viewOutput;

		public void Display(DialogueUserInterfaceViewDisplayData displayData)
		{
			DialogueData dialogueData = displayData.DialogueData;

			//AudioPlayer.PlayClip(dialogueData._AudioCover, AudioPlayer.AudioPlayerType.Specialty);

			this.viewOutput._MessageTextField.text = dialogueData._SentenceText;

			this.viewOutput._CollocutorNameTextField.text = dialogueData._Collocutor._ProfileName;
			this.viewOutput._CollocutorIconImageField.sprite = dialogueData._Collocutor._ProfileIcon;
		}

		private DialogueUserInterfaceViewDisplayData _displayedData;
		private Dialogue _displayedDialogue;
		private int _displayDialogueDataIndex;

		public virtual void DisplayDialogue(Dialogue dialogue)
		{
			this._displayedDialogue = dialogue;
			this._displayedData = new DialogueUserInterfaceViewDisplayData(this._displayedDialogue._DialogueData[0]);

			this._displayDialogueDataIndex = 0;

			this.Display(this._displayedData);
		}

		public void DisplayNext()
		{
			this._displayDialogueDataIndex++;

			if (this._displayDialogueDataIndex < this._displayedDialogue._DialogueData.Count)
			{
				this._displayedData.DialogueData = this._displayedDialogue._DialogueData[this._displayDialogueDataIndex];
				this.Display(this._displayedData);
			}
			else
				this.Hide();
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}

		[CustomEditor(typeof(DialogueUserInterfaceView))]
		[CanEditMultipleObjects]
		public class DialogueUserInterfaceViewEditor : ViewEditor
		{
		}
#endif
	}
}