using System;

namespace CslaContrib.Net.Caliburn.Micro
{
  /// <summary>
  /// Contains information about the error that
  /// has occurred.
  /// </summary>
  public class ErrorEventArgs : EventArgs
  {
    /// <summary>
    /// Gets the Exception object for the error
    /// that occurred.
    /// </summary>
    public Exception Error { get; internal set; }
  }
}
