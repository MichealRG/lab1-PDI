using RssReader.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace RssReader
{
    public partial class RssDisplay : System.Web.UI.Page
    {
        static List<List<RssItem>> pageRssItems = new List<List<RssItem>>();
        protected void Page_Load(object sender, EventArgs e)
        {
            var xml = XElement.Load("https://news.google.com/rss?hl=en-US&gl=US&ceid=US:en");
            //var items = from n in xml.Descendants("item")
            //            select new RssItem
            //            {
            //                Title = n.Element("title").Value,
            //                Description= n.Element("description").Value,
            //                PubDate = n.Element("pubDate").Value,
            //            };
            var items = xml.Descendants("item").Select(x => new RssItem
            {
                Title = x.Element("title").Value,
                Description = x.Element("description").Value,
                PubDate = x.Element("pubDate").Value,
            });
            for (int i = 0; i < 4; i++)
            {
                pageRssItems.Add(new List<RssItem>());
            }
            for (int i = 0; i < items.Count(); i+=4)
            {
                if (i == items.Count() || items.Count() == i + 1 || items.Count() == i + 2 || items.Count() == i + 3)
                    break;
                pageRssItems[0].Add(items.ElementAt(i));
                pageRssItems[1].Add(items.ElementAt(i+1));
                pageRssItems[2].Add(items.ElementAt(i+2));
                pageRssItems[3].Add(items.ElementAt(i+3));
            }

            //Create the PagedDataSource that will be used in paging
            PagedDataSource pgitems = new PagedDataSource();
            pgitems.DataSource = items;
            
            pgitems.AllowPaging = true;

            //Control page size from here 
            pgitems.PageSize = 4;
            pgitems.CurrentPageIndex = PageNumber;
            if (pgitems.PageCount > 1)
            {
                Repeater1.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i <= pgitems.PageCount - 1; i++)
                {
                    pages.Add((i + 1).ToString());
                }
                Repeater1.DataSource = pages;
                Repeater1.DataBind();
            }
            else
            {
                Repeater1.Visible = false;
            }

            //Finally, set the datasource of the repeater
            Repeater1.DataSource = pgitems;
            Repeater1.DataBind();

            //GridView1.DataSource = pageRssItems[0];
            //GridView1.DataBind();
            //Repeater1.DataSource = items;
            //Repeater1.DataBind();//przypiecie danych
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var link = TextBox1.Text;
            var xml = XElement.Load(link);
            var items = xml.Descendants("item").Select(x => new RssItem
            {
                Title = x.Element("title").Value,
                Description = x.Element("description").Value,
                PubDate = x.Element("pubDate").Value,
            });
            //GridView1.DataSource= items;
            //GridView1.DataBind();
            //Repeater1.DataSource = items;
            //Repeater1.DataBind();//przypiecie danych
        }
        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }
        protected void rptPaging_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
            Page_Load(source,e);
        }
        //protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GridView1.DataBind();
        //}
    }
}