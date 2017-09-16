using System;
using System.Collections.Generic;

using Motorola.Magnum.UIControls;

namespace Motorola.Magnum.ScanItem
{
	/// <summary>
	/// a screen within a form
	/// </summary>
	public abstract class ProgramScreen
	{
		// member variables
		public MainForm mainForm = null;

		/// <summary>
		/// initializes the member variables
		/// </summary>
		/// <param name="mainForm">form to display the screen on</param>
		public ProgramScreen(MainForm mainForm)
		{
			this.mainForm = mainForm;
		}

		/// <summary>
		/// initializes this screen
		/// </summary>
		/// <returns>true if successful, false if not</returns>
		public virtual bool Initialize()
		{
			return true;
		}

		/// <summary>
		/// displays this screen
		/// </summary>
		public abstract void Display();

		/// <summary>
		/// cleans up when the screen is being closed
		/// </summary>
		public virtual void Done()
		{
		}

		/// <summary>
		/// performs the action associated with the selected list item
		/// </summary>
		/// <param name="item">selected list item</param>
		/// <returns>new screen to be displayed, null to stay on the same screen</returns>
		public virtual ProgramScreen ExecuteListItem(ScrollableListItem item)
		{
			return null;
		}

		/// <summary>
		/// performs the action associated with the left soft key
		/// </summary>
		/// <returns>new screen to be displayed, null to stay on the same screen, 'this' to pop to the previous screen</returns>
		public virtual ProgramScreen LeftSoftKeyPressed()
		{
			return null;
		}

		/// <summary>
		/// performs the action associated with the right soft key
		/// </summary>
		/// <returns>new screen to be displayed, null to stay on the same screen, 'this' to pop to the previous screen</returns>
		public virtual ProgramScreen RightSoftKeyPressed()
		{
			return this;
		}

		/// <summary>
		/// called when the list selection changes
		/// </summary>
		public virtual void SelectionChanged(object sender, EventArgs e)
		{
		}
	}

	/// <summary>
	/// a stack of ProgramScreen's
	/// </summary>
	public class ProgramScreenStack : Stack<ProgramScreen> { }
}
