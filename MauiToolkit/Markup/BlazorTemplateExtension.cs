using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace MauiToolkit.Markup
{
    /// <summary>
    /// Generates a <see cref="DataTemplate"/> that will load a Blazor component through a BlazorWebView
    /// </summary>
    [ContentProperty(nameof(ComponentType))]
    public sealed class BlazorTemplateExtension : IMarkupExtension<DataTemplate>
    {
        /// <summary>
        /// Creates a <see cref="DataTemplate"/> that renders a Blazor component.
        /// </summary>
        /// <param name="componentType">
        /// The type of the component to render.
        /// </param>
        /// <returns>
        /// The <see cref="DataTemplate"/>.
        /// </returns>
        private DataTemplate CreateComponentTemplate(Type componentType)
        {
            // Create a DataTemplate that lazy loads the browser
            return new DataTemplate(() =>
            {
                // Create the Content Page and add it to the template
                ContentPage page = new ContentPage();

                // Create the Blazor WebView
                BlazorWebView webView = new BlazorWebView();

                // Enable routing extensions
                webView.EnableRoutingExtensions();

                // Set the host page
                webView.HostPage = HostPage;

                // Create a RootComponent for the component
                RootComponent component = new RootComponent()
                { 
                    ComponentType = componentType,
                    Selector = Selector,
                };

                // Add the RootComponent to the RootComponents list
                webView.RootComponents.Add(component);

                // Set the page content to the WebView
                page.Content = webView;

                // Return the page
                return page;
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

            // Need a Xaml type resolver to resolve types
            if (!(serviceProvider.GetService(typeof(IXamlTypeResolver)) is IXamlTypeResolver typeResolver))
            {
                throw new ArgumentException("No IXamlTypeResolver in IServiceProvider");
            }

            // If type isn't specified, log with details about the xaml line where it's missing.
            if (string.IsNullOrEmpty(ComponentType))
            {
                throw new XamlParseException("TypeName isn't set.", serviceProvider.GetLineInfo());
            }

            // Placeholder for resolved type
            Type componentType;

            // If we can't resolve the type, log a parse exception with line information
            if (!typeResolver.TryResolve(ComponentType, out componentType))
            {
                throw new XamlParseException($"{nameof(BlazorTemplateExtension)}: Could not locate type for {ComponentType}.", serviceProvider.GetLineInfo());
            }

            // Now that we have the type, make sure it's a Blazor component
            if (!typeof(ComponentBase).IsAssignableFrom(componentType))
            {
                throw new XamlParseException($"{nameof(BlazorTemplateExtension)}: {ComponentType} is not a Blazor component.", serviceProvider.GetLineInfo());
            }

            // Create the component template
            return CreateComponentTemplate(componentType);
        }

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the Blazor component type that will be used to generate the template.
        /// </summary>
        public string ComponentType { get; set; }

        /// <summary>
        /// Gets or sets the path to the HTML file to render.
        /// <para>This is an app relative path to the file such as <c>wwwroot\index.html</c></para>
        /// </summary>
        public string HostPage { get; set; } = "wwwroot/index.html";

        /// <summary>
		/// Gets or sets the CSS selector string that specifies where in the document the component should be placed.
		/// This must be unique among the root components within the <see cref="BlazorWebView"/>.
        /// </summary>
        public string Selector { get; set; } = "#app";

        #endregion Public Properties
    }
}