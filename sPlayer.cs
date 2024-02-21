using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace GraRPG
{
    static public class sPlayer
    {

        static public cPlayer player = new cPlayer();
        static public bool ifDeleted = false;
        static public void savePlayer()
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(path, "GraRPG", "Players", sPlayer.player.name + ".txt");
            string path2 = Path.Combine(path, "GraRPG", "Players");
            Directory.CreateDirectory(path2);
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, sPlayer.player);
            stream.Close();
            //Stream stream1 = File.Open("Names.txt", FileMode.Append);
            //BinaryFormatter formatter2 = new BinaryFormatter();
            //formatter2.Serialize(stream1, sPlayer.player.name);
            //stream1.Close();
            filePath = Path.Combine(path,"GraRPG","Players", "Names.txt");
            using (StreamWriter sw1 = new StreamWriter(filePath, true))
            {
                sw1.WriteLine(player.name);
            }
        }
        static public void  saveAgainPlayer()
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(path, "GraRPG", "Players", sPlayer.player.name + ".txt");
            string path2 = Path.Combine(path, "GraRPG", "Players");
            Directory.CreateDirectory(path2);
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, sPlayer.player);
            stream.Close();
        }
        static public void loadPlayer(string name1)
        {
            //var jsonsett = new Newtonsoft.Json.JsonSerializerSettings();
            //jsonsett.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(path,"GraRPG","Players", name1 + ".txt");

            Stream stream = File.Open(filePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            sPlayer.player = (cPlayer)formatter.Deserialize(stream);
            stream.Close();

            //string character;
            //using (StreamReader sw = new StreamReader(filePath))
            //{
            //    character=sw.ReadToEnd();
            //}
            //sPlayer.player=Newtonsoft.Json.JsonConvert.DeserializeObject<cPlayer>(character,jsonsett);
        }
        static public void savePlayer1()
        {
            //var jsonsett = new Newtonsoft.Json.JsonSerializerSettings();
            //jsonsett.TypeNameHandling= Newtonsoft.Json.TypeNameHandling.All;
            //var serializedObject=Newtonsoft.Json.JsonConvert.SerializeObject(sPlayer.player,Newtonsoft.Json.Formatting.Indented,jsonsett);
            //string path = null;
            //path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //string filePath = Path.Combine(path, sPlayer.player.name+".txt");
            //using (StreamWriter sw=new StreamWriter(filePath))
            //{
            //    sw.Write(serializedObject);
            //}
            //filePath = Path.Combine(path, "Names.txt");
            //using (StreamWriter sw1=new StreamWriter(filePath,true))
            //{
            //    sw1.WriteLine(player.name);
            //}

        }
        static public void deletePlayer(string name1)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(path,"GraRPG","Players", name1 + ".txt");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                filePath = Path.Combine(path,"GraRPG","Players", "Names.txt");
                //File.Open(filePath,FileMode.Open);
                string[] names1 = new string[20];
                //for (int i = 0; i < 20; i++)
                //{
                //    names1[i] = "";
                //}
                names1 = File.ReadAllLines(filePath);
                List<string> list = new List<string>(names1);
                for (int i = 0; i < names1.Length; i++)
                {
                    if (names1[i] == name1)
                    {
                        list.RemoveAt(i);
                    }
                }
                names1 = list.ToArray();
                File.WriteAllLines(filePath, names1);
            }
        }
    }
}
