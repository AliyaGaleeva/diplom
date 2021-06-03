using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using SautinSoft.PdfFocus;

namespace firstTry
{
    public class ExtractMetadata
    {
        public int pageCount = 0;
        public Dictionary<string,string> ExtractAllMetadata(string fileName)
        {
            var dict = new Dictionary<string, string>();

            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(fileName);
            pageCount = f.PageCount;
            string[] pages = new string[f.PageCount];
            // MessageBox.Show(pages.Length.ToString());

            for (int i = 0; i < f.PageCount; i++)
            {
               
                pages[i] = f.ToText(i+1, 1);
              //  MessageBox.Show(pages[i]);

            }

            var blocks = MakeBlocks(pages);

            dict["Format"] = ExtractFormat(fileName);
            dict["Name"] = ExtractName(blocks);
            dict["Author"] = ExtractAuthor(pages);
            dict["Year"] = ExtractYear(pages);
            dict["Annotation"] = ExtractAnnotation(blocks);
            dict["KeyWords"] = ExtractKeyWords(blocks);
            dict["References"] = ExtractReferences(blocks);
            dict["Maths"] = ExtractMaths(fileName);
            return dict;
        }

        public string[][] MakeBlocks(string[] pages)
        {
            string[][] blocks = new string[pages.Length][];
            for (int i = 0; i < pages.Length; i++)
            {
                var parts = pages[i].Split(Environment.NewLine);
              

         //       parts = parts.ToList().Take(parts.Length - 3).ToArray();
                var myParts = new List<string>();
                var str = "";
                for (int j = 0; j < parts.Length - 1; j++)
                {
                    str = parts[j].Replace("Created by unlicensed version of PDF Focus .Net 7.8.1.29!", "").
                    Replace("The unlicensed version inserts \"trial\" into random places.", "").
                    Replace("This text will disappear after purchasing the license.", "");
                    //str = str + parts[j] + "\n";
                    //if (parts[j + 1] == "")
                    //{
                    //    myParts.Add(str);
                    //    str = "";
                    //}
                    myParts.Add(str);
                }
                var pageBlocks = myParts.Where(x => x != "").ToArray();
                
                //foreach(string bl in pageBlocks)
                //{
                //    MessageBox.Show(bl);
                //}
                blocks[i] = pageBlocks;
            }

       
            return blocks;
        }
        public string ExtractFormat(string fileName)
        {
            var res = fileName.Split('.').Last();
            return res;
        }

        public string ExtractName(string[][] blocks)
        {
            var res = "";

            
            return res;
        }

        public string ExtractAuthor(string[] pages)
        {
            var res = "";

            return res;
        }

        public string ExtractYear(string[] pages)
        {
            var res = "";

            return res;
        }

        public string ExtractAnnotation(string[][] blocks)
        {
            var res = "";

            int maxPage = Math.Min(pageCount, 3);
           for (int i=0; i<maxPage; i++)
            {
                for (int j=0; j<blocks[i].Length; j++) 
                {
                   // MessageBox.Show(blocks[i][j]);
                    if (blocks[i][j].Contains("Аннотация") || blocks[i][j].Contains("Annotation") || blocks[i][j].Contains("Abstract"))
                    {
                        if (blocks[i][j].Length <= 15) res = blocks[i][j + 1];
                        else res = blocks[i][j];
                     //   MessageBox.Show(res);
                        break;
                    }
                }
                if (res != "") break;
            }

            return res;
        }

        public string ExtractKeyWords(string[][] blocks)
        {
            var res = "";
            int maxPage = Math.Min(pageCount, 3);

            for (int i = 0; i < maxPage; i++)
            {
                for (int j = 0; j < blocks[i].Length; j++)
                {
                    // MessageBox.Show(blocks[i][j]);
                    if (blocks[i][j].Contains("Ключевые слова") || blocks[i][j].Contains("Keywords"))
                    {
                        if (blocks[i][j].Length <= 17) res = blocks[i][j + 1];
                        else res = blocks[i][j];
                      //  MessageBox.Show(res);
                        break;
                    }
                }
                if (res != "") break;
            }
            return res;
        }

        public string ExtractReferences(string[][] blocks)
        {
            var res = "";

            for (int i = 0; i < pageCount; i++)
            {
                for (int j = 0; j < blocks[i].Length; j++)
                {
                    // MessageBox.Show(blocks[i][j]);
                    if (blocks[i][j].Contains("Литература") || blocks[i][j].Contains("Список использованной литературы") || blocks[i][j].Contains("References"))
                    {
                        if (blocks[i][j].Length <= 40) res = blocks[i][j + 1];
                        else res = blocks[i][j];
                      //  MessageBox.Show(res);
                        break;
                    }
                }
                if (res != "") break;
            }

            return res;
        }

        public string ExtractMaths(string f)
        {
            var res = "";

            return res;
        }
    }
}
