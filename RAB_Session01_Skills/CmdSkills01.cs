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
    public class CmdSkills01 : IExternalCommand
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

            // 1. variables (string, int, double, XYZ)
            var myString = "Welcome to Revit Add-in Bootcamp ";
            var mySecongString = myString + ". It's great to have you here.";
            var filePath = @"C:\documents\mydocument";

            var myNumber = 15;
            var myNextNumber = -20.5;
            var answer = (myNumber + myNextNumber) / (10 * 20);

            XYZ myPoint = new XYZ(10,10,0);
            XYZ myNextPoint = new XYZ();

            // 5. Filtered Element Collectors
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfCategory(BuiltInCategory.OST_TextNotes);
            //collector.WhereElementIsElementType();
            collector.OfClass(typeof(TextNoteType));

          

            Transaction t = new Transaction(doc);
            t.Start("Create text note");

            XYZ offset = new XYZ(0, 5, 0);
            XYZ newPoint = myPoint;

            // 2. for loop
            var total = 0;
            for (var i = 0; i <=10; i++)
            {
                newPoint = newPoint.Add(offset);

                // 4. Text Notes
                TextNote myTextNote = TextNote.Create(doc,
                    doc.ActiveView.Id, newPoint,
                    myString + i.ToString() + "\n",
                    collector.FirstElementId());
            }

            t.Commit();
            return Result.Succeeded;
        }
    }
}
