using Microsoft.Extensions.Configuration;

namespace JobCandidates.Application.Common
{
    public class AttachmentService : IAttachmentService
    {
     

        private IConfiguration _configuration;
        public AttachmentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get real directory after mapping mask variables in directory mask (ex: /real/dir/)
        /// </summary>
        /// <param name="directoryMask">Directory containing variables to be replaced</param>
        /// <param name="maskVars">Loaded variables to be replaced in masked directory</param>
        /// <returns>Real directory after applying mask variables</returns>
        public string GetMappedDirectory(string directoryMask, Dictionary<string, string> maskVars, UploadTypeEnum uploadType)
        {
            string baseDir = _configuration.GetSection("Attachment").GetSection("AttachmentPath").Value;
            foreach (var item in maskVars)
            {
                directoryMask = directoryMask.Replace("{" + item.Key + "}", item.Value);
            }

            if (directoryMask.Contains("{") || directoryMask.Contains("}"))
            {
                List<string> missingMaskVarList = new List<string>();
                var missingMaskVarSplitted = directoryMask.Split('{');
                foreach (var item in missingMaskVarSplitted)
                    missingMaskVarList.Add(item.Split('}')[0]);
                new BadRequestException("This mask Variables ar missing : " + string.Join(",", missingMaskVarList));
            }

            baseDir += directoryMask;

            if (uploadType == UploadTypeEnum.LocalServer)
            {
                if (!Directory.Exists(baseDir))
                    Directory.CreateDirectory(baseDir);
            }

            return baseDir;

        }

        public void UploadAttachment(List<KeyValuePair<Stream, string>> attachments, UploadTypeEnum uploadType)
        {
            if (uploadType == UploadTypeEnum.LocalServer)
            {
                foreach (var item in attachments)
                {
                    using (FileStream outputFileStream = File.Create(item.Value))
                    {
                        item.Key.CopyTo(outputFileStream);
                    }
                }
            }

        }


        public byte[] DownloadAttachment(string filePath, UploadTypeEnum uploadType)
        {
            if (uploadType == UploadTypeEnum.LocalServer)
            {
                return File.ReadAllBytes(Path.Combine(filePath));
            }

            return null;
        }
    }
}
