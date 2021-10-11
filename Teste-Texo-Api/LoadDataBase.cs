using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Texo_Api
{
    public class LoadDataBase
    {

        public void LoadData(CatalogContext context)
        {
            int year; 
            string title; 
            string studios; 
            string producers;
            bool winner;
            TitleItem titleItem;


            var path = @$"{AppDomain.CurrentDomain.BaseDirectory}\movielist.csv";
            using (TextFieldParser csvReader = new TextFieldParser(path))
            {
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { ";" });
                csvReader.HasFieldsEnclosedInQuotes = true;

                csvReader.ReadLine();

                while (!csvReader.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.

                    string[] fields = csvReader.ReadFields();

                    titleItem = new TitleItem();
                    titleItem.Year = Int32.Parse(fields[0]);
                    titleItem.Title = fields[1];
                    titleItem.Studios = fields[2];
                    titleItem.Producers = fields[3];
                    titleItem.Winner = fields[4].Equals("YES");

                    context.TitleItems.Add(titleItem);
                    context.SaveChanges();
                    //System.Console.WriteLine(title);
                }

            }

        }

    }
}
