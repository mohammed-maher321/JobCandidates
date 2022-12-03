namespace JobCandidates.Application.Common
{
    public interface IAttachmentService
    {
        public string GetMappedDirectory(string directoryMask, Dictionary<string, string> maskVars, UploadTypeEnum uploadType);
        public void UploadAttachment(List<KeyValuePair<Stream, string>> attachments, UploadTypeEnum uploadType);
        public byte[] DownloadAttachment(string filePath, UploadTypeEnum uploadType);
    }
}
