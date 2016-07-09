using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LectorASP.Controllers
{
    public class LectorCreateController : Controller
    {
        // GET: LectorCreate
        public ActionResult Index()
        {
            return View();
        }
        /*
        [HttpPost]
        public ActionResult GetEntitiesFromDB([Bind(Include = "tables")] JsonDictionary tables)
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            var otherController = DependencyResolver.Current.GetService<HomeController>();

            //var json = new System.Web.Script.Serialization.JavaScriptSerializer();
            //var data = json.Deserialize<Dictionary<string, IEnumerable<string>>>(tables);
            Dictionary<string, List<Models.DataBaseShowCustomContainer>> customEntityDictionary = new Dictionary<string, List<Models.DataBaseShowCustomContainer>>();

            foreach (var item in tables)
            {
                List<Models.DataBaseShowCustomContainer> customEntityList = new List<Models.DataBaseShowCustomContainer>();
                switch (item.Key)
                {
                    case "Question":
                        {
                            customEntityList = otherController.FillCustomContainerQuestion(ref lectorSrv, "", item.Value as IEnumerable<string>);
                            break;
                        }
                    case "Chapter":
                        {
                            customEntityList = otherController.FillCustomContainerChapter(ref lectorSrv, "", item.Value as IEnumerable<string>);
                            break;
                        }
                    case "Student":
                        {
                            customEntityList = otherController.FillCustomContainerStudent(ref lectorSrv, "", item.Value as IEnumerable<string>);
                            break;
                        }
                    case "Subject":
                        {
                            customEntityList = otherController.FillCustomContainerSubject(ref lectorSrv, "", item.Value as IEnumerable<string>);
                            break;
                        }
                    case "Test":
                        {
                            customEntityList = otherController.FillCustomContainerTest(ref lectorSrv, "", item.Value as IEnumerable<string>);
                            break;
                        }
                }
                customEntityDictionary.Add(item.Key, customEntityList);
            }

            return Json(customEntityDictionary, JsonRequestBehavior.AllowGet);
        }
        */

        [HttpGet]
        public ActionResult GetEntitiesFromDB(List<string> tables, string includeProperties = "", string constraint = null)
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            var otherController = DependencyResolver.Current.GetService<HomeController>();

            //var json = new System.Web.Script.Serialization.JavaScriptSerializer();
            //var data = json.Deserialize<Dictionary<string, IEnumerable<string>>>(tables);
            Dictionary<string, List<Models.DataBaseShowCustomContainer>> customEntityDictionary = new Dictionary<string, List<Models.DataBaseShowCustomContainer>>();

            foreach (var item in tables)
            {
                List<Models.DataBaseShowCustomContainer> customEntityList = new List<Models.DataBaseShowCustomContainer>();
                switch (item)
                {
                    case "Question":
                        {
                            customEntityList = otherController.FillCustomContainerQuestion(ref lectorSrv, includeProperties, constraint);
                            break;
                        }
                    case "Chapter":
                        {
                            customEntityList = otherController.FillCustomContainerChapter(ref lectorSrv, includeProperties, constraint);
                            break;
                        }
                    case "Student":
                        {
                            customEntityList = otherController.FillCustomContainerStudent(ref lectorSrv, includeProperties, constraint);
                            break;
                        }
                    case "Subject":
                        {
                            customEntityList = otherController.FillCustomContainerSubject(ref lectorSrv, includeProperties, constraint);
                            break;
                        }
                    case "Test":
                        {
                            customEntityList = otherController.FillCustomContainerTest(ref lectorSrv, includeProperties, constraint);
                            break;
                        }
                }
                customEntityDictionary.Add(item, customEntityList);
            }

            return Json(customEntityDictionary, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateQuestion()
        {
            var x = Request.Form;
            return null;
        }
    }
}