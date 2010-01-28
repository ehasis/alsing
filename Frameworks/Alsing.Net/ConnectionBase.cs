using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Net.Messages;
using Alsing.Messaging;
using System.Net.Sockets;
using System.Threading;
using System.IO;
namespace Alsing.Net
{
    public abstract class ConnectionBase
    {
        private readonly System.Net.Sockets.TcpClient client;
        private StreamWriter writer;
        private readonly IMessageSink messageSink;
        private string host;
        private int port;
        public readonly Guid Guid = Guid.NewGuid();

        public string Host
        {
            get
            {
                return host;
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
        }

        protected ConnectionBase(IMessageSink messageSink)
        {
            this.messageSink = messageSink;
            this.client = new TcpClient();
        }

        public void Connect(string host, int port)
        {
            if (client.Connected == true)
                throw new Exception("Connection is already open");

            this.host = host;
            this.port = port;
            
            client.BeginConnect(host, port, new AsyncCallback(ConnectionCallback), client);
        }

        private void ConnectionCallback(IAsyncResult asyncresult)
        {
            try
            {
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;

                if (tcpclient != null && tcpclient.Client != null)
                {
                    OnConnected();
                    tcpclient.EndConnect(asyncresult);

                    var stream = tcpclient.GetStream();
                    writer = new StreamWriter(stream);
                    ReadLoop(stream);
                }
                else
                {
                    OnConnectionFailed(null);
                }
            }
            catch (Exception ex)
            {
                OnConnectionFailed(ex);
            }
        }

        private void ReadLoop(NetworkStream stream)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                while (true)
                {
                    int i = stream.ReadByte(); // Inefficient but more reliable 

                    if (i == -1) break;  // Other side has closed socket 

                    char c = (char)i;   // Accrue 'c' to save page data 
                    
                    if (c == (char)13)
                    {
                        DataReceivedMessage dataReceivedMessage = new DataReceivedMessage
                        {
                            Connection = this,
                            Data = sb.ToString(),
                        };
                        messageSink.Send(dataReceivedMessage);
                        sb.Clear();
                    }
                    else if (c == (char)10)
                    {
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            catch
            {
            }

            Close();
        }

        private void OnConnected()
        {            
            ConnectedMessage connectedMessage = new ConnectedMessage
            {
                Connection = this,
                Host = host,
                Port = port,
            };
            messageSink.Send(connectedMessage);
        }

        private void OnConnectionFailed(Exception ex)
        {
            ConnectionFailedMessage failedMessage = new ConnectionFailedMessage
            {
                Exception = ex,
                Connection = this,
                Host = host,
                Port = port,
            };
            messageSink.Send(failedMessage);
        }

        public void Close()
        {
            if (client.Connected == false)
                return;

            client.Close();

            ConnectionClosedMessage connectionClosed = new ConnectionClosedMessage
            {
                Connection = this,
                Host = host,
                Port = port,
            };
            messageSink.Send(connectionClosed);
        }

        public void WriteLine(string data)
        {
            writer.Write(data + "\r\n");
            writer.Flush();

            var message = new DataSentMessage
            {
                Connection = this,
                Data = data,
            };

            this.messageSink.Send(message);
        }
    }
}
