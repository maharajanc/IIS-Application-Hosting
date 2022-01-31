using mpex.deployment.web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace mpex.deployment.web.Services
{
    public class ScriptUpdate
    {
        public bool UpdateDatabase(string script, List<string> ClientDB, int scriptId, string Servername, bool isFile)
        {
            try
            {
                List<String> scripts = SplitScript(script);
                foreach (string DB in ClientDB)
                {
                    using (SqlConnection con = new SqlConnection(DB))
                    {
                        con.Open();

                        foreach (string sc in scripts)
                        {
                            try
                            {
                                SqlCommand cmd = new SqlCommand(sc, con);
                                int error = cmd.ExecuteNonQuery();
                            }
                            catch (SqlException EX)
                            {

                                if (isFile)
                                {
                                    CreatErrorLogOnScriptFile(EX, DB, scriptId, Servername);
                                }
                                else
                                {
                                    CreatErrorLogOnScriptText(EX, DB, scriptId, Servername);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateDatabase(MemoryStream script, List<string> ClientDB, int scriptId, string Servername, bool isFile)
        {
            try
            {
                List<String> scripts = SplitScript(script);
                foreach (string DB in ClientDB)
                {
                    using (SqlConnection con = new SqlConnection(DB))
                    {
                        con.Open();

                        foreach (string sc in scripts)
                        {
                            try
                            {
                                SqlCommand cmd = new SqlCommand(sc, con);
                                int error = cmd.ExecuteNonQuery();
                            }
                            catch (SqlException EX)
                            {

                                if (isFile)
                                {
                                    CreatErrorLogOnScriptFile(EX, DB, scriptId, Servername);
                                }
                                else
                                {
                                    CreatErrorLogOnScriptText(EX, DB, scriptId, Servername);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SQlScript(string s, out List<string> l)
        {
            l = new List<string>();

            string[] se = s.Split(new string[] { "GO", "go", "Go", "gO" }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in se)
            {
                if (!String.IsNullOrWhiteSpace(item) && item.Length > 11)
                {
                    if (!item.Substring(0, 10).Contains("PRINT"))
                    {
                        l.Add(item);
                    }
                }
            }
        }

        public List<string> SplitScript(MemoryStream data)
        {
            List<string> scripts = new List<string>();
            try
            {
                using (var sr = new StreamReader(data))
                {
                    string finalscript = "";
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (String.IsNullOrWhiteSpace(line))
                        {
                            if (finalscript.Length > 0)
                            {
                                finalscript = finalscript + "\n" + line;
                            }
                        }
                        else if (!String.IsNullOrEmpty(line))
                        {
                            string SplitWord = line.ToUpper().Trim();
                            if (SplitWord != "GO")
                            {
                                if (finalscript.Length > 0)
                                {
                                    finalscript = finalscript + "\n" + line;
                                }
                                else
                                {
                                    finalscript = line;
                                }

                            }
                            else
                            {
                                if (finalscript.Length > 0)
                                {
                                    scripts.Add(finalscript);
                                    finalscript = "";
                                }
                            }
                        }
                        else
                        {
                            finalscript = finalscript + "\n";
                        }
                    }

                    if (finalscript.Length > 0)
                    {
                        scripts.Add(finalscript);
                        finalscript = "";
                    }
                }
            }
            catch (Exception)
            {

            }
            return scripts;
        }


        public List<string> SplitScript(DirectoryInfo scriptlocation)
        {
            List<string> script = new List<string>();
            try
            {
                string sql = System.IO.File.ReadAllText((scriptlocation).FullName);
                string[] se = sql.Split(new string[] { "GO", "go", "Go", "gO" }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in se)
                {
                    if (!String.IsNullOrWhiteSpace(item) && item.Length > 11)
                    {
                        if (!item.Substring(0, 10).Contains("PRINT"))
                        {
                            script.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return script;
        }

        public void CreatErrorLogOnScriptFile(SqlException EX, String ClientDB, int scriptId, string Servername)
        {
            try
            {
                for (int i = 0; i < EX.Errors.Count; i++)
                {
                    using (ApplicationDbContext ctx = new ApplicationDbContext(Servername))
                    {
                        SQLExceptionErrorLog sql = new SQLExceptionErrorLog
                        {
                            DataBaseUpdateId = scriptId,
                            DataBaseName = ClientDB,
                            ErrorMessage = EX.Errors[i].Message,
                            LineNumber = EX.Errors[i].LineNumber,
                            Procedure = EX.Errors[i].Procedure,
                            Server = EX.Errors[i].Server
                        };
                        ctx.SQLExceptionErrorLogs.Add(sql);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void CreatErrorLogOnScriptText(SqlException EX, String ClientDB, int scriptId, string Server)
        {
            try
            {
                for (int i = 0; i < EX.Errors.Count; i++)
                {
                    using (ApplicationDbContext ctx = new ApplicationDbContext(Server))
                    {
                        SQLExceptionErrorLog sql = new SQLExceptionErrorLog
                        {
                            ScriptTextId = scriptId,
                            DataBaseName = ClientDB,
                            ErrorMessage = EX.Errors[i].Message,
                            LineNumber = EX.Errors[i].LineNumber,
                            Procedure = EX.Errors[i].Procedure,
                            Server = EX.Errors[i].Server
                        };
                        ctx.SQLExceptionErrorLogs.Add(sql);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public List<string> SplitScript(string script)
        {
            List<string> scripts = new List<string>();
            try
            {
                SQlScript(script, out scripts);
            }
            catch (Exception)
            {

                throw;
            }
            return scripts;
        }

        public static bool ChechDBExists(string db)
        {
            using (var ctx = new ScriptDBContext(db))
            {
                if (ctx.Database.Exists())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Write(Stream s, Byte[] bytes)
        {
            using (var writer = new BinaryWriter(s))
            {
                writer.Write(bytes);
            }
        }
    }
}