namespace ByteNom
{
    /// <summary>
    /// Represents a server.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        ///     Occurs when a new connection is received.
        /// </summary>
         event ConnectionEventHandler ConnectionReceived;
    }
}
