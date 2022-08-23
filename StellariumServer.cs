using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace telescope
{
    public delegate void ReferencesCallback(StellariumServer server, TcpListener listener);
    public delegate byte[] PositionCallback(byte[] data);

    public class StellariumServer
    {
        readonly TcpListener listener = null;
        public bool ListenerAllowedToRun = true;

        public StellariumServer(string ip, int port, ReferencesCallback referencesCallback, PositionCallback positionCallback)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            Debug.WriteLine("1");
            listener = new TcpListener(localAddr, port);
            Debug.WriteLine("2");
            // this callback allows the caller to stop all threads at any time
            referencesCallback(this, listener);
            Debug.WriteLine("3");
            listener.Start();
            Debug.WriteLine("4");
            try
            {
                Debug.WriteLine("Waiting for a connection...");
                TcpClient client = listener.AcceptTcpClient();

                Debug.WriteLine("Connected!");

                NetworkStream stream = client.GetStream();
                Debug.WriteLine("5");
                byte[] bytes = new byte[256];
                int i;
                try
                {
                    while (ListenerAllowedToRun)
                    {
                        i = stream.Read(bytes, 0, bytes.Length);
                        Debug.WriteLine($"{i}");
                        if (i > 0)
                        {
                            string content = Encoding.ASCII.GetString(bytes, 0, i);
                            Debug.WriteLine("Rx:\n{0}", content);

                            byte[] result = positionCallback(bytes);

                            Send(stream, result);
                        }
                        else
                        {
                            // this callback indicates streaming error
                            referencesCallback(null, null);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception: {0}", e.ToString());
                }
                finally
                {
                    Debug.WriteLine("6");
                    client.Close();
                    Debug.WriteLine("7");
                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
                listener.Stop();
            }
            Debug.WriteLine("8");
            listener.Stop();
            Debug.WriteLine("9");
        }

        private void Send(NetworkStream stream, string str)
        {
            byte[] reply = Encoding.ASCII.GetBytes(str);
            stream.Write(reply, 0, reply.Length);
            Debug.WriteLine("Tx:\n{0}", str);
        }

        private void Send(NetworkStream stream, byte[] reply)
        {
            stream.Write(reply, 0, reply.Length);
            //Debug.WriteLine("Tx:\n{0}", str);
        }
    }
}
