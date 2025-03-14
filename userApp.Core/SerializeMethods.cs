using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using userApp.Domain.Models;


namespace userApp.Core
{
    public class SerializeMethods
    {
        XmlSerializer seriaXml = new XmlSerializer(typeof(List<DataUserModel>));
        //BinaryFormatter formatter = new BinaryFormatter();
        public void SerializeXml(List<DataUserModel> _users)
        {
            using (FileStream fs = new FileStream("ToXml.xml", FileMode.OpenOrCreate))
            {
                seriaXml.Serialize(fs, _users);
            }
        }

        public void DeserializeXml()
        {
            using (FileStream fs = new FileStream("ToXml.xml", FileMode.OpenOrCreate))
            {
                List<DataUserModel>? newpeople = seriaXml.Deserialize(fs) as List<DataUserModel>;

                if (newpeople != null)
                {
                    using (StreamWriter writer = new StreamWriter("XmlToTxt.txt"))
                    {
                        foreach (DataUserModel users in newpeople)
                        {
                            writer.WriteLine(users.UserName + "," + users.Password + "," + users.Email);
                        }
                    }
                }
            }
        }

        //public void SerializeBinary(List<DataUserModel> _users)
        //{
        //    using (FileStream fs = new FileStream("ToBinary.dat", FileMode.OpenOrCreate))
        //    {
        //        formatter.Serialize(fs, _users);
        //    }
        //}
        //public void DeserializeBinary()
        //{
        //    using (FileStream fs = new FileStream("ToBinary.dat", FileMode.OpenOrCreate))
        //    {
        //        List<DataUserModel> newpeople = (List<DataUserModel>)formatter.Deserialize(fs);

        //        if (newpeople != null)
        //        {
        //            using (StreamWriter writer = new StreamWriter("BinaryToTxt.txt"))
        //            {
        //                foreach (DataUserModel users in newpeople)
        //                {
        //                    writer.WriteLine(users.UserName + "," + users.Password + "," + users.Email);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
