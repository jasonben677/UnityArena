using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using TestDll;
using System;

namespace Common
{
    public class BookRecordSerializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (typeName == "Player" || typeName == "Message03")
            {
                return null;
            }
            else
            {
                throw new ArgumentException("Unexpected type", nameof(typeName));
            }
        }
    }


    public class SerializationManager
    {
        BinaryFormatter s_bf;

        /// <summary>
        /// 反序列化
        /// </summary>
        public Message03 DeserializeClass(TcpClient _client)
        {
            s_bf = new BinaryFormatter();
            //s_bf.Binder = new BookRecordSerializationBinder();
            NetworkStream clientStream = _client.GetStream();
            byte[] buffer = new byte[_client.Available];
            clientStream.Read(buffer,0,buffer.Length);

            MemoryStream ms = new MemoryStream(buffer);
            Message03 result = (Message03)s_bf.Deserialize(ms);

            return result;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void SerializeClass(TcpClient _client, Message03 _sendmsg)
        {
            s_bf = new BinaryFormatter();
            //s_bf.Binder = new BookRecordSerializationBinder();
            MemoryStream ms = new MemoryStream();
            s_bf.Serialize(ms,_sendmsg);
            byte[] buffer = ms.ToArray();
            _client.GetStream().Write(buffer, 0, buffer.Length);
        }

    }
}
