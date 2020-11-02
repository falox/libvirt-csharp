namespace libvirt
{
    ///<summary>
    /// Enumerate errors
    ///</summary>
    public enum virErrorNumber
    {
        /// <summary>
        /// No error
        /// </summary>
        VIR_ERR_OK = 0,
        /// <summary>
        /// Internal error
        /// </summary>
        VIR_ERR_INTERNAL_ERROR = 1,
        /// <summary>
        /// Memory allocation failure
        /// </summary>
        VIR_ERR_NO_MEMORY = 2,
        /// <summary>
        /// No support for this function
        /// </summary>
        VIR_ERR_NO_SUPPORT = 3,
        /// <summary>
        /// Could not resolve hostname
        /// </summary>
        VIR_ERR_UNKNOWN_HOST = 4,
        /// <summary>
        /// Can't connect to hypervisor
        /// </summary>
        VIR_ERR_NO_CONNECT = 5,
        /// <summary>
        /// Invalid connection object
        /// </summary>
        VIR_ERR_INVALID_CONN = 6,
        /// <summary>
        /// Invalid domain object
        /// </summary>
        VIR_ERR_INVALID_DOMAIN = 7,
        /// <summary>
        /// Invalid function argument
        /// </summary>
        VIR_ERR_INVALID_ARG = 8,
        /// <summary>
        ///  A command to hypervisor failed
        /// </summary>
        VIR_ERR_OPERATION_FAILED = 9,
        /// <summary>
        /// A HTTP GET command to failed
        /// </summary>
        VIR_ERR_GET_FAILED = 10,
        /// <summary>
        /// A HTTP POST command to failed
        /// </summary>
        VIR_ERR_POST_FAILED = 11,
        /// <summary>
        /// Unexpected HTTP error code
        /// </summary>
        VIR_ERR_HTTP_ERROR = 12,
        /// <summary>
        /// Failure to serialize an S-Expr
        /// </summary>
        VIR_ERR_SEXPR_SERIAL = 13,
        /// <summary>
        /// Could not open Xen hypervisor control
        /// </summary>
        VIR_ERR_NO_XEN = 14,
        /// <summary>
        /// Failure doing an hypervisor call
        /// </summary>
        VIR_ERR_XEN_CALL = 15,
        /// <summary>
        /// Unknown OS type
        /// </summary>
        VIR_ERR_OS_TYPE = 16,
        /// <summary>
        /// Missing kernel information
        /// </summary>
        VIR_ERR_NO_KERNEL = 17,
        /// <summary>
        /// Missing root device information
        /// </summary>
        VIR_ERR_NO_ROOT = 18,
        /// <summary>
        /// Missing source device information
        /// </summary>
        VIR_ERR_NO_SOURCE = 19,
        /// <summary>
        /// Missing target device information
        /// </summary>
        VIR_ERR_NO_TARGET = 20,
        /// <summary>
        /// Missing domain name information
        /// </summary>
        VIR_ERR_NO_NAME = 21,
        /// <summary>
        /// Missing domain OS information
        /// </summary>
        VIR_ERR_NO_OS = 22,
        /// <summary>
        /// Missing domain devices information
        /// </summary>
        VIR_ERR_NO_DEVICE = 23,
        /// <summary>
        /// Could not open Xen Store control
        /// </summary>
        VIR_ERR_NO_XENSTORE = 24,
        /// <summary>
        /// Too many drivers registered
        /// </summary>
        VIR_ERR_DRIVER_FULL = 25,
        /// <summary>
        /// Not supported by the drivers (DEPRECATED)
        /// </summary>
        VIR_ERR_CALL_FAILED = 26,
        /// <summary>
        /// An XML description is not well formed or broken
        /// </summary>
        VIR_ERR_XML_ERROR = 27,
        /// <summary>
        /// The domain already exist
        /// </summary>
        VIR_ERR_DOM_EXIST = 28,
        /// <summary>
        /// Operation forbidden on read-only connections
        /// </summary>
        VIR_ERR_OPERATION_DENIED = 29,
        /// <summary>
        /// Failed to open a conf file
        /// </summary>
        VIR_ERR_OPEN_FAILED = 30,
        /// <summary>
        /// Failed to read a conf file
        /// </summary>
        VIR_ERR_READ_FAILED = 31,
        /// <summary>
        /// Failed to parse a conf file
        /// </summary>
        VIR_ERR_PARSE_FAILED = 32,
        /// <summary>
        /// Failed to parse the syntax of a conf file
        /// </summary>
        VIR_ERR_CONF_SYNTAX = 33,
        /// <summary>
        /// Failed to write a conf file
        /// </summary>
        VIR_ERR_WRITE_FAILED = 34,
        /// <summary>
        /// Detail of an XML error
        /// </summary>
        VIR_ERR_XML_DETAIL = 35,
        /// <summary>
        /// Invalid network object
        /// </summary>
        VIR_ERR_INVALID_NETWORK = 36,
        /// <summary>
        /// The network already exist
        /// </summary>
        VIR_ERR_NETWORK_EXIST = 37,
        /// <summary>
        /// General system call failure
        /// </summary>
        VIR_ERR_SYSTEM_ERROR = 38,
        /// <summary>
        /// Some sort of RPC error
        /// </summary>
        VIR_ERR_RPC = 39,
        /// <summary>
        /// Error from a GNUTLS call
        /// </summary>
        VIR_ERR_GNUTLS_ERROR = 40,
        /// <summary>
        /// Failed to start network
        /// </summary>
        VIR_WAR_NO_NETWORK = 41,
        /// <summary>
        /// Domain not found or unexpectedly disappeared
        /// </summary>
        VIR_ERR_NO_DOMAIN = 42,
        /// <summary>
        /// Network not found
        /// </summary>
        VIR_ERR_NO_NETWORK = 43,
        /// <summary>
        /// Invalid MAC address
        /// </summary>
        VIR_ERR_INVALID_MAC = 44,
        /// <summary>
        /// Authentication failed
        /// </summary>
        VIR_ERR_AUTH_FAILED = 45,
        /// <summary>
        /// Invalid storage pool object
        /// </summary>
        VIR_ERR_INVALID_STORAGE_POOL = 46,
        /// <summary>
        /// Invalid storage vol object
        /// </summary>
        VIR_ERR_INVALID_STORAGE_VOL = 47,
        /// <summary>
        /// Failed to start storage
        /// </summary>
        VIR_WAR_NO_STORAGE = 48,
        /// <summary>
        /// Storage pool not found
        /// </summary>
        VIR_ERR_NO_STORAGE_POOL = 49,
        /// <summary>
        /// Storage pool not found
        /// </summary>
        VIR_ERR_NO_STORAGE_VOL = 50,
        /// <summary>
        /// Failed to start node driver
        /// </summary>
        VIR_WAR_NO_NODE = 51,
        /// <summary>
        /// Invalid node device object
        /// </summary>
        VIR_ERR_INVALID_NODE_DEVICE = 52,
        /// <summary>
        /// Node device not found
        /// </summary>
        VIR_ERR_NO_NODE_DEVICE = 53,
        /// <summary>
        /// Security model not found
        /// </summary>
        VIR_ERR_NO_SECURITY_MODEL = 54,
        /// <summary>
        /// Operation is not applicable at this time
        /// </summary>
        VIR_ERR_OPERATION_INVALID = 55,
        /// <summary>
        /// Failed to start interface driver
        /// </summary>
        VIR_WAR_NO_INTERFACE = 56,
        /// <summary>
        /// Interface driver not running
        /// </summary>
        VIR_ERR_NO_INTERFACE = 57,
        /// <summary>
        /// Invalid interface object
        /// </summary>
        VIR_ERR_INVALID_INTERFACE = 58,
        /// <summary>
        /// More than one matching interface found
        /// </summary>
        VIR_ERR_MULTIPLE_INTERFACES = 59,
        /// <summary>
        /// Failed to start secret storage
        /// </summary>
        VIR_WAR_NO_SECRET = 60,
        /// <summary>
        /// Invalid secret
        /// </summary>
        VIR_ERR_INVALID_SECRET = 61,
        /// <summary>
        /// Secret not found
        /// </summary>
        VIR_ERR_NO_SECRET = 62,
        /// <summary>
        /// Unsupported configuration construct
        /// </summary>
        VIR_ERR_CONFIG_UNSUPPORTED = 63,
        /// <summary>
        /// Timeout occurred during operation
        /// </summary>
        VIR_ERR_OPERATION_TIMEOUT = 64,
        /// <summary>
        /// A migration worked, but making the VM persist on the dest host failed
        /// </summary>
        VIR_ERR_MIGRATE_PERSIST_FAILED = 65,
    }
}