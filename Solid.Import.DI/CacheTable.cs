using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Solid.Import.DI
{
    public sealed class CacheTable : ICacheTable
    {
        private DataTable dtCachedData;
        void ICacheTable.Create(List<ColumnMapping> columnMappings)
        {
            if (dtCachedData == null)
            {
                dtCachedData = new DataTable();
                foreach (var columnMapping in columnMappings)
                {
                    dtCachedData.Columns.Add(columnMapping.SourceColumn);
                }
            }
        }

        void ICacheTable.Clear(int batchSize)
        {
            if (dtCachedData.Rows.Count == batchSize)
            {
                dtCachedData.Rows.Clear();
            }
        }

        DataRow ICacheTable.CreateNewRow()
        {
            return dtCachedData.NewRow();
        }

        void ICacheTable.AddNewRow(DataRow newRow)
        {
            dtCachedData.Rows.Add(newRow);
        }


        void ICacheTable.Clear()
        {
            if(dtCachedData.Rows.Count>0)
            {
                dtCachedData.Rows.Clear();
            }
        }


        DataTable ICacheTable.GetCachedData()
        {
            return dtCachedData;
        }
    }
}
