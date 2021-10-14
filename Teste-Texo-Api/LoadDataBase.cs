﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_Texo_Api
{
    public class LoadDataBase
    {
     
        public void LoadData(CatalogContext context)
        {
            MovieItem titleItem;

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

                    titleItem = new MovieItem();
                    titleItem.Year = Int32.Parse(fields[0]);
                    titleItem.Title = fields[1];
                    foreach (var studio in fields[2].Split(',').ToList())
                    {
                        if (studio.Trim() != "")
                        {
                            titleItem.Studios.Add(studio.Trim());
                        }
                    }
                    foreach (var producer in fields[3].Split(',').ToList())
                    {
                        if (producer.Trim() != "")
                        {
                            if (producer.Contains("and"))
                            {
                                foreach (var producersplitand in producer.Split(" and ").ToList())
                                {
                                    if (producersplitand.Trim() != "") { 
                                        titleItem.Producers.Add(producersplitand.Trim());
                                    }
                                }                                    
                            }
                            else
                            {
                                titleItem.Producers.Add(producer.Trim());
                            }                            
                        }
                    }
                    //titleItem.Studios = fields[2].Split(',').ToList();
                    //titleItem.Producers = fields[3].Split(',').ToList();
                    titleItem.Winner = fields[4].ToUpper().Equals("YES");

                    context.TitleItems.Add(titleItem);
                    context.SaveChanges();
                    //System.Console.WriteLine(title);
                }

            }

        }

    }
}
