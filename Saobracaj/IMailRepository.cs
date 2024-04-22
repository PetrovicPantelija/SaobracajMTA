using OpenPop.Mime;
using System.Collections.Generic;

namespace Saobracaj
{
    public interface IMailRepository
    {
        void Connect(string pop3Server, string pop3User, string pop3Pass, int pop3Port, bool pop3UseSsl);
        List<Pop3Mail> GetMail();
        List<Pop3Mail> GetMail(string toAddress);
        List<Pop3Mail> GetMail(string toAddress, string fromAddress);
        List<string> GetAttachments(Message msg);
        void DeleteAll();
        void Delete(int msgNumber);
    }
}
