using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiToolkit.Markup
{
    /// <summary>
    /// Extension methods and other helpers for managing markup.
    /// </summary>
    static public class MarkupHelpers
    {
        /// <summary>
        /// Gets information about the current line while parsing Xmal.
        /// </summary>
        /// <param name="serviceProvider">
        /// The <see cref="IServiceProvider"/> that will be used to obtain line information.
        /// </param>
        /// <returns>
        /// The <see cref="System.Xml.IXmlLineInfo"/> containing line information.
        /// </returns>
        static public System.Xml.IXmlLineInfo GetLineInfo(this IServiceProvider serviceProvider)
        {
            return (serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lip) ? lip.XmlLineInfo : new XmlLineInfo();
        }

    }
}
