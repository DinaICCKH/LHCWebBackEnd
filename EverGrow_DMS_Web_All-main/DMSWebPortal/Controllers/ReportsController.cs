using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;

public class ReportsController : Controller
{
    // Page that contains the report viewer
    public IActionResult Viewer()
    {
        return View();
    }

    // Generate report and display inline in browser via iframe
    public IActionResult PreviewReport()
    {
        string reportPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Reports",
            "Testing_Report.rdlc"
        );

        LocalReport report = new LocalReport(reportPath);

        // Render as PDF
        var result = report.Execute(RenderType.Pdf, 1, null, "");

        if (result?.MainStream == null)
            return Content("Error: Report generation failed!");

        // Inline display in browser (acts like HTML preview)
        Response.Headers["Content-Disposition"] = "inline; filename=Testing_Report.pdf";
        return File(result.MainStream, "application/pdf");
    }

    // Export buttons
    public IActionResult Export(string format)
    {
        string reportPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Reports",
            "Testing_Report.rdlc"
        );

        LocalReport report = new LocalReport(reportPath);

        RenderType renderType = format?.ToUpper() switch
        {
            "PDF" => RenderType.Pdf,
            "EXCEL" => RenderType.Excel,
            "WORD" => RenderType.Word,
            _ => RenderType.Pdf
        };

        var result = report.Execute(renderType, 1, null, "");

        if (result?.MainStream == null)
            return Content("Error: Report generation failed!");

        string mimeType = format?.ToUpper() switch
        {
            "PDF" => "application/pdf",
            "EXCEL" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "WORD" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => "application/pdf"
        };

        string ext = format?.ToUpper() switch
        {
            "PDF" => "pdf",
            "EXCEL" => "xlsx",
            "WORD" => "docx",
            _ => "pdf"
        };

        return File(result.MainStream, mimeType, $"Testing_Report.{ext}");
    }
}