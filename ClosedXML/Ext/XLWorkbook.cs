using System.IO;

using ClosedXML.Excel;

namespace HaFT.Utilities.ClosedXML;

public static class XLWorkbookExt
{
	public static byte[] GetBytes(this XLWorkbook book)
	{
		using var ms = new MemoryStream();
		book.SaveAs(ms);
		return ms.ToArray();
	}
}
