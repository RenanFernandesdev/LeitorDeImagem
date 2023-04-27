using System;
using Tesseract;
using System.IO;

namespace LeitorDeImagem
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile;
            string[] files = Directory.GetFiles(@"");
            string directoryLocale = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(directoryLocale + "\\Retorno");
            


            for (int i = 0; i < files.Length; i++)
            {
                string pathToSave = directoryLocale + $"\\Retorno\\Retorno da leitura {i+1} - {DateTime.Now.ToString("hhmmss")}.txt";
                using (StreamWriter sr = new StreamWriter(pathToSave))
                {

                    using (var ocr = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                    {
                        sourceFile = files[i];
                        using (var img = Pix.LoadFromFile(sourceFile))
                        {
                            var result = ocr.Process(img);

                            var textResult = result.GetText();

                            sr.Write(textResult);

                            Console.Clear();
                            Console.WriteLine(Percent(files.Length, i+1).ToString("F2") + "% - " + i + " de " + files.Length);
                        }
                    }

                }
            }
        }

        static double Percent(int lengthFiles, int i)
        {
            double result = (double)(i / lengthFiles) * 100.0;

            return result;
        }
    }
}
