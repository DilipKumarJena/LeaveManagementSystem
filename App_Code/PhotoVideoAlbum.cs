using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

/// <summary>
/// Summary description for PhotoVideoAlbum
/// </summary>
public sealed class PhotoVideoAlbum
{
    public PhotoVideoAlbum()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static object ConvertInByteArray(FileUpload info, string Size)
    {
        try
        {
            
            string s = info.FileName;
            string sss = HttpContext.Current.Server.MapPath("~/TempFolderForFileUpload");

            DeleteAllFiles(sss);
            
            sss = sss + "/" + s;




            info.SaveAs(sss);


            if (Size == "Small")
                FixedSize(sss, sss, 80, 60);
            if (Size == "Medium")
                FixedSize(sss, sss, 150, 100);
            if (Size == "Large")
                FixedSize(sss, sss, 600, 400);
            if (Size == "Equal")
                FixedSize(sss, sss, 250, 250);


            FileInfo obj = new FileInfo(sss);
            byte[] content = new byte[obj.Length];
            FileStream imagestream = obj.OpenRead();
            imagestream.Read(content, 0, content.Length);
            imagestream.Close();
            obj.Delete();
            return content;
        }
        catch
        {
            return DBNull.Value;
        }
    }


    public static void FixedSize(string strImageSrcPath, string strImageDesPath, int Width, int Height)
    {


        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(strImageSrcPath);


        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.Clear(Color.Red);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();

        imgPhoto.Dispose();
        imgPhoto = null;

        if (strImageSrcPath == strImageDesPath)
            System.IO.File.Delete(strImageSrcPath);

        bmPhoto.Save(strImageDesPath);
        bmPhoto.Dispose();

    }



    public static void ImageResize(string strImageSrcPath, string strImageDesPath, int intWidth, int intHeight)
    {
        if (System.IO.File.Exists(strImageSrcPath) != false)
        {
            System.Drawing.Image objImage = System.Drawing.Image.FromFile(strImageSrcPath);

            int H_Ratio = objImage.Width / objImage.Height;

            if (objImage.Width < objImage.Height)
            {
                int Temp = intWidth;
                intWidth = intHeight;
                intHeight = Temp;
            }


            if (intWidth > objImage.Width)
                intWidth = objImage.Width;
            if (intHeight > objImage.Height)
                intHeight = objImage.Height;
            if (intWidth == 0 && intHeight == 0)
            {
                intWidth = objImage.Width;
                intHeight = objImage.Height;
            }
            else if (intHeight == 0 && intWidth != 0)
                intHeight = objImage.Height * intWidth / objImage.Width;
            else if (intWidth == 0 && intHeight != 0)
                intWidth = objImage.Width * intHeight / objImage.Height;




            System.Drawing.Bitmap imgOutput = new System.Drawing.Bitmap(objImage, intWidth, intHeight);
            System.Drawing.Imaging.ImageFormat imgFormat = objImage.RawFormat;

            objImage.Dispose();
            objImage = null;

            if (strImageSrcPath == strImageDesPath)
                System.IO.File.Delete(strImageSrcPath);

            imgOutput.Save(strImageDesPath, imgFormat);
            imgOutput.Dispose();
        }
    }

    public static DataTable BuildDataTable(DataTable _Datatable, int NoOfColumnInDataTable, int NumberOfColumn)
    {
        DataTable dt = new DataTable();
        if (_Datatable.Rows.Count > 0)
        {
            for (int i = 1; i <= NumberOfColumn; i++)
            {
                for (int j = 0; j < NoOfColumnInDataTable; j++)
                    dt.Columns.Add(_Datatable.Columns[j].ColumnName + i.ToString());
            }

            int RowsLength = _Datatable.Rows.Count / NumberOfColumn;
            if ((RowsLength * NumberOfColumn) != _Datatable.Rows.Count)
                RowsLength++;

            for (int i = 0; i < RowsLength; i++)
                dt.Rows.Add();

            int row = 0;
            int column = 0;

            for (int i = 0; i < _Datatable.Rows.Count; i++)
            {
                for (int j = 0; j < NoOfColumnInDataTable; j++)
                {
                    dt.Rows[row][column] = _Datatable.Rows[i][j];
                    if ((NumberOfColumn * NoOfColumnInDataTable) - 1 == column)
                    {
                        row++;
                        column = 0;
                    }
                    else
                        column++;
                }
            }
        }
        return dt;
    }


    public static void DeleteAllFiles(string Path)
    {


        DirectoryInfo Temp = new DirectoryInfo(Path);
        if (Temp.Exists == false)
        {
            Directory.CreateDirectory(Path);
        }
        Temp = new DirectoryInfo(Path);
        Temp.Refresh();
        Temp.Attributes = FileAttributes.Normal;

        string[] Files = Directory.GetFiles(Path);

        for (int i = 0; i < Files.Length; i++)
        {
            try
            {
                FileInfo F = new FileInfo(Files[i].ToString());
                F.Delete();
            }
            catch
            { }
        }


    }


}
