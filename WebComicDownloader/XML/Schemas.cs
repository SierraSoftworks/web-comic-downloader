using System.IO;
using System.Text;
using System.Xml.Schema;

namespace WebcomicDownloader.XMLSearcher
{
    internal static class Schemas
    {
        public static XmlSchema WebComicSchema
        {
            get
            {
                try
                {
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(WebcomicDownloader.Properties.Resources.WebComicDefinition));
                    XmlSchema schema = XmlSchema.Read(ms, (o, e) =>
                        {
                            throw e.Exception;
                        });

                    return schema;
                }
                catch
                {
                    return new XmlSchema();
                }
            }
        }

        public static XmlSchema WebComicSaveSchema
        {
            get
            {
                try
                {
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(WebcomicDownloader.Properties.Resources.WebComicSave));
                    XmlSchema schema = XmlSchema.Read(ms, (o, e) =>
                    {
                        throw e.Exception;
                    });

                    return schema;
                }
                catch
                {
                    return new XmlSchema();
                }
            }
        }
    }
}