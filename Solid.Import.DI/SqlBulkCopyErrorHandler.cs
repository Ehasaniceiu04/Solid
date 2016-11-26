using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
    public sealed class SqlBulkCopyErrorHandler:IErrorHandler
    {
        bool _isRollBack = false;
        DataTable _dtError;
        public bool IsRollBack
        {
            get { return _isRollBack; }
        }
        void IErrorHandler.AddRow(SqlBulkCopy sqlBulkCopy, DataTable dtCacheData,int errorThreshold)
        {
            if (dtCacheData.Rows.Count > 0)
            {
                //Create datatable for holding data temporarily
                DataTable dtSingleRow=CreateTemporaryTableForSingleCachedData(dtCacheData);

                //Create error table for holding error data
                CreateErrorTable(dtCacheData);


                foreach (DataRow dataRow in dtCacheData.Rows)
                {
                    if (_isRollBack)
                        break;
                    // clear the temp DataTable from which the single-record bulk copy will be done
                    dtSingleRow.Rows.Clear();
                    // load the values into the temp DataTable
                    dtSingleRow.ImportRow(dataRow);
                    try
                    {
                        sqlBulkCopy.WriteToServer(dtSingleRow);
                    }
                    catch (Exception ex)
                    {
                        if (dtSingleRow.Rows.Count > 0)
                        {
                            _dtError.ImportRow(dtSingleRow.Rows[0]);
                            _isRollBack = _dtError.Rows.Count >= errorThreshold;
                        }
                        //TODO: after catching error we shall manipulate it here
                    }
                }

            }
        }

        private void CreateErrorTable(DataTable dtCacheData)
        {
            if (_dtError == null)
                _dtError = dtCacheData.Clone();
        }

        private DataTable CreateTemporaryTableForSingleCachedData(DataTable dtCacheData)
        {
            return dtCacheData.Clone();
        }
    }
}
