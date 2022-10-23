namespace SolerSoft.Maui;

/// <summary>
/// Thrown when an unexpected branch occurs, usually due to an unexpected value.
/// </summary>
public class UnexpectedBranchException : ApplicationException
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new <see cref="UnexpectedBranchException" />.
    /// </summary>
    /// <param name="value">
    /// The unknown value that caused the branch.
    /// </param>
    public UnexpectedBranchException(object value) : base($"An unexpected branch was caused by the unknown value '{value}'.") { }

    #endregion Public Constructors
}