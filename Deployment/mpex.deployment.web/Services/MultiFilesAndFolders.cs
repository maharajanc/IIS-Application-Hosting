using mpex.deployment.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mpex.deployment.web.Services
{
    public class MultiFilesAndFolders
    {
        private FileTransfer FileTransfer = new FileTransfer();

        private ApplicationDbContext dbo = null;

        public ApplicationDbContext db
        {
            set
            {
                dbo = value;
            }
            get
            {
                return dbo;
            }
        }

        public bool Process(MultiFileDeployment model, string Server)
        {
            try
            {

                db = new ApplicationDbContext(Server);

                //delete source file
                List<int> intFile = GetId(model.fkFileIdList); 
                List<int> intFolder = GetId(model.fkFolderIdList); 
                List<int> intClient = GetId(model.fkClientIdList); 

                List<string> folder = db.Folders.Where(i => intFolder.Contains(i.Id)).Select(s => s.Name).ToList();
                List<string> file = db.Files.Where(i => intFile.Contains(i.Id)).Select(s => s.Name).ToList();

                var clients = db.Clients;
                List<string> client = clients.Where(i => intClient.Contains(i.Id)).Select(s => s.IISFileLocation).ToList();
                List<string> Apools = clients.Where(i => intClient.Contains(i.Id)).Select(s => s.IISPoolName).ToList();


                //stop application pool
                AppPool.Instance.StopAllAppplicationPools(Apools);
                FileTransfer.DeleteFiles(new DirectoryInfo(model.SourceFileLocation), file, folder, true);

                FileTransfer.TargetLocationDelete(client, file, folder, false);

                FileTransfer.CopySourceToDestination(model.SourceFileLocation, client);
                //Start application pool
                AppPool.Instance.StartAllAppplicationPools(Apools);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<int> GetId(string s)
        {
            List<int> ids = new List<int>();

            if (!string.IsNullOrEmpty(s))
            {
                ids = s.Split(new char[] { ',' }).ToList<string>().ConvertAll(int.Parse);
            }
            return ids;
        }
    }
}