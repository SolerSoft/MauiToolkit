namespace SolerSoft.Maui.Mvvm;

/// <summary>
/// Base class for Views
/// </summary>
public class ViewPage : ContentPage { }

/// <summary>
/// Base class for Views with a ViewModel.
/// </summary>
/// <typeparam name="TViewModel">
/// The type of the ViewModel.
/// </typeparam>
public class ViewPage<TViewModel> : ViewPage where TViewModel : ViewModel, new()
{
    #region Private Fields

    private TViewModel viewModel;

    #endregion Private Fields

    /// <summary>
    /// Ensures that the ViewModel has been created and the BindingContext is using it.
    /// </summary>
    private void EnsureViewModel()
    {
        if (BindingContext is TViewModel vm)
        {
            viewModel = vm;
        }
        else
        {
            viewModel = new TViewModel();
            BindingContext = viewModel;
        }
    }

    #region Public Constructors

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the ViewModel for the view.
    /// </summary>
    public TViewModel ViewModel
    {
        get
        {
            EnsureViewModel();
            return viewModel;
        }
    }

    #endregion Public Properties
}