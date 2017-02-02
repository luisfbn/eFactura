using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace eFactura
{
    public class Comprobante
    {

        public bool EsValido;
        public bool Procesado;
        public string InvalidoPor;

        public SAT.Comprobante ConvertirArchivoXmlEntidad(HttpPostedFileBase fileHttp)
        {
            EsValido = false;
            Procesado = false;
            InvalidoPor = string.Empty;

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(SAT.Comprobante));

                //https://richardscannell.wordpress.com/2014/07/23/c-function-for-file-upload-to-convert-httppostedfilebase-to-byte-array/

                Stream fileStream = fileHttp.InputStream;
                var mStreamer = new MemoryStream();
                mStreamer.SetLength(fileStream.Length);
                fileStream.Read(mStreamer.GetBuffer(), 0, (int)fileStream.Length);
                mStreamer.Seek(0, SeekOrigin.Begin);
                //byte[] fileBytes = mStreamer.GetBuffer();

                SAT.Comprobante cXML = ser.Deserialize(mStreamer) as SAT.Comprobante;

                EsValido = true;

                return cXML;

            } //try

          

            catch (System.InvalidOperationException ex)
            {
                InvalidoPor = ex.InnerException.Message;
                EsValido = false;
                Procesado = true;
            }

            catch (System.Exception ex)
            {

                InvalidoPor = ex.Message;
                EsValido = false;
                Procesado = true;
            }

            return null; // comprobanteE;
        }

        
        
    }
}