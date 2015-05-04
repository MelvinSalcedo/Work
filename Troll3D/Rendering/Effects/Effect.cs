using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Troll3D.Effects
{

    /// <summary>L'objectif de la classe Effect est de réunir les différents shaders nécessaire à l'implémentation
    /// d'un effet graphique souhaité
    /// </summary>
    
    [Serializable()]
    public class Effect : ISerializable{

        public static Effect Load(string file){
            Stream stream   = File.Open(file, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            Effect effect = (Effect)bformatter.Deserialize(stream);
            stream.Close();
            return effect;
        }
            
        /// <summary> Deserialization Constructor </summary>
        public Effect(SerializationInfo info, StreamingContext context){
            m_name      = (string)info.GetValue("m_name", typeof(string));
            m_surname   = (string)info.GetValue("m_surname",typeof(string)); 
        }

        public Effect(string name, string surname){
            m_name      = name;
            m_surname   = surname; 
        }

        /// <summary> Serialise et sauvegarde les données de la classe dans un fichier binaire</summary>
        /// <param name="filepath"></param>
        public void Save(string filepath){
            Stream stream = File.Open(filepath, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, this);
            stream.Close();
        }

        /// <summary>
        /// Méthode de sérialisation
        /// </summary>
        public void GetObjectData(SerializationInfo info, StreamingContext context){
            info.AddValue("m_name", m_name);
            info.AddValue("m_surname", m_surname);
        }

        public string m_name;
        public string m_surname;
    }
}
