﻿using System;
using System.Windows.Forms;

namespace NotepadSharp.Utils
{
    public static class HotKeyManager {

		public static bool Enable { get; set; } = true;

		public static void AddHotKey(Form form, Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false) {
			form.KeyPreview = true;

			form.KeyDown += delegate(object sender, KeyEventArgs e) {
				if (IsHotkey(e, key, ctrl, shift, alt)) {
					function();
				}
			};
		}

		public static bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false) {
			return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
		}


	}
}
