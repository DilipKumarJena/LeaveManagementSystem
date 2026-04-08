using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for JoinsInDatatable
/// </summary>
public class JoinsInDatatable
{
    public JoinsInDatatable()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //FJC = First Join Column

    //SJC = Second Join Column

    public static DataTable Join(DataTable First, DataTable Second, DataColumn[] FJC, DataColumn[] SJC)
    {
        //Create Empty Table
        DataTable table = new DataTable("Join");
        // Use a DataSet to leverage DataRelation
        using (DataSet ds = new DataSet())
        {
            //Add Copy of Tables
            ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });
            //Identify Joining Columns from First
            DataColumn[] parentcolumns = new DataColumn[FJC.Length];
            for (int i = 0; i < parentcolumns.Length; i++)
            {
                parentcolumns[i] = ds.Tables[0].Columns[FJC[i].ColumnName];
            }

            //Identify Joining Columns from Second
            DataColumn[] childcolumns = new DataColumn[SJC.Length];
            for (int i = 0; i < childcolumns.Length; i++)
            {
                childcolumns[i] = ds.Tables[1].Columns[SJC[i].ColumnName];
            }
            //Create DataRelation
            DataRelation r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);
            ds.Relations.Add(r);
            //Create Columns for JOIN table
            for (int i = 0; i < First.Columns.Count; i++)
            {
                table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
            }
            for (int i = 0; i < Second.Columns.Count; i++)
            {
                //Beware Duplicates
                if (!table.Columns.Contains(Second.Columns[i].ColumnName))
                    table.Columns.Add(Second.Columns[i].ColumnName, Second.Columns[i].DataType);
                else
                    table.Columns.Add(Second.Columns[i].ColumnName + "_Second", Second.Columns[i].DataType);
            }
            //Loop through First table
            table.BeginLoadData();
            foreach (DataRow firstrow in ds.Tables[0].Rows)
            {
                //Get "Left joined" rows
                DataRow[] childrows = firstrow.GetChildRows(r);
                if (childrows != null && childrows.Length > 0)
                {
                    object[] parentarray = firstrow.ItemArray;
                    foreach (DataRow secondrow in childrows)
                    {
                        object[] secondarray = secondrow.ItemArray;
                        object[] joinarray = new object[parentarray.Length + secondarray.Length];
                        Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                        Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);
                        table.LoadDataRow(joinarray, true);
                    }
                }
                else
                {
                    //firstrow
                    DataRow DrTables = table.NewRow();
                    DrTables["Path"] = firstrow["Path"];
                    table.Rows.Add(DrTables);
                }

            }
            table.EndLoadData();
        }
        return table;
    }
    public static DataTable Join(DataTable First, DataTable Second, DataColumn FJC, DataColumn SJC)
    {
        return JoinsInDatatable.Join(First, Second, new DataColumn[] { FJC }, new DataColumn[] { SJC });
    }
    public static DataTable Join(DataTable First, DataTable Second, string FJC, string SJC)
    {
        return JoinsInDatatable.Join(First, Second, new DataColumn[] { First.Columns[FJC] }, new DataColumn[] { First.Columns[SJC] });
    }


    public static DataTable myJoinMethod(DataTable LeftTable, DataTable RightTable, String LeftPrimaryColumn, String RightPrimaryColumn)
    {
        //first create the datatable columns 
        DataSet mydataSet = new DataSet();
        mydataSet.Tables.Add("  ");
        DataTable myDataTable = mydataSet.Tables[0];

        //add left table columns 
        DataColumn[] dcLeftTableColumns = new DataColumn[LeftTable.Columns.Count];
        LeftTable.Columns.CopyTo(dcLeftTableColumns, 0);

        foreach (DataColumn LeftTableColumn in dcLeftTableColumns)
        {
            if (!myDataTable.Columns.Contains(LeftTableColumn.ToString()))
                myDataTable.Columns.Add(LeftTableColumn.ToString());
        }

        //now add right table columns 
        DataColumn[] dcRightTableColumns = new DataColumn[RightTable.Columns.Count];
        RightTable.Columns.CopyTo(dcRightTableColumns, 0);

        foreach (DataColumn RightTableColumn in dcRightTableColumns)
        {
            if (!myDataTable.Columns.Contains(RightTableColumn.ToString()))
            {
                if (RightTableColumn.ToString() != RightPrimaryColumn)
                    myDataTable.Columns.Add(RightTableColumn.ToString());
            }
        }

        //add left-table data to mytable 
        foreach (DataRow LeftTableDataRows in LeftTable.Rows)
        {
            myDataTable.ImportRow(LeftTableDataRows);
        }

        ArrayList var = new ArrayList(); //this variable holds the id's which have joined 

        ArrayList LeftTableIDs = new ArrayList();
        LeftTableIDs = DataSetToArrayList(0, LeftTable);

        //import righttable which having not equal Id's with lefttable 
        foreach (DataRow rightTableDataRows in RightTable.Rows)
        {
            if (LeftTableIDs.Contains(rightTableDataRows[0]))
            {
                string wherecondition = "[" + myDataTable.Columns[0].ColumnName + "]='"
                        + rightTableDataRows[0].ToString() + "'";
                DataRow[] dr = myDataTable.Select(wherecondition);
                int iIndex = myDataTable.Rows.IndexOf(dr[0]);

                foreach (DataColumn dc in RightTable.Columns)
                {
                    if (dc.Ordinal != 0)
                        myDataTable.Rows[iIndex][dc.ColumnName.ToString().Trim()] =
                rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                }
            }
            else
            {
                int count = myDataTable.Rows.Count;
                DataRow row = myDataTable.NewRow();
                row[0] = rightTableDataRows[0].ToString();
                myDataTable.Rows.Add(row);
                foreach (DataColumn dc in RightTable.Columns)
                {
                    if (dc.Ordinal != 0)
                        myDataTable.Rows[count][dc.ColumnName.ToString().Trim()] =
                rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                }
            }
        }

        return myDataTable;
    }

    public static ArrayList DataSetToArrayList(int ColumnIndex, DataTable dataTable)
    {
        ArrayList output = new ArrayList();

        foreach (DataRow row in dataTable.Rows)
            output.Add(row[ColumnIndex]);

        return output;
    }
}