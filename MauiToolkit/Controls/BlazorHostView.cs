using MauiToolkit.Components;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.Maui;
using System.Reflection;

namespace MauiToolkit.Controls
{
    /// <summary>
    /// A <see cref="BlazorWebView" /> that simplifies hosting app content.
    /// </summary>
    public class BlazorHostView : BlazorWebView
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new <see cref="BlazorHostView" />.
        /// </summary>
        public BlazorHostView()
        {
            base.BlazorWebViewInitializing += BlazorHostView_BlazorWebViewInitializing;
        }

        private void BlazorHostView_BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
        {
            // If no host page is defined, use an estimated default
            if (HostPage == null)
            {
                HostPage = "wwwroot/index.html";
            }

            // If the app assembly isn't defined, use the one that started the process
            if (AppAssembly == null)
            {
                AppAssembly = Assembly.GetEntryAssembly()!;
            }

            // Handle navigation
            UrlLoading += OnUrlLoading;

            // Add root components
            AddRootComponents();
        }

        #endregion Public Constructors

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
        protected virtual void OnUrlLoading(object? sender, UrlLoadingEventArgs e)
        {
        }

        #endregion Overridables / Event Triggers

        #region Protected Methods

        /// <summary>
        /// Adds the default root components when the view is created.
        /// </summary>
        /// <remarks>
        /// The default component is the <see cref="BlazorRouter" />
        /// </remarks>
        protected virtual void AddRootComponents()
        {
            // Add the router
            RootComponents.Add(CreateRouterComponent());
        }

        /// <summary>
        /// Creates a <see cref="RootComponent" /> that loads a <see cref="BlazorRouter" />.
        /// </summary>
        /// <returns>
        /// The <see cref="RootComponent" />.
        /// </returns>
        protected RootComponent CreateRouterComponent()
        {
            // Create a RootComponent for the router
            RootComponent routerComponent = new RootComponent()
            {
                // Set the type to the router
                ComponentType = typeof(BlazorRouter),

                // Use the selector specified in properties
                Selector = RouterSelector,
            };

            // Create parameters
            Dictionary<string, object?> parameters = new();

            // Specify the app assembly, from properties
            parameters.Add("AppAssembly", AppAssembly);

            // Add an initial route if defined
            if (InitialRoute != null)
            {
                parameters.Add("InitialRoute", InitialRoute);
            }

            // Set parameters
            routerComponent.Parameters = parameters;

            // Done
            return routerComponent;
        }

        #endregion Protected Methods

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="Assembly" /> that will be used to resolve Blazor components and pages.
        /// </summary>
        public Assembly? AppAssembly { get; set; }
        
        /// <summary>
        /// Gets or sets the initial path to load in the Blazor router.
        /// </summary>
        public string? InitialRoute { get; set; }

        /// <summary>
        /// Gets or sets the CSS selector string that specifies where in the document the router should be placed.
        /// This must be unique among the root components within the <see cref="BlazorWebView" />.
        /// </summary>
        public string RouterSelector { get; set; } = "#router";

        #endregion Public Properties
    }
}