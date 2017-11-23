using PreFinalQuiz1.App_Code;
using PreFinalQuiz1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreFinalQuiz1.Controllers
{
    public class TitlesController : Controller
    {

        public ActionResult Add()
        {
            TitlesModels titles = new TitlesModels();
            titles.Publishers = GetCategories();
            titles.Authors = GetCategories1();
            return View(titles);
        }


        [HttpPost]
        public ActionResult Add(TitlesModels titles)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Titles VALUES 
                (@pubID, @authorID, @titleName, @titlePrice, @titlePubDate, @titleNotes)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@pubID", titles.PubID);
                    cmd.Parameters.AddWithValue("@authorID", titles.AuthorID);
                    cmd.Parameters.AddWithValue("@titleName", titles.Name);
                    cmd.Parameters.AddWithValue("@titlePrice", titles.Price);
                    cmd.Parameters.AddWithValue("@titlePubDate", titles.PubDate);
                    cmd.Parameters.AddWithValue("@titleNotes", titles.Notes);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }


        public List<SelectListItem> GetCategories()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT pubID, pubName FROM publishers ORDER BY pubName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = data["pubName"].ToString(),
                                Value = data["pubID"].ToString()
                            });
                        }
                    }
                }
            }

            return items;
        }

        public List<SelectListItem> GetCategories1()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT authorID, authorLN, authorFN FROM authors ORDER BY authorLN";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = data["authorLN"].ToString() + "," +
                                data["authorFN"].ToString(),
                                Value = data["authorID"].ToString()
                            });
                        }
                    }
                }
            }

            return items;
        }

        // GET: Titles
        public ActionResult Index()
        {

            List<TitlesModels> list = new List<TitlesModels>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT t.titleID, t.titleName, p.pubName, 
                a.authorLN, a.authorFN, t.titlePrice, t.titlePubDate, t.titleNotes
                FROM titles t INNER JOIN publishers p ON t.pubID = p.pubID
                INNER JOIN authors a ON t.authorID = a.authorID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new TitlesModels
                            {
                                ID = Convert.ToInt32(data["titleID"].ToString()),
                                Name = data["titleName"].ToString(),
                                Publisher = data["pubName"].ToString(),
                                Author = data["authorLN"].ToString() + "," +
                                data["authorFN"].ToString(),
                                Price = data["titlePrice"].ToString(),
                                Notes = data["titleNotes"].ToString(),
                                PubDate = data["titlePubDate"].ToString()
                            });
                        }

                    }
                }
            }

            return View(list);
        }
    }
}