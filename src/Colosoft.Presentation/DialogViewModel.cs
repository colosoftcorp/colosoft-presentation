using System;

namespace Colosoft.Presentation
{
    public class DialogViewModel : ViewModel, IDialogViewModel
    {
        public IDialogAccessor DialogAccessor { get; }

        public DialogViewModel(IDialogAccessorFactory dialogAccessorFactory)
        {
            if (dialogAccessorFactory is null)
            {
                throw new ArgumentNullException(nameof(dialogAccessorFactory));
            }

            this.DialogAccessor = dialogAccessorFactory.Create(this);
        }
    }
}
