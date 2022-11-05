#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.UI.Events;

#endregion

namespace RAB_Session01_Skills
{
    [Transaction(TransactionMode.Manual)]
    public class Cmd01Challenge : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;


            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(TextNoteType));
            
            XYZ insPoint = new XYZ(0,0,0);
            var offset = 0.05;
            var calcOffset = offset * doc.ActiveView.Scale;
            XYZ offsetPoint = new XYZ(0, calcOffset,0);

            var range = 100;
            using (Transaction t = new Transaction(doc))
            {
                t.Start("FizzBuzz");
                for (var i = 1; i <= range; i++)
                {
                    var result = "";
                    if (i % 3 == 0)
                    {
                        result = "FIZZ";
                    }
                    if (i % 5 == 0)
                    {
                        result += "BUZZ";
                    }
                    if (i % 3 != 0 && i % 5 != 0)
                    {
                        result = i.ToString();
                    }

                    Debug.Print(result);

                    TextNote myTextNote = TextNote.Create(doc, doc.ActiveView.Id, insPoint, result, collector.FirstElementId());

                    insPoint = insPoint.Subtract(offsetPoint);
                }
                t.Commit();
            }
            return Result.Succeeded;

        }
    }
}
