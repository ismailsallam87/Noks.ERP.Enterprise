using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace Device.Enrollment.Migration
{
    public class Migration
    {
        private string FilePath { get; set; }
        private string Extract_Destination { get; set;}
        private List<Enrollment> Enrollments { get; set; }
        private List<string> Files { get; set; }
        private List<int> Enrolmment_Numbers { get; set; }
        /// <summary>
        /// Structor should take file path and extract destination.
        /// </summary>
        /// <param name="_filePath"></param>
        /// <param name="_Extract_Destination"></param>
        public Migration(string _filePath,string _Extract_Destination)
        {
            this.FilePath = _filePath;
            this.Extract_Destination = _Extract_Destination;
        }
        /// <summary>
        /// Extract Zip Files, based on Filepath to Extract Destination
        /// </summary>
        /// <returns>Try and Catch, True if successed or false if not.</returns>
        public bool Extract()
        {
            bool result=true;
            try
            {
                ZipFile.ExtractToDirectory(FilePath, Extract_Destination);
            }
            catch
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// Check the File if is Zip or Other, But need to use FilePath
        /// </summary>
        /// <returns>True if the file path is Zip or false if not.</returns>
        public bool IsZIP()
        {   
            bool result= true;

            try
            {
                using (var zipFile = ZipFile.OpenRead(FilePath))
                {
                    var entries = zipFile.Entries;
                    result= true ;
                }
            }
            catch (InvalidDataException)
            {
                 result=false;
            }

            return result;
        }
        /// <summary>
        /// Load All XML Files from Extract Destination
        /// </summary>
        /// <returns>List of Files</returns>
        public List<string> Get_Files()
        {
            this.Files = new List<string>();
            foreach (string file in Directory.EnumerateFiles(Extract_Destination, "*.xml"))
            {
                this.Files.Add((file));
            }
            return Files;
        }        
        /// <summary>
        /// Return a list of enrollments from the files
        /// </summary>
        /// <returns>Enrollments</returns>
        public List<Enrollment> Extract_Enrollments()
        {
            this.Enrollments = new List<Enrollment>();
            foreach(var file in Files)
            {
                string filename = file.Substring(file.LastIndexOf(@"\")).Replace(@"\","");
                filename = filename.Replace("user_", "");
                filename = filename.Replace(".xml", "");
                int enrollNo = Convert.ToInt32(filename);
                XmlDataDocument xmldoc = new XmlDataDocument();
                XmlNodeList xmlnode;
                int i = 0;
                string str = null;
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                xmlnode = xmldoc.GetElementsByTagName("name");
                string name = "";
                for (i = 0; i <= xmlnode.Count - 1; i++)
                {
                    try
                    {
                        name = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                    }
                    catch(Exception exc)
                    {

                    }
                   
                }

                Enrollments.Add(new Enrollment { Enrollment_No = enrollNo,Name= name,XML_File= file});
            }
            return Enrollments;
        }
        #region Utilities

        #endregion
    }

    public class Enrollment
    {
        public int Enrollment_No { get; set; }
        public string Name { get; set; }
        public string XML_File { get; set; }
    }
}
