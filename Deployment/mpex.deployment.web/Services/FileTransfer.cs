using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mpex.deployment.web.Services
{
    public sealed class FileTransfer
    {
        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }


            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }

        }

        public void CopySingleFile() { }

        public void CopySourceToDestination(string sourceDirectory, List<string> targetDirectory)
        {
            foreach (string target in targetDirectory)
            {
                CopyAll(new DirectoryInfo(sourceDirectory), new DirectoryInfo(target));
            }
        }

        public void DeleteFiles(DirectoryInfo target, List<string> filename, List<string> folderName, bool source)
        {
            if (Directory.Exists(target.FullName))
            {

                // Delete each file into it's  directory.
                foreach (FileInfo fi in target.GetFiles())
                {
                    if (filename.Any(l => l == fi.Name.ToString()) == source)
                    {
                        System.IO.File.Delete(fi.FullName);
                    }
                }

                // Delete each subdirectory
                foreach (DirectoryInfo diSourceSubDir in target.GetDirectories())
                {
                    if (folderName.Any(l => l == diSourceSubDir.Name) == source)
                    {
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(diSourceSubDir.FullName);
                        dir.Delete(true);
                        //System.IO.File.Delete(diSourceSubDir.FullName);
                    }
                }

            }

        }

        public void TargetLocationDelete(List<string> targetDirectory, List<string> fileType, List<string> folderName, bool IsTarget)
        {
            foreach (var target in targetDirectory)
            {
                DeleteFiles(new DirectoryInfo(target), fileType, folderName, IsTarget);
            }
        }

        public void SingleFielDelete(List<string> targetDirectory)
        {
            foreach (var target in targetDirectory)
            {
                if (File.Exists(target))
                {
                    System.IO.File.Delete(target);
                }
            }
        }

        public void SingleFielCopy(string sourceDirectory, List<string> targetDirectory)
        {
            foreach (string target in targetDirectory)
            {
                File.Copy(sourceDirectory, target);
            }
        }

        public bool FileLocationExists(string l)
        {
            if (Directory.Exists(l))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FolderCheck()
        {
            string pathString = System.Web.Configuration.WebConfigurationManager.AppSettings["MSourceFolderName"].ToString();

            if (!new DirectoryInfo(pathString).Exists)
            {
                System.IO.Directory.CreateDirectory(pathString);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool FolderFileCheck()
        {
            string pathString = System.Web.Configuration.WebConfigurationManager.AppSettings["MSourceFolderName"].ToString();

            if (!Directory.EnumerateFiles(pathString).Any())
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public static FileTransfer objFileTransfer
        {
            get { return new FileTransfer(); }
        }
    }
}