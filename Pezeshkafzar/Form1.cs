using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pezeshkafzar.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace Pezeshkafzar
{
    public partial class Form1 : Form
    {
        Pezeshkafzar_db db = new Pezeshkafzar_db();
        public Form1()
        {
            InitializeComponent();
        }

        private UserCredential credential;

        private void button1_Click(object sender, EventArgs e)
        {
            //label2.Text = "در حال آپدیت قیمت ها. اندکی صبر...";
            dgResult.Rows.Clear();
            dgResult.Refresh();
            button1.Enabled = false;
            Cursor.Current = Cursors.No;
            dgResult.Cursor = Cursors.WaitCursor;
            string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string ApplicationName = "Pezeshkafzar Price Updater";


            if (credential == null)
            {
                using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }
            }
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            String spreadsheetId = "113B6GbzhMJkB-8mzgFOwaeAYDJdzE4eBjlJFTNRfcLE";
            String range = "Sheet1";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                List<Product> products = db.Products.ToList();
                int i = 0;
                foreach (var item in values)
                {
                    if (i != 0)
                    {
                        int productId = int.Parse(item[0].ToString());
                        if (products.Any(p => p.ProductID == productId))
                        {
                            var product = products.FirstOrDefault(p => p.ProductID == productId);
                            var a = item[2];
                            if (product.Price != int.Parse(item[2].ToString().Replace(",","")) || product.PriceAfterDiscount != int.Parse(item[3].ToString().Replace(",", "")) || product.Stock != int.Parse(item[4].ToString().Replace(",", "")))
                            {
                                product.Price = int.Parse(item[2].ToString().Replace(",", ""));
                                product.PriceAfterDiscount = int.Parse(item[3].ToString().Replace(",", ""));
                                product.Stock = int.Parse(item[4].ToString().Replace(",", ""));

                                product.LastUpdated = DateTime.Now;

                                dgResult.Rows.Add(i, product.Title, product.Price, product.PriceAfterDiscount, product.Stock);
                            }

                            db.Entry(product).State = System.Data.Entity.EntityState.Modified;

                            

                            //listView1.Items.Add($"{i} - {product.Title} : {product.Price} --->>> {item[2].ToString()}");
                        }
                    }
                    i++;
                }

                Product _product = products.FirstOrDefault(p => p.ProductID == 1009);
                _product.Stock = 100;
                db.SaveChanges();
                button1.Enabled = true;
                Cursor.Current = Cursors.Hand;
                dgResult.Cursor = Cursors.Default;
                //label2.Text = "تغییرات با موفقیت انجام شد!";
            }
            else
            {
                Console.WriteLine("No data found.");
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgProducts.DataSource = db.Products.ToList();
        }
    }


}
