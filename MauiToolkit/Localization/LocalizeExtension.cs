using Microsoft.Extensions.Localization;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolerSoft.Maui.Localization;

[ContentProperty(nameof(Key))]
public class LocalizeExtension : IMarkupExtension
{
    IStringLocalizer _localizer;

    public string Key { get; set; } = string.Empty;

    public LocalizeExtension()
    {
        _localizer = Ioc.Default.GetService<IStringLocalizer>();
    }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        string localizedText = (_localizer != null ? _localizer[Key] : null);
        return localizedText;
    }
}
