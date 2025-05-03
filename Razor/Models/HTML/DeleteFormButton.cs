using System.Diagnostics.CodeAnalysis;

namespace HaFT.Utilities.Razor.Models.HTML;

public class DeleteFormButton : Form
{
	[SetsRequiredMembers]
	public DeleteFormButton() : this(s_defaultOptions) { }

	[SetsRequiredMembers]
	public DeleteFormButton(Options options)
	{
		Children = [$"<input type='submit' value='{options.ButtonText}' class='btn btn-outline-danger' />"];

		if (!string.IsNullOrEmpty(options.ConfirmationText))
			OnSubmit = "return confirm(\"Are you sure to delete?\")";

		if (options.OpenInNewTab)
			Attributes["target"] = "_blank";
	}

	static readonly Options s_defaultOptions = new();

	public class Options
	{
		public bool OpenInNewTab { get; set; }

		/// <summary>
		/// null or empty means no confirmation
		/// </summary>
		public string? ConfirmationText { get; set; } = "Are you sure to delete?";

		public string ButtonText { get; set; } = "Delete";
	}
}
