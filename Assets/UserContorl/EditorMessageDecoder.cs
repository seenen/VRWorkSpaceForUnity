using System;
using System.Text;
using ProtoBuf;
using System.IO;

namespace VRClient
{
    class EditorMessageDecoder
    {
        public static object DecodeMessage(string text)
        {
            object obj = JsonFx.Json.JsonReader.Deserialize(text);
            return obj;
        }


        public static T DecodeMessage<T>(string text)
        {
            T t = JsonFx.Json.JsonReader.Deserialize<T>(text);

            return t;
        }

        public static string EncodeMessage(object msg)
        {
            string str = JsonFx.Json.JsonWriter.Serialize(msg);

            return str;
        }

        #region XML的序列号和反序列化
        public static string EncodeMessageByProtobuf<T>(T msg)
        {
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, msg); //ms流对象，instance转换成byte数组会存储在ms里面。《序列化》  
                bytes = new byte[ms.Position]; //为bytes实例化一个长度（传递过来的类型转换成（byte）数组之后的长度）ms.position(ms流对象的长度）  
                var fullBytes = ms.GetBuffer(); //获取储存在内存流里面的字节数据  
                Array.Copy(fullBytes, bytes, bytes.Length); //将保存在 fullBytes内存流里的数据拷贝到bytes里。  

                bytes = CompressionHelper.Compress(bytes);

                string json = Convert.ToBase64String(bytes);

                ms.Close();
                ms.Dispose();

                return json;
            }
        }

        public static T DecodeMessageByProtobuf<T>(string text)
        {
            byte[] bytes = Convert.FromBase64String(text);

            bytes = CompressionHelper.DeCompress(bytes);

            using (var ms = new MemoryStream(bytes)) //(声明一个内存流对象)  
            {
                return Serializer.Deserialize<T>(ms); //《反序列化》  
            }
        }
        #endregion XML的序列号和反序列化

    }
}
