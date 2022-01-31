using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace mpex.deployment.web.Services
{
    public sealed class AppPool
    {
        private static readonly AppPool instance = new AppPool();  

        private AppPool() { }
        static AppPool() { }

        public static AppPool Instance
        {
            get
            {
                return instance;
            }

        }

        public void StopAllAppplicationPools(List<string> APools)
        {

            using (ServerManager iisManager = new ServerManager())
            {
                ApplicationPoolCollection SPools = iisManager.ApplicationPools;
                foreach (string Apool in APools)
                {
                    foreach (ApplicationPool SPool in SPools)
                    {
                        if (string.Equals(SPool.Name, Apool))
                        {
                            StopApplicationPool(SPool);
                        }
                    }
                }
            }
        }

        public void StartAllAppplicationPools(List<string> APools)
        {

            using (ServerManager iisManager = new ServerManager())
            {
                ApplicationPoolCollection SPools = iisManager.ApplicationPools;
                foreach (string Apool in APools)
                {
                    foreach (ApplicationPool SPool in SPools)
                    {
                        if (string.Equals(SPool.Name, Apool))
                        {
                            StartApplicationPool(SPool);
                        }
                    }
                }
            }
        }

        public void RefreshAllAppplicationPools(List<string> APools)
        {

            using (ServerManager iisManager = new ServerManager())
            {
                ApplicationPoolCollection SPools = iisManager.ApplicationPools;
                foreach (string Apool in APools)
                {
                    foreach (ApplicationPool SPool in SPools)
                    {
                        if (string.Equals(SPool.Name, Apool))
                        {
                            RefreshApplicationPool(SPool);
                        }
                    }
                }
            }
        }

        public void StartApplicationPool(ApplicationPool pool)
        {
            if (pool.State == ObjectState.Stopped)
            {
                pool.Start();
            }
        }

        public void StopApplicationPool(ApplicationPool pool)
        {
            if (pool.State == ObjectState.Started)
            {
                pool.Stop();
            }
        }

        public void RefreshApplicationPool(ApplicationPool pool)
        {
            if (pool.State == ObjectState.Stopped)
            {
                pool.Recycle();
            }
        }

        public bool PoolExists(string p)
        {
            using (ServerManager iisManager = new ServerManager())
            {
                if (iisManager.ApplicationPools.Any(n => n.Name == p))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}