using System;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace XML_en_ListView
{
    public partial class Form1 : Form
    {
        private string xmlUrl;

        public Form1()
        {
            InitializeComponent();
            // Asigna la URL a la variable xmlUrl
            xmlUrl = "https://www.w3schools.com/xml/cd_catalog.xml";
            // Llama al método para cargar el XML de manera asincrónica
            LVXmlReaderLoad();
        }

        private async void LVXmlReaderLoad()
        {
            try
            {
                WebClient client = new WebClient();
                string xmlContent = await client.DownloadStringTaskAsync(xmlUrl);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlContent);

                XmlNodeList cds = xDoc.GetElementsByTagName("CD");

                // Agrega los encabezados al ListView
                listView1.View = View.Details;
                listView1.Columns.Add("TITLE", 150);
                listView1.Columns.Add("ARTIST", 150);
                listView1.Columns.Add("COUNTRY", 100);
                listView1.Columns.Add("COMPANY", 100);
                listView1.Columns.Add("PRICE", 80);
                listView1.Columns.Add("YEAR", 80);

                // Itera sobre los elementos y agrega información al ListView
                foreach (XmlElement cd in cds)
                {
                    string title = cd["TITLE"]?.InnerText ?? "";
                    string artist = cd["ARTIST"]?.InnerText ?? "";
                    string country = cd["COUNTRY"]?.InnerText ?? "";
                    string company = cd["COMPANY"]?.InnerText ?? "";
                    string price = cd["PRICE"]?.InnerText ?? "";
                    string year = cd["YEAR"]?.InnerText ?? "";

                    ListViewItem item = new ListViewItem(title);
                    item.SubItems.Add(artist);
                    item.SubItems.Add(country);
                    item.SubItems.Add(company);
                    item.SubItems.Add(price);
                    item.SubItems.Add(year);
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el XML: " + ex.Message);
            }
        }

    }
}
