using MauiToolkit.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.Maui;
using System.ComponentModel;
using System.Reflection;

namespace MauiToolkit.Pages;

/// <summary>
/// A <see cref="ContentPage" /> that hosts Blazor pages and components.
/// </summary>
public partial class BlazorPage : ContentPage
{
	#region Public Constructors

	/// <summary>
	/// Initializes a new <see cref="BlazorPage" />.
	/// </summary>
	public BlazorPage()
	{
		// Load XAML
		InitializeComponent();

		// Set the host page
		webView.HostPage = HostPage;

		// If the app assembly isn't defined, use the one that started the process
		if (AppAssembly == null)
		{
			AppAssembly = Assembly.GetEntryAssembly()!;
		}

		// Add root components
		AddRootComponents();
	}

	#endregion Public Constructors

	#region Event Handlers

	private void webView_UrlLoading(object sender, UrlLoadingEventArgs e)
	{
		OnUrlLoading(sender, e);
	}

	#endregion Event Handlers

	#region Overridables / Event Triggers

	/// <summary>
	/// Occurs when the <see cref="WebView" /> is loading a URL.
	/// </summary>
	/// <param name="sender">
	/// The <see cref="WebView" />.
	/// </param>
	/// <param name="e">
	/// A <see cref="UrlLoadingEventArgs" /> that contains the URL data.
	/// </param>
	protected virtual void OnUrlLoading(object sender, UrlLoadingEventArgs e)
	{
	}

	#endregion Overridables / Event Triggers

	#region Protected Methods

	/// <summary>
	/// Adds the default root components when the view is created.
	/// </summary>
	/// <remarks>
	/// The default components are a <see cref="BlazorHost"/>
	/// </remarks>
	protected virtual void AddRootComponents()
	{
		// Add the RootComponent to the RootComponents list
		webView.RootComponents.Add(CreateHostComponent());
	}

	/// <summary>
	/// Creates a <see cref="RootComponent"/> that loads a <see cref="BlazorHost"/>.
	/// </summary>
	/// <returns>
	/// The <see cref="RootComponent"/>.
	/// </returns>
	protected RootComponent CreateHostComponent()
	{
		// Create a RootComponent for the component
		RootComponent hostComponent = new RootComponent()
		{
			// Set the type to the host
			ComponentType = typeof(BlazorHost),

			// Use the selector specified in properties
			Selector = Selector,
		};

		// Create parameters
		Dictionary<string, object?> parameters = new();

		// Specify the app assembly, from properties
		parameters.Add("AppAssembly", AppAssembly);

		// Add an initial route if defined
		if (BlazorRoute == null)
		{
            parameters.Add("InitialRoute", BlazorRoute);
        }

        // Set parameters
        hostComponent.Parameters = parameters;

		// Done
		return hostComponent;
	}

	#endregion Protected Methods

	#region Public Properties

	/// <summary>
	/// Gets or sets the path to the HTML file to render.
	/// <para>This is an app relative path to the file such as <c>wwwroot/index.html</c></para>
	/// </summary>
	public string HostPage { get; set; } = "wwwroot/index.html";

    /// <summary>
    /// Gets or sets the initial path to load in the Blazor router.
    /// </summary>
    public string? BlazorRoute { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Assembly" /> that will be used to resolve Blazor components and pages.
    /// </summary>
    public Assembly AppAssembly { get; set; }

	/// <summary>
	/// Gets or sets the CSS selector string that specifies where in the document the component should be placed. This
	/// must be unique among the root components within the <see cref="BlazorWebView" />.
	/// </summary>
	public string Selector { get; set; } = "#app";

	/// <summary>
	/// Gets the <see cref="BlazorWebView" /> hosted in the page.
	/// </summary>
	public BlazorWebView WebView { get => webView; }

	#endregion Public Properties
}