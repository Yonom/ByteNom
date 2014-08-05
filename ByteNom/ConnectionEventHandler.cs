namespace ByteNom
{
    /// <summary>
    ///     Provvides data for the <see cref="Server.ConnectionReceived" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="connection">The connection.</param>
    public delegate void ConnectionEventHandler(object sender, Connection connection);
}