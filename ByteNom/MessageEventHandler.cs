namespace ByteNom
{
    /// <summary>
    /// Provides data for the <see cref="Connection.Message"/> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="message">The received message.</param>
    public delegate void MessageEventHandler(object sender, Message message);
}