namespace ByteNom
{
    /// <summary>
    ///     Provides data for the <see cref="Server.ConnectionReceived" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="connection">The connection.</param>
    public delegate void TcpConnectionEventHandler(object sender, TcpConnection connection);
}
