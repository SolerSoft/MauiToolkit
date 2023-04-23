using MauiToolkit.Components;
using MauiToolkit.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace MauiToolkit.Markup
{
    /// <summary>
    /// Generates a <see cref="DataTemplate"/> that will load a Blazor Route through a BlazorWebView
    /// </summary>
    [ContentProperty(nameof(Route))]
    public sealed class BlazorTemplateExtension : IMarkupExtension<DataTemplate>
    {
        /// <summary>
        /// Creates a <see cref="DataTemplate"/> that renders a Blazor Route.
        /// </summary>
        /// <param name="route">
        /// The route to render.
        /// </param>
        /// <returns>
        /// The <see cref="DataTemplate"/>.
        /// </returns>
        private DataTemplate CreateRouteTemplate(string route)
        {
            // Create a DataTemplate that lazy loads the browser
            return new DataTemplate(() =>
            {
                // Create the Content Page and add it to the template
                ContentPage contentPage = new ContentPage();

                // Create the Blazor Host View
                BlazorHostView hostView = new BlazorHostView()
                {
                    // Use the components assembly
                    InitialRoute = route,

                    // Set the host page
                    HostPage = HostPage,
                };

                // Enable routing extensions
                hostView.EnableRoutingExtensions();

                // Set the page content to the Host View
                contentPage.Content = hostView;

                // Return the page
                return contentPage;
            });
        }

        #region IMarkupExtension Members

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<DataTemplate>).ProvideValue(serviceProvider);
        }

        #endregion // IMarkupExtension Members


        #region Public Methods

        public DataTemplate ProvideValue(IServiceProvider serviceProvider)
        {
            // Need a service provider to get other services
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            // If Route isn't specified, log with details about the xaml line where it's missing.
            if (string.IsNullOrEmpty(Route))
            {
                throw new XamlParseException($"{nameof(Route)} isn't set.", serviceProvider.GetLineInfo());
            }

            // Create the component template
            return CreateRouteTemplate(Route);
        }

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the Blazor component type that will be used to generate the template.
        /// </summary>
        public string Route { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path to the HTML file to render.
        /// <para>This is an app relative path to the file such as <c>wwwroot\index.html</c></para>
        /// </summary>
        public string HostPage { get; set; } = "wwwroot/index.html";

        /// <summary>
		/// Gets or sets the CSS selector string that specifies where in the document the router should be placed.
		/// This must be unique among the root components within the <see cref="BlazorWebView"/>.
        /// </summary>
        public string RouterSelector { get; set; } = "#router";

        #endregion Public Properties
    }
}