using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ilrLearnerEntry.Behaviors
{
 /// <summary>
    /// a scroll to end behaviour
    /// </summary>
    /// <seealso cref="Behavior{TextBox}" />
    public sealed class TextBoxUnsignedIntegersOnlyBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// Called when [attached].
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
            {
                AssociatedObject.PreviewTextInput += OnPreviewTextInput;
                AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
            }
        }

        /// <summary>
        /// Called when [detaching].
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            }
        }

        private static readonly Regex _regex = new Regex("[^0-9]+", RegexOptions.Compiled);

        private bool IsValid(string text)
        {
            return _regex.IsMatch(text);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = IsValid(e.Text);

        private void OnPreviewKeyDown(object sender, KeyEventArgs e) =>
            e.Handled = e.Key == Key.Space;

    }
}
