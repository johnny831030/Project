using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace longtermcare
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string index = Request["target"];
                string filename = Request["filename"];
                string docupath = Request.PhysicalApplicationPath;
                if (!Directory.Exists(docupath + "Accreditations\\AccFiles\\temp"))
                    Directory.CreateDirectory(docupath + "Accreditations\\AccFiles\\temp");
                string err = "";
                if (Request["mode"] == "download")
                {
                    System.Net.WebClient wc = new System.Net.WebClient(); //呼叫 webclient 方式做檔案下載
                    byte[] xfile = null;
                    xfile = wc.DownloadData(docupath + "Accreditations\\AccFiles\\" + index + "_" + filename);
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                    Response.ContentType = "application/octet-stream"; //二進位方式
                    Response.BinaryWrite(xfile); //內容轉出作檔案下載
                    Response.End();
                }
                else if (Request["mode"] == "print")
                {
                    string[] exfilename = filename.Split('.');

                    switch (exfilename[exfilename.Length - 1].ToLower())
                    {
                        case "pptx":
                        case "ppt":
                            PowerPoint2Pdf(docupath + "Accreditations\\AccFiles\\" + filename, docupath + "Accreditations\\AccFiles\\temp\\" + exfilename[0] + ".pdf");
                            break;
                        case "xlsx":
                        case "xls":
                            Excel2Pdf(docupath + "Accreditations\\AccFiles\\" + filename, docupath + "Accreditations\\AccFiles\\temp\\" + exfilename[0] + ".pdf");
                            break;
                        case "docx":
                        case "doc":
                            Word2Pdf(docupath + "Accreditations\\AccFiles\\" + filename, docupath + "Accreditations\\AccFiles\\temp\\" + exfilename[0] + ".pdf");
                            break;
                        case "jpeg":
                        case "jpg":
                        case "png":
                        case "bmp":
                            try
                            {
                                Document doc1 = new Document(PageSize.A4);
                                PdfWriter.GetInstance(doc1, new FileStream(docupath + "Accreditations\\AccFiles\\temp\\" + exfilename[0] + ".pdf", FileMode.Create));
                                doc1.Open();
                                iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(new Uri(docupath + "Accreditations\\AccFiles\\" + filename));
                                //pic.Alignment = iTextSharp.text.Image.ALIGN_JUSTIFIED_ALL;
                                //pic.ScalePercent(100);
                                doc1.Add(pic);
                                doc1.Close();
                            }
                            catch (Exception ex)
                            { Console.WriteLine(ex.Message); }
                            break;
                        default:
                            err += exfilename[exfilename.Length - 1] + ", ";
                            break;
                    }

                    if (err.Length > 0)
                    {
                        err = err.Trim().TrimEnd(',');
                    }

                }
                else if (Request["mode"] == "merge")
                {
                    String[] FileCollection;
                    String FilePath = docupath + "Accreditations\\AccFiles\\temp";
                    string name = sqlTime.datetime() + ".pdf";
                    FileCollection = Directory.GetFiles(FilePath, "*.*");
                    MergePDFs(docupath + "Accreditations\\AccFiles\\" + name, FileCollection);

                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] xfile = null;
                    xfile = wc.DownloadData(docupath + "Accreditations\\AccFiles\\" + name);
                    Response.AddHeader("content-disposition", "attachment;filename=" + name);
                    Response.ContentType = "application/octet-stream"; //二進位方式
                    Response.BinaryWrite(xfile); //內容轉出作檔案下載
                    Response.End(); Directory.Delete(docupath + "Accreditations\\AccFiles\\temp", true);
                }
                else if (Request["mode"] == "delete")
                {
                    try
                    { Directory.Delete(docupath + "Accreditations\\AccFiles\\temp", true); }
                    catch (Exception ex) { Console.Error.WriteLine(ex.Message); }
                }

            }
            catch { }
        }

        public static bool PowerPoint2Pdf(string source, string target)
        {
            try
            {
                var PowerPointApp = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Interop.PowerPoint.Presentation presentation = PowerPointApp.Presentations.Open(source, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
                presentation.SaveAs(target, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF);
                PowerPointApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(PowerPointApp);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static bool Excel2Pdf(string source, string target)
        {
            try
            {
                var ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook book = ExcelApp.Workbooks.Open(source);
                Microsoft.Office.Interop.Excel.XlFileFormat xlFormatPDF = (Microsoft.Office.Interop.Excel.XlFileFormat)57;
                book.SaveAs(target, xlFormatPDF);
                ExcelApp.Visible = false;
                ExcelApp.Quit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static bool Word2Pdf(string source, string target)
        {
            try
            {
                var WordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = WordApp.Documents.Open(source);
                doc.SaveAs(target, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                WordApp.Visible = false;
                WordApp.Quit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static string MergePDFs(string destinationFile, string[] sourceFiles)
        {
            string ret = "ok";
            try
            {
                int f = 0;
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(sourceFiles[f]);
                // we retrieve the total number of pages
                int n = reader.NumberOfPages;
                Console.WriteLine("There are " + n + " pages in the original file.");
                // step 1: creation of a document-object
                Document document = new Document(reader.GetPageSizeWithRotation(1));
                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
                // step 3: we open the document
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;
                // step 4: we add content
                while (f < sourceFiles.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        Console.WriteLine("Processed page " + i);
                    }
                    f++;
                    if (f < sourceFiles.Length)
                    {
                        reader = new PdfReader(sourceFiles[f]);
                        // we retrieve the total number of pages
                        n = reader.NumberOfPages;
                        Console.WriteLine("There are " + n + " pages in the original file.");
                    }
                }
                // step 5: we close the document
                document.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
                ret = e.Message;
            }
            return ret;
        }
    }
}