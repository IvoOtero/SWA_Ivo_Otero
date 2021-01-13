using System;
using System.Collections.Generic;
using System.IO;

namespace DataHandling_and_Logging
{
    //
    // Handles the log-data from the chat application 
    // and manages the saving, deleting or moving from files 
    //
    public class DataHandler
    {
        private const string folder = @"Logs/";
        private const string fileExtension = ".txt"; 
        
        public String[] Load(string fileName)
        {
            String[] lines = File.ReadAllLines(folder + fileName);
            return lines;
        }
        public void Save(List<string> data)
        {
            File.WriteAllLines(folder + DateTime.Now.ToShortDateString() + "__" + DateTime.Now.ToFileTimeUtc() + fileExtension, data.ToArray());
        }

        public string[] ListFilesFromFolder()
        {
            //if the directory for the log-files does not exist, we need to create a new one 
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            //create class to move and modify through a folder 
            DirectoryInfo info = new DirectoryInfo(folder);

            //get all files in the folder with the extension ".txt" and save them in an array 
            var result = info.GetFiles("*" + fileExtension);
            string[] tmp = new string[result.Length];
            int i = 0;

            //save each filename in the folder into the list to be returned 
            foreach (var file in result)
            {
                tmp[i] = file.Name;
                i++;
            }

            return tmp;
        }

        public void Delete(string name)
        {
            // if the selected file exist in the folder -> delete file
            if (File.Exists(folder + name))
            {
                File.Delete(folder + name);
            }
        }



    }
}
