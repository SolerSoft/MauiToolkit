using SolerSoft.Maui.Mvvm;

namespace SolerSoft.Maui.Navigation;

/// <summary>
/// A helper class for managing routes.
/// </summary>
public static class Router
{
    #region Private Fields

    private static Dictionary<Type, string> pluralNames = new();
    private static Dictionary<Type, string> singularNames = new();

    #endregion Private Fields

    #region Public Methods

    /// <summary>
    /// Gets the route for a detail view of the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to get the detail route for.
    /// </typeparam>
    /// <returns>
    /// The detail route.
    /// </returns>
    public static string GetDetailRoute<TEntity>()
    {
        return '/' + GetSingular<TEntity>();
    }

    /// <summary>
    /// Gets the route for a list view of the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to get the list route for.
    /// </typeparam>
    /// <returns>
    /// The list route.
    /// </returns>
    public static string GetListRoute<TEntity>()
    {
        return '/' + GetPlural<TEntity>();
    }

    /// <summary>
    /// Gets the plural name for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type to provide the plural name of.
    /// </typeparam>
    /// <returns>
    /// The plural name.
    /// </returns>
    public static string GetPlural<T>()
    {
        // Get type from generic
        Type t = typeof(T);

        // If not in lookup, add it
        if (!pluralNames.ContainsKey(t))
        {
            pluralNames[t] = GetSingular<T>() + 's';
        }

        // Return lookup
        return pluralNames[t];
    }

    /// <summary>
    /// Gets the singular name for the specified type.
    /// </summary> 
    /// <typeparam name="T"> 
    /// The type to provide the singular name of.
    /// </typeparam>
    /// <returns>
    /// The plural name.
    /// </returns>
    public static string GetSingular<T>()
    {
        // Get type from generic
        Type t = typeof(T);

        // If not in lookup, add it
        if (!singularNames.ContainsKey(t))
        {
            singularNames[t] = t.Name.ToLowerInvariant();
        }

        // Return lookup
        return singularNames[t];
    }

    /// <summary>
    /// Registers a detail route for the specified entity to the specified page.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to map.
    /// </typeparam>
    /// <typeparam name="TPage">
    /// The type of page that will display the details.
    /// </typeparam>
    public static void RegisterDetail<TEntity, TPage>() where TPage : ViewPage
    {
        Routing.RegisterRoute(GetDetailRoute<TEntity>(), typeof(TPage));
    }

    /// <summary>
    /// Registers a list route for the specified entity to the specified page.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to map.
    /// </typeparam>
    /// <typeparam name="TPage">
    /// The type of page that will display the list.
    /// </typeparam>
    public static void RegisterList<TEntity, TPage>() where TPage : ViewPage
    {
        Routing.RegisterRoute(GetListRoute<TEntity>(), typeof(TPage));
    }

    /// <summary>
    /// Sets the plural name for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type to provide the plural name of.
    /// </typeparam>
    /// <param name="name">
    /// The plural name.
    /// </param>
    public static void SetPlural<T>(string name)
    {
        pluralNames[typeof(T)] = name;
    }

    /// <summary>
    /// Sets the singular name for the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type to provide the singular name of.
    /// </typeparam>
    /// <param name="name">
    /// The plural name.
    /// </param>
    public static void SetSingular<T>(string name)
    {
        singularNames[typeof(T)] = name;
    }

    #endregion Public Methods
}