namespace SolerSoft.Maui.Navigation;

static public class NavigationExtensions
{
    /// <summary>
    /// Converts a <see cref="FormattableString"/> to a named value set of parameters.
    /// </summary>
    /// <param name="param">
    /// The <see cref="FormattableString"/> to convert.
    /// </param>
    /// <returns>
    /// The converted parameters.
    /// </returns>
    static private Dictionary<string, object> ToParameters(this FormattableString param)
    {
        // Placeholder for results
        Dictionary<string, object> parameters = new();

        // Split first on comas to get named values
        string[] tuples = param.Format.Split(',');

        // Process and add each comma
        foreach (string tuple in tuples)
        {
            // Get name and value
            string[] namedValue = tuple.Split('=');

            // Get name
            string name = namedValue[0];
            for (int i = 0; i < param.ArgumentCount; i++)
            {
                name = name.Replace($"{{{i}}}", param.GetArgument(i).ToString());
            }
            name = name.Trim();

            // Get value index
            int index = int.Parse(namedValue[1].Trim(' ', '{', '}'));

            // Add
            parameters[name] = param.GetArgument(index);
        }

        // Done!
        return parameters;
    }

    /// <summary>
    /// Automatically creates a navigation state dictionary and completes navigation.
    /// </summary>
    /// <param name="shell">
    /// The <see cref="Shell"/> where navigation will occur.
    /// </param>
    /// <param name="state">
    /// The <see cref="ShellNavigationState"/> that indicates where to navigate.
    /// </param>
    /// <param name="param">
    /// A <see cref="FormattableString"/> that will be converted to a state dictionary.
    /// </param>
    /// <returns>
    /// The navigation <see cref="Task"/>.
    /// </returns>
    static public Task GoToAsync(this Shell shell, ShellNavigationState state, FormattableString param)
    {
        // Convert and navigate with parameters
        return shell.GoToAsync(state, param.ToParameters());
    }

    /// <summary>
    /// Automatically creates a navigation state dictionary and completes navigation to the details page.
    /// </summary>
    /// <param name="shell">
    /// The <see cref="Shell"/> where navigation will occur.
    /// </param>
    /// <typeparam name="TEntity">
    /// The type of entity to navigate to the detail page for.
    /// </typeparam>
    /// <param name="param">
    /// A <see cref="FormattableString"/> that will be converted to a state dictionary.
    /// </param>
    /// <returns>
    /// The navigation <see cref="Task"/>.
    /// </returns>
    static public Task GoToDetailAsync<TEntity>(this Shell shell, FormattableString param)
    {
        return GoToAsync(shell, Router.GetDetailRoute<TEntity>(), param);
    }

    /// <summary>
    /// Completes navigation to the details page for the specific entity.
    /// </summary>
    /// <param name="shell">
    /// The <see cref="Shell"/> where navigation will occur.
    /// </param>
    /// <typeparam name="TEntity">
    /// The type of entity to navigate to the detail page for.
    /// </typeparam>
    /// <param name="entity">
    /// The entity instance to pass to the page.
    /// </param>
    /// <returns>
    /// The navigation <see cref="Task"/>.
    /// </returns>
    static public Task GoToDetailAsync<TEntity>(this Shell shell, TEntity entity)
    {
        return GoToAsync(shell, Router.GetDetailRoute<TEntity>(), $"{Router.GetSingular<TEntity>()}={entity}");
    }

    /// <summary>
    /// Automatically creates a navigation state dictionary and completes navigation to the list page.
    /// </summary>
    /// <param name="shell">
    /// The <see cref="Shell"/> where navigation will occur.
    /// </param>
    /// <typeparam name="TEntity">
    /// The type of entity to navigate to the list page for.
    /// </typeparam>
    /// <param name="param">
    /// A <see cref="FormattableString"/> that will be converted to a state dictionary.
    /// </param>
    /// <returns>
    /// The navigation <see cref="Task"/>.
    /// </returns>
    static public Task GoToListAsync<TEntity>(this Shell shell, FormattableString param)
    {
        return GoToAsync(shell, Router.GetListRoute<TEntity>(), param);
    }

    /// <summary>
    /// Completes navigation to the list page for the list of entities.
    /// </summary>
    /// <param name="shell">
    /// The <see cref="Shell"/> where navigation will occur.
    /// </param>
    /// <typeparam name="TEntity">
    /// The type of entity to navigate to the list page for.
    /// </typeparam>
    /// <param name="entities">
    /// The list of entities to pass to the page.
    /// </param>
    /// <returns>
    /// The navigation <see cref="Task"/>.
    /// </returns>
    static public Task GoToListAsync<TEntity>(this Shell shell, IEnumerable<TEntity> entities)
    {
        return GoToAsync(shell, Router.GetListRoute<TEntity>(), $"{Router.GetPlural<TEntity>()}={entities}");
    }
}
