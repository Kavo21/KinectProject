using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_v2_Learning
{
    public static class RecordStudentsData
    { 
        public static void WriteInToFile(String students ,String title, String vocabulary, int count)
        {
            if (students != null & title != null & vocabulary != null)
            {
                //string path = @"C:\Users\a846205123\Desktop\" + title + ".txt";
                string DeskTopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string Connect = "/";
                string path = DeskTopPath + Connect + title + ".txt";


                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    string createText = students + " : " + vocabulary + " : " + count + Environment.NewLine;
                    File.WriteAllText(path, createText);
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                string appendText = students + " : " + vocabulary + " : " + count + Environment.NewLine;

                try
                {
                    File.AppendAllText(path, appendText);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                // Open the file to read from.
                //string readText = File.ReadAllText(path);

                //Console.WriteLine(readText);
            }
        }
    }
}
