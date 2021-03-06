﻿/* Copyright 2015 Per Rovegard
   Licensed under the MIT license. See LICENSE file in the root of the repo for the full license. */
namespace Nancy.AspNet.WebSockets
{
    /// <summary>
    /// IWebSocketClient is an interface that represents a WebSocket client, i.e. typically the
    /// user's web browser.
    /// </summary>
    public interface IWebSocketClient
    {
        /// <summary>
        /// Send a string message to the client.
        /// </summary>
        /// <param name="message">the message to send</param>
        void Send(string message);

        /// <summary>
        /// Send binary data to the client.
        /// </summary>
        /// <param name="data">the data to send</param>
        void Send(byte[] data);

        /// <summary>
        /// Close the connection to the client.
        /// </summary>
        void Close();
    }
}
