using log4net;
using Log4net.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestLog4net
{
    public partial class Home : System.Web.UI.Page
    {
        private static readonly LogWrapper _logger = new LogWrapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            _logger.Debug("home页面2");
            Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
            MachineKeySection configSection = (MachineKeySection)config.GetSection("system.web/machineKey");
            configSection.ValidationKey = CreateKey(64);
            configSection.DecryptionKey = CreateKey(24);
            configSection.Validation = MachineKeyValidation.SHA1;
            configSection.Decryption = "3DES";
            if (!configSection.SectionInformation.IsLocked)
            {
                config.Save();
            }
        }

        public void test()
        {
            var a = 0;
            var i = 1 / a;
        }

        /// <summary>
        /// 生成随机Key
        /// </summary>
        /// <param name="numBytes"></param>
        /// <returns></returns>
        public string CreateKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];
            rng.GetBytes(buff);
            System.Text.StringBuilder hexString = new StringBuilder(64);
            for (int i = 0; i < buff.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", buff[i]));
            }

            return hexString.ToString();

        }
    }
}