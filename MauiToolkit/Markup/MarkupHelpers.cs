using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MauiToolkit.Markup
{
    /// <summary>
    /// Extension methods and other helpers for managing markup.
    /// </summary>
    public static class MarkupHelpers
    {
        #region Private Methods

        /// <summary>
        /// Handles routing within the UrlLoading event.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The UrlLoading event data.
        /// </param>
        private static async void RouteUrlLoadingHandler(object? sender, UrlLoadingEventArgs? e)
        {
            // Make sure we got args
            if (e == null) { return; }

            // Get original string
            string original = e.Url.OriginalString;

            // Is it a shell link?
            if (original.StartsWith("shell:"))
            {
                // Get relative path
                string path = original.Substring(6);

                // Cancel navigation
                e.UrlLoadingStrategy = UrlLoadingStrategy.CancelLoad;

                // Use shell navigation
                await Shell.Current.GoToAsync(path);
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Enables routing extensions for the <see cref="BlazorWebView"/>.
        /// </summary>
        /// <param name="webView">
        /// The <see cref="BlazorWebView"/> to enable routing on.
        /// </param>
        public static void EnableRoutingExtensions(this BlazorWebView webView)
        {
            // Validate
            if (webView == null) { throw new ArgumentNullException(nameof(webView)); }

            // Subscribe to the handler but only once
            webView.UrlLoading -= RouteUrlLoadingHandler;
            webView.UrlLoading += RouteUrlLoadingHandler;
        }

        /// <summary>
        /// Gets information about the current line while parsing Xmal.
        /// </summary>
        /// <param name="serviceProvider">
        /// The <see cref="IServiceProvider" /> that will be used to obtain line information.
        /// </param>
        /// <returns>
        /// The <see cref="System.Xml.IXmlLineInfo" /> containing line information.
        /// </returns>
        public static System.Xml.IXmlLineInfo GetLineInfo(this IServiceProvider serviceProvider)
        {
            return (serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lip) ? lip.XmlLineInfo : new XmlLineInfo();
        }

        #endregion Public Methods
    }
}