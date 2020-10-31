namespace libvirt
{
    /// <summary>
    /// Enumerate the error levels
    /// </summary>
    public enum ErrorLevel
    {
        /// <summary>
        /// No error
        /// </summary>
        VIR_ERR_NONE = 0,
        /// <summary>
        /// A simple warning.
        /// </summary>
        VIR_ERR_WARNING = 1,
        /// <summary>
        /// An error.
        /// </summary>
        VIR_ERR_ERROR = 2,
    }
}