using firstTry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.Win32;
//using System.Windows;
using System.Windows.Forms;

namespace firstTry.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var labelViewModel = new LabelViewModel();
            labelViewModel.FileNameLabel = "no file";

            return View(labelViewModel);
        }

        [HttpPost]
        public IActionResult Click()
        {
            Thread t = new Thread(OpenFiles);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            t.Join();

            if (ViewBag.File != "")
            {
                var extMetadata = new ExtractMetadata();
                var metadataDict = extMetadata.ExtractAllMetadata((string)ViewData["File"]);
                foreach (string key in metadataDict.Keys)
                {
                    ViewData[key] = metadataDict[key];
                }
               

            }
            return View("Index");
        }

        public void OpenFiles()
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы pdf|*.pdf|Файлы docx|*.docx";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                ViewData["File"] = OPF.FileName;
                
            }
        }

        //[HttpPost]
        //public string FindMetadata(string i)
        //{
        //    return i;
        //}
        //void button1_Click(object sender, EventArgs e)
        //{
        //    Console.WriteLine("presssed");
        //    //(Button)sender.Text = "123";
        //    //((Label)FindControl("fileName")).Text = "123";
        //}

        //public void ButtonPressed()
        //{

        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
