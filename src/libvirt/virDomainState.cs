namespace libvirt
{
    /// <summary>
    /// States of a domain
    /// </summary>
    public enum virDomainState
    {
        /// <summary>
        /// No state.
        /// </summary>
        VIR_DOMAIN_NOSTATE = 0,
        /// <summary>
        /// The domain is running.
        /// </summary>
        VIR_DOMAIN_RUNNING = 1,
        /// <summary>
        /// The domain is blocked on resource.
        /// </summary>
        VIR_DOMAIN_BLOCKED = 2,
        /// <summary>
        /// The domain is paused by user.
        /// </summary>
        VIR_DOMAIN_PAUSED = 3,
        /// <summary>
        /// The domain is being shut down.
        /// </summary>
        VIR_DOMAIN_SHUTDOWN = 4,
        /// <summary>
        /// The domain is shut off.
        /// </summary>
        VIR_DOMAIN_SHUTOFF = 5,
        /// <summary>
        /// The domain is crashed.
        /// </summary>
        VIR_DOMAIN_CRASHED = 6
    }
}